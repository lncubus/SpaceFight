using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;
using System.Xml;
using System.Xml.Serialization;
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

        public readonly IDictionary<string, IHelm> Helms;
        public readonly IDictionary<string, Star> Stars;
        private readonly IList<IMissile> m_missiles;
        private readonly Thread m_backgroundWorker;

//        public ServerDamageContract.IServerDamageCallbackContract DamageServiceCallback;
        //public const byte BreakEverything = (byte)RanmaRepairSeverity.Hard * Subsystem.Length;
        //public const byte SevereDamage = (byte)RanmaRepairSeverity.Hard * 2;

        private byte ThroatDamage()
        {
            var r = Random.NextDouble();
            if (r < 0.1)
                return 1;//(byte)RanmaRepairSeverity.Easy;
            if (r < 0.3)
                return 2; //(byte)RanmaRepairSeverity.Medium;
            return 3;//(byte)RanmaRepairSeverity.Hard;
        }

        private byte BoardDamage()
        {
            var r = Random.NextDouble();
            if (r < 0.6)
                return 1;//(byte)RanmaRepairSeverity.Easy;
            if (r < 0.9)
                return 2;//(byte)RanmaRepairSeverity.Medium;
            return 3;//(byte)RanmaRepairSeverity.Hard;
        }

        private int CollisionDamage(bool isLight)
        {
            return isLight ? 9 : 3;
        }

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

        private string SerializeCollection<T>(IEnumerable<T> collection)
        {
            var list = collection.ToArray();
            var serializer = new XmlSerializer(list.GetType());
            var writer = new StringWriter();
            serializer.Serialize(writer, list);
            writer.Close();
            return writer.ToString();
        }

        private void SerializeCollection<T>(IEnumerable<T> collection, XmlDocument doc)
        {
            var nav = doc.CreateNavigator();
            using (XmlWriter writer = nav.AppendChild())
            {
                var list = collection.ToArray();
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

            Helms = helms.Select(Helm.Load).ToDictionary(ship => ship.Name);
            foreach (var helmDefinition in helms)
            {
                if (string.IsNullOrEmpty(helmDefinition.Carrier))
                    continue;
                var helm = (Helm)Helms[helmDefinition.Name];
                helm.CarrierShip = Helms[helmDefinition.Carrier];
            }
            Stars = stars.ToDictionary(star => star.Name);
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
                SerializeCollection(Stars.Values, starsDoc);
                SerializeCollection(Helms.Values.Select(HelmDefinition.Store), helmsDoc);
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
            if (me == null)
                return null;
            lock (m_locker)
            {
                bool blind = me.IsDead() || !me.InSpace() || me.Carrier != null;
                return new View
                {
                    Time = Time,
                    Helm = HelmDefinition.Store(me),
                    Ships = blind ? new ShipDefinition[0] : GetVisibleShips(me).Select(ShipDefinition.Store).ToArray(),
                    Missiles = blind ? new MissileDefinition[0] : GetVisibleMissiles(me).Select(MissileDefinition.Store).ToArray(),
                    Stars = GetStars().ToArray(),
                    Carried = me.Class.Superclass != ShipSuperclass.CLAC ? new string[0] : GetCarriedShips(me).Select(ship => ship.Name).ToArray(),
                };
            }
        }

        public CatalogDefinition GetCatalog(string nation)
        {
            lock (m_locker)
            {
                var shipClassesByNation = Catalog.Instance.ShipClasses.Values.Where(c => c.Nation == nation);
                var shipClassesByShips = Helms.Where(i => i.Value.Nation == nation).Select(i => i.Value.Class);
                var missileClassesByNation = Catalog.Instance.MissileClasses.Values.Where(c => c.Nation == nation);
                var missileClassesByShips = Helms.Where(i => i.Value.Nation == nation).Select(i => i.Value.Missile);
                return new CatalogDefinition
                {
                    MaximumMissileRange = Catalog.Instance.MaximumMissileRange,
                    SkirtAngle = Catalog.Instance.SkirtAngle,
                    ThroatAngle = Catalog.Instance.ThroatAngle,
                    DefaultScale = Catalog.Instance.DefaultScale,
                    CarrierRange = Catalog.Instance.CarrierRange,
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

        public void Fire(IHelm from, string to, int[] launchers)
        {
            if (from.IsDead())
                return;
            lock (m_locker)
            {
                var target = GetHelm(to);
                if (from.Missile == null || target == null || launchers == null || launchers.Length == 0)
                    return;
                var left = from.IsLeftBoard(target);
                var board = left ? from.Left : from.Right;
                if (board.Accumulator > 0)
                    return;
                var fired = false;
                foreach (var n in launchers)
                {
                    if (n < 0 || n >= board.Launchers.Length || !MathUtils.NearlyEqual(board.Launchers[n], 0))
                        continue;
                    fired = true;
                    board.Launchers[n] = from.Class.ReloadTime;
                    // missile hit the wedge
                    if (Random.NextDouble() > from.Board())
                        continue;
                    var result = new Missile(from, target, Time, (Random.NextDouble() - 0.5)*Math.PI/3);
                    m_missiles.Add(result);
                }
                if (fired)
                    board.Accumulator = from.Class.RechargeTime;
                if (left)
                    from.Left = board;
                else
                    from.Right = board;
            }
        }

        public void Launch(IHelm from, string name)
        {
            if (from.IsDead())
                return;
            lock (m_locker)
            {
                var arrow = (Helm)GetHelm(name);
                if (arrow == null || arrow.Carrier != from.Name || arrow.IsDead())
                    return;
                var arrowDef = HelmDefinition.Store(arrow);
                var angle = Random.NextAngle();
                var distance = (1 + Random.NextDouble())/3;
                arrowDef.HeadingTo = arrowDef.Heading = angle;
                arrowDef.RollTo = arrowDef.Roll = from.Roll;
                arrowDef.ThrustTo = arrowDef.Thrust = 0;
                arrowDef.Speed = from.Speed;
                arrowDef.Position = from.Position + distance*Vector.Direction(angle)*Catalog.Instance.CarrierRange;
                arrow.CarrierShip = null;
                arrow.Dynamics = new Dynamics(arrow.Class, arrowDef, Time);
                arrow.Dynamics.UpdateTime(Time.TotalSeconds);
            }
        }

        private IHelm GetHelm(string name)
        {
            IHelm result;
            if (!Helms.TryGetValue(name, out result))
                return null;
            return result;
        }

        private IEnumerable<IShip> GetVisibleShips(IHelm me)
        {
            return Helms.Values.Where(i => i != me && i.InSpace() && i.Carrier == null); 
        }

        private IEnumerable<IShip> GetCarriedShips(IHelm me)
        {
            return Helms.Values.Where(i => i.Carrier == me.Name);
        }

        private IEnumerable<IMissile> GetVisibleMissiles(IHelm me)
        {
            return m_missiles;
        }

        private IEnumerable<Star> GetStars()
        {
            return Stars.Values;
        }

        public KeyValuePair<string, string[]>[] GetShipNames()
        {
            lock (m_locker)
            {
                var nations = Helms.Select(i => i.Value.Nation).Distinct().ToList();
                nations.Sort();
                int n = nations.Count;
                var result = new KeyValuePair<string, string[]>[n];
                for (int i = 0; i < n; i++)
                {
                    var nation = nations[i];
                    var ships = Helms.Where(p => p.Value.Nation == nation).Select(p => p.Key).Distinct().ToList();
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
                    var dt = t - tPrev;
                    foreach (Helm helm in Helms.Values)
                    {
                        helm.Dynamics.UpdateTime(t);
                        helm.UpdateWeapons(dt);
                    }
                    foreach (Missile missile in m_missiles)
                        missile.UpdateTime(t);
                    CheckCollisions(t, dt);
                    foreach (Helm helm in Helms.Values.Where(helm => helm.Health.Changed))
                    {
                        helm.Dynamics.EngineHealth = helm.Health.Engine;
                        helm.Dynamics.NavigationHealth = helm.Health.Navigation;
                        helm.Dynamics.UpdateHealth(t);
                        helm.Health.ResetChanged();
                        if (helm.InSpace() && !helm.IsDead() && helm.HealthRate <= MathUtils.Epsilon)
                            helm.State = ShipState.Junk;
                        if (helm.InSpace() && helm.State == ShipState.Junk && helm.HealthRate > MathUtils.Epsilon)
                            helm.State = ShipState.Normal;
                    }
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
            var helms = Helms.Values.Where(helm => helm.InSpace()).ToList();
            foreach (Helm helm in helms)
                foreach (Star star in Stars.Values)
                    if (m_collider.HaveCollision(helm, star, dt))
                    {
                        System.Diagnostics.Trace.WriteLine(string.Format(
                            "Корабль {0} врезался в планету {1}.", helm.Name, star.Name));
                        DestroyShip(helm);
                    }
            foreach (Missile missile in m_missiles)
                foreach (Star star in Stars.Values)
                    if (m_collider.HaveCollision(missile, star, dt))
                    {
                        System.Diagnostics.Trace.WriteLine(string.Format(
                            "Ракета врезалась в планету {0}", star.Name));
                        missile.Exploded = true;
                    }
            foreach (Missile missile in m_missiles)
            {
                var target = (Helm) (missile.Target);
                if (!target.InSpace())
                {
                    missile.Exhausted = true;
                    System.Diagnostics.Trace.WriteLine(string.Format(
                        "Ракета потеряла цель {0}", target.Name));
                    continue;
                }
                if (m_collider.HaveCollision(missile, target, dt, missile.Class.HitDistance))
                {
                    System.Diagnostics.Trace.WriteLine(string.Format("Ракета поразила корабль {0}.", target.Name));
                    missile.Exploded = true;
                    var v = missile.Speed - target.Speed;
                    double angle = Math.Abs(target.Heading - v.Argument) % (2 * Math.PI);
                    if (angle > Math.PI)
                        angle = Math.PI - angle;
                    var throat = (Math.PI - angle) < Catalog.Instance.ThroatAngle/2;
                    var skirt = angle < Catalog.Instance.SkirtAngle/2;
                    System.Diagnostics.Trace.WriteLine(string.Format("Угол {0}{1}.", MathUtils.ToDegreesInt(angle), throat ? " горло" : skirt ? " юбка" : string.Empty));
                    byte severity;
                    if (throat || skirt)
                        severity = ThroatDamage();
                    else if (Random.NextDouble() <= target.Board())
                        severity = BoardDamage();
                    else
                        severity = 0;
                    if(severity > 0)
                        DamageShip(target, severity);
                }
            }
            foreach (Helm one in helms)
                foreach (Helm two in helms)
                    if (one != two && m_collider.HaveCollision(one, two, dt))
                    {
                        System.Diagnostics.Trace.WriteLine(string.Format("Корабли {0} и {1} столкниулись.", one.Name, two.Name));
                        DamageShip(one, CollisionDamage(one.IsLight()));
                        DamageShip(two, CollisionDamage(two.IsLight()));
                    }
        }

        private void DamageShip(Helm target, int severity)
        {
            System.Diagnostics.Trace.WriteLine(string.Format("Корабль {0} поврежден. Уровень {1}", target.Name, severity));
            //if (DamageServiceCallback != null)
            //{
            //    try
            //    {
            //        while (severity > (byte) RanmaRepairSeverity.Hard)
            //        {
            //            DamageServiceCallback.DamageShip(target.Id, (byte) RanmaRepairSeverity.Hard);
            //            severity -= (byte) RanmaRepairSeverity.Hard;
            //        }
            //        if (severity > 0)
            //            DamageServiceCallback.DamageShip(target.Id, severity);
            //    }
            //    catch
            //    {
            //        System.Diagnostics.Trace.WriteLine("Потеряно соединение с сервером повреждений.");
            //    }
            //}
            //else
            //{
                //target.State = ShipState.Junk;
                var health = target.Health;
                var damage = (Random.NextDouble() + 1)/2;
                if (damage*severity > health.Rate*health.Subsystems)
                    health.Crash();
                else
                    for (var i = 0; i < severity; i++)
                        health.Damage(damage, Random.Next(health.Subsystems));
            //}
        }

        private void DestroyShip(Helm target)
        {
            System.Diagnostics.Trace.WriteLine(string.Format("Корабль {0} уничтожен.", target.Name));
            //if (DamageServiceCallback != null)
            //{
            //    try
            //    {
            //        DamageServiceCallback.DestroyShip(target.Id);
            //    }
            //    catch
            //    {
            //        System.Diagnostics.Trace.WriteLine("Потеряно соединение с сервером повреждений.");
            //    }
            //}
            //else
            //{
                target.State = ShipState.Annihilated;
            //}
        }

        //public void SetShipsHealth(ShipStatus[] shipStatuses)
        //{
        //    lock (m_locker)
        //    {
        //        var helms = Helms.Values.OfType<Helm>().ToDictionary(helm => helm.Id);
        //        foreach (var status in shipStatuses)
        //        {
        //            Helm helm;
        //            if (!helms.TryGetValue(status.ShipGuid, out helm))
        //            {
        //                System.Diagnostics.Trace.WriteLine(string.Format("Ship Id = {0} not found.", status.ShipGuid));
        //                continue;
        //            }
        //            foreach (var subsystemStatus in status.SubsystemStatuses)
        //                helm.Damage[(int) subsystemStatus.SubSystem] = (byte)((int)subsystemStatus.Severity*100/(int)RanmaRepairSeverity.Hard);
        //            helm.HealthChanged = true;
        //        }
        //    }
        //}
    }
}
