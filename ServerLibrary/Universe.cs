using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading;
using System.Xml;
using System.Xml.Serialization;
using HonorInterfaces;
using SF.Space;

namespace SF.ServerLibrary
{
    public sealed class Universe
    {
        private static readonly Random Random = new Random();
        private static readonly SynchronizationContext Context = SynchronizationContext.Current;
        private object m_locker = new object();

        private Collider m_collider = new Collider();

        public const int SmallDelay = 100;

        private readonly System.Diagnostics.Stopwatch m_stopWatch = new System.Diagnostics.Stopwatch();

        private readonly IDictionary<string, IHelm> m_helms;
        private readonly IDictionary<string, Star> m_stars;
        private readonly IList<IMissile> m_missiles;
        private readonly Thread m_backgroundWorker;

        public ServerDamageContract.IServerDamageCallbackContract DamageServiceCallback;


        private string SerializeObject<T>(T instance)
        {
            var serializer = new XmlSerializer(instance.GetType());
            var writer = new StringWriter();
            serializer.Serialize(writer, instance);
            writer.Close();
            return writer.ToString();
        }

        private T DeserializeObject<T>(string source)
        {
            var serializer = new XmlSerializer(typeof(T));
            var reader = new StringReader(source);
            var read = (T)serializer.Deserialize(reader);
            reader.Close();
            return read;
        }

        private string SerializeCollection<T, U>(IEnumerable<T> collection) where U : T
        {
            var list = collection.Cast<U>().ToArray();
            var serializer = new XmlSerializer(list.GetType());
            var writer = new StringWriter();
            serializer.Serialize(writer, list);
            writer.Close();
            return writer.ToString();
        }

        private void SerializeCollection<T, U>(IEnumerable<T> collection, XmlDocument doc) where U : T
        {
            var nav = doc.CreateNavigator();
            using (XmlWriter writer = nav.AppendChild())
            {
                var list = collection.Cast<U>().ToArray();
                var serializer = new XmlSerializer(list.GetType());
                serializer.Serialize(writer, list);
            }
        }

        private U[] DeserializeCollection<U>(string source)
        {
            var serializer = new XmlSerializer(typeof(U[]));
            var reader = new StringReader(source);
            var read = (U[])serializer.Deserialize(reader);
            return read;
        }

        public Universe(string fileName)
        {
            var catalog = File.ReadAllText("catalog.xml");
            var catalogDefinition = DeserializeObject<CatalogDefinition>(catalog);
            Catalog.Create(catalogDefinition);

            XmlReader starsReader = XmlReader.Create(new StringReader(File.ReadAllText(fileName)));
            starsReader.ReadToDescendant("ArrayOfStar");
            var stars = DeserializeCollection<Star>(starsReader.ReadOuterXml());

            XmlReader helmsReader = XmlReader.Create(new StringReader(File.ReadAllText(fileName)));
            helmsReader.ReadToDescendant("ArrayOfHelmDefinition");
            var helms = DeserializeCollection<HelmDefinition>(helmsReader.ReadOuterXml());

            m_helms = helms.Select(Helm.Load).ToDictionary(ship => ship.Name);
            m_stars = stars.ToDictionary(star => star.Name);
            m_missiles = new List<IMissile>();

            m_backgroundWorker = new Thread(TimingThreadStart) { IsBackground = true };
        }

        public void Save()
        {
            var fileName = DateTime.Now.ToString("s").Replace(":", "_");
            XmlDocument starsDoc = new XmlDocument();
            XmlDocument helmsDoc = new XmlDocument();
            lock (m_locker)
            {
                SerializeCollection<Star, Star>(m_stars.Values, starsDoc);
                SerializeCollection<HelmDefinition, HelmDefinition>(m_helms.Values.Select(HelmDefinition.Store), helmsDoc);
            }
            XmlDocument doc = new XmlDocument();
            var root = doc.CreateElement("Universe");
            root.AppendChild(doc.ImportNode(starsDoc.FirstChild, true)); // only one child for sure
            root.AppendChild(doc.ImportNode(helmsDoc.FirstChild, true)); // only one child for sure
            doc.AppendChild(root);
            doc.Save(fileName);
        }

        public bool IsRunning
        {
            get { return m_stopWatch.IsRunning; }
            set
            {
                if (m_stopWatch.IsRunning == value)
                    return;
                if (value)
                {
                    m_stopWatch.Start();
                    if (!m_backgroundWorker.IsAlive)
                        m_backgroundWorker.Start();
                }
                else
                    m_stopWatch.Stop();
            }
        }

        public TimeSpan Time
        {
            get { return m_stopWatch.Elapsed; }
        }

        public View GetView(IHelm me)
        {
            lock (m_locker)
            {
                bool blind = me.IsDead() || !me.InSpace();
                return new View
                {
                    Time = Time,
                    Helm = HelmDefinition.Store(me),
                    Ships = blind ? new ShipDefinition[0] : GetVisibleShips(me).Select(ShipDefinition.Store).ToArray(),
                    Missiles = blind ? new MissileDefinition[0] : GetVisibleMissiles(me).Select(MissileDefinition.Store).ToArray(),
                    Stars = GetStars().ToArray(),
                };
            }
        }

        public CatalogDefinition GetCatalog(string nation)
        {
            lock (m_locker)
            {
                var shipClassesByNation = Catalog.Instance.ShipClasses.Values.Where(c => c.Nation == nation);
                var shipClassesByShips = m_helms.Where(i => i.Value.Nation == nation).Select(i => i.Value.Class);
                var missileClassesByNation = Catalog.Instance.MissileClasses.Values.Where(c => c.Nation == nation);
                var missileClassesByShips = m_helms.Where(i => i.Value.Nation == nation).Select(i => i.Value.Missile);
                return new CatalogDefinition
                {
                    MaximumMissileRange = Catalog.Instance.MaximumMissileRange,
                    SkirtAngle = Catalog.Instance.SkirtAngle,
                    ThroatAngle = Catalog.Instance.ThroatAngle,
                    DefaultScale = Catalog.Instance.DefaultScale,
                    ShipClasses = shipClassesByNation.Union(shipClassesByShips).Distinct().ToArray(),
                    MissileClasses = missileClassesByNation.Union(missileClassesByShips).Distinct().ToArray(),
                };
            }
        }

        public IHelm GetHelm(string nation, string name)
        {
            lock (m_locker)
            {
                var helm = GetHelm(name);
                if (helm.Nation != nation)
                    return null;
                return helm;
            }
        }

        public void Fire(IShip from, bool left, string to, int number)
        {
            if (from.IsDead())
                return;
            lock (m_locker)
            {
                number = Math.Min(number, from.Missiles);
                var target = GetHelm(to);
                if (from.Missile == null || target == null || number <= 0)
                    return;
                var result = new Missile(from, left, target, number, Time);
                m_missiles.Add(result);
            }
        }

        private IHelm GetHelm(string name)
        {
            IHelm result;
            if (!m_helms.TryGetValue(name, out result))
                return null;
            return result;
        }

        private IEnumerable<IShip> GetVisibleShips(IHelm me)
        {
            return m_helms.Values.Where(i => i != me && i.InSpace()); 
        }

        private IEnumerable<IMissile> GetVisibleMissiles(IHelm me)
        {
            return m_missiles;
        }

        private IEnumerable<Star> GetStars()
        {
            return m_stars.Values;
        }

        public KeyValuePair<string, string[]>[] GetShipNames()
        {
            lock (m_locker)
            {
                var nations = m_helms.Select(i => i.Value.Nation).Distinct().ToList();
                nations.Sort();
                int n = nations.Count;
                var result = new KeyValuePair<string, string[]>[n];
                for (int i = 0; i < n; i++)
                {
                    var nation = nations[i];
                    var ships = m_helms.Where(p => p.Value.Nation == nation).Select(p => p.Key).Distinct().ToList();
                    ships.Sort();
                    result[i] = new KeyValuePair<string, string[]>(nation, ships.ToArray());
                }
                return result;
            }
        }
        
        private void TimingThreadStart()
        {
            var tPrev = Time.TotalSeconds;
            while (true)
            {
                Thread.Sleep(SmallDelay);
                if (!m_stopWatch.IsRunning)
                    continue;
                lock (m_locker)
                {
                    double t = Time.TotalSeconds;
                    foreach (Ship helm in m_helms.Values)
                        helm.Dynamics.UpdateTime(t);
                    foreach (Missile missile in m_missiles)
                        missile.UpdateTime(t);
                    var dt = t - tPrev;
                    CheckCollisions(t, dt);
                    tPrev = t;
                    var deleted = m_missiles.Where(missile => missile.IsDead).ToList();
                    foreach (var missile in deleted)
                        m_missiles.Remove(missile);
                }
            }
        }

        private void CheckCollisions(double t, double dt)
        {
            if (dt < MathUtils.Epsilon)
                return;
            var helms = m_helms.Values.Where(helm => helm.InSpace()).ToList();
            foreach (Ship helm in helms)
                foreach (Star star in m_stars.Values)
                    if (m_collider.HaveCollision(helm, star, dt))
                    {
                        System.Diagnostics.Trace.WriteLine(string.Format(
                            "Корабль {0} врезался в планету {1}.", helm.Name, star.Name));
                        if (DamageServiceCallback != null)
                        {
                            DamageServiceCallback.DestroyShip(helm.Id);
                        }
                        helm.State = ShipState.Annihilated;
                    }
            foreach (Missile missile in m_missiles)
                foreach (Star star in m_stars.Values)
                    if (m_collider.HaveCollision(missile, star, dt))
                    {
                        System.Diagnostics.Trace.WriteLine(string.Format(
                            "Ракета врезалась в планету {0}", star.Name));
                        missile.Exploded = true;
                    }
            foreach (Missile missile in m_missiles)
            {
                var target = (Helm) (missile.Target);
                if (m_collider.HaveCollision(missile, target, dt, missile.Class.HitDistance))
                {
                    System.Diagnostics.Trace.WriteLine(string.Format("Ракета поразила корабль {0}.", target.Name));
                    if (DamageServiceCallback != null)
                    {
                        DamageServiceCallback.DamageShip(target.Id, Convert.ToByte(Random.Next(2) + 1));
                    }
                    missile.Exploded = true;
                    target.State = ShipState.Junk;
                }
            }
            foreach (Ship one in helms)
                foreach (Ship two in helms)
                    if (one != two && m_collider.HaveCollision(one, two, dt))
                    {
                        System.Diagnostics.Trace.WriteLine(string.Format("Корабли {0} и {1} столкниулись.", one.Name, two.Name));
                        if (DamageServiceCallback != null)
                        {
                            DamageServiceCallback.DestroyShip(one.Id);
                            DamageServiceCallback.DestroyShip(two.Id);
                        }
                        one.State = ShipState.Junk;
                        two.State = ShipState.Junk;
                    }
        }

        static int generation = 0;

        public void BigBangTest()
        {
            lock (m_locker)
            {
                for (int i = 0; i < 100; i++)
                {
                    var h = Random.NextAngle();
                    var a = Random.NextDouble();
                    var classification = Catalog.Instance.ShipClasses.Values.First();
                    var missile = Catalog.Instance.MissileClasses.Values.First();
                    var helm = new HelmDefinition
                               {
                                   Thrust = a * classification.MaximumAcceleration,
                                   ThrustTo = a * classification.MaximumAcceleration,
                                   ClassName = classification.Name,
                                   Heading = h,
                                   HeadingTo = h,
                                   Nation = "Солярианская Лига",
                                   Name = "Бандит-" + (generation > 0 ? generation + "-" : "") + (i + 1),
                                   Missiles = 1,
                                   MissileName = missile.Name,
                                   Position = 300000000 * (Random.NextDouble() + Random.NextDouble()) * Random.NextDirection(),
                                   Speed = 300000 * (Random.NextDouble() + Random.NextDouble()) * Random.NextDirection(),
                               };
                    m_helms.Add(helm.Name, Helm.Load(helm));
                }
                for (int i = 0; i < 1000; i++)
                {
                    var from = m_helms.Values.RandomOf(Random);
                    var to = m_helms.Values.RandomOf(Random);
                    var missile = new Missile(from, true, to, 1, Time);
                    m_missiles.Add(missile);
                }
                generation++;
            }
        }
    }
}
