using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;
using System.Xml.Serialization;

using SF.Space;

namespace SF.ServerLibrary
{
    public sealed class Universe
    {
        private static readonly Random Random = new Random();

        private static readonly SynchronizationContext Context = SynchronizationContext.Current;

        public const int SmallDelay = 100;

        private readonly System.Diagnostics.Stopwatch m_stopWatch = new System.Diagnostics.Stopwatch();

        private readonly IDictionary<string, IHelm> m_helms;
        private readonly Thread m_backgroundWorker;

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

        private U[] DeserializeCollection<U>(string source)
        {
            var serializer = new XmlSerializer(typeof(U[]));
            var reader = new StringReader(source);
            var read = (U[])serializer.Deserialize(reader);
            return read;
        }

        public Universe()
        {
            var catalog = File.ReadAllText("catalog.xml");
            var catalogDefinition = this.DeserializeObject<CatalogDefinition>(catalog);
            Catalog.Create(catalogDefinition);
            var ships = File.ReadAllText("helms.xml");
            var helms = this.DeserializeCollection<HelmDefinition>(ships);
            m_helms = helms.Select(Helm.Load).ToDictionary(ship => ship.Ship.Name);
            this.m_backgroundWorker = new Thread(this.TimingThreadStart) { IsBackground = true };
        }

        public bool IsRunning
        {
            get { return this.m_stopWatch.IsRunning; }
            set
            {
                if (this.m_stopWatch.IsRunning == value)
                    return;
                if (value)
                {
                    this.m_stopWatch.Start();
                    if (!this.m_backgroundWorker.IsAlive)
                        this.m_backgroundWorker.Start();
                }
                else
                    this.m_stopWatch.Stop();
            }
        }

        public TimeSpan Time
        {
            get { return this.m_stopWatch.Elapsed; }
        }

        public CatalogDefinition GetCatalog(string nation)
        {
            var shipClassesByNation = Catalog.Instance.ShipClasses.Values.Where(c => c.Nation == nation);
            var shipClassesByShips = this.m_helms.Where(i => i.Value.Ship.Nation == nation).Select(i => i.Value.Ship.Class);
            var missleClassesByNation = Catalog.Instance.MissleClasses.Values.Where(c => c.Nation == nation);
            //var missleClassesByShips = this.m_helms.Where(i => i.Value.Ship.Nation == nation).Select(i => i.Value.Ship.Class);
            return new CatalogDefinition
            {
                ShipClasses = shipClassesByNation.Union(shipClassesByShips).Distinct().ToArray(),
                MissleClasses = missleClassesByNation.ToArray(),
            };
        }

        public IHelm GetHelm(string nation, string name)
        {
            var helm = this.m_helms[name];
            return helm.Ship.Nation != nation ? null : helm;
        }

        public IEnumerable<IShip> GetVisibleShips(IHelm me)
        {
            return this.m_helms.Where(i => i.Value != me).Select(i => i.Value.Ship); 
        }

        public IEnumerable<IMissle> GetVisibleMissles(IHelm me)
        {
            yield break;
        }

        public IEnumerable<string> GetNations()
        {
            return this.m_helms.Select(i => i.Value.Ship.Nation).Distinct();
        }

        public IEnumerable<string> GetShipNames(string nation)
        {
            return this.m_helms.Where(i => i.Value.Ship.Nation == nation).Select(i => i.Value.Ship.Name).Distinct();
        }

        private void TimingThreadStart()
        {
            while (true)
            {
                Thread.Sleep(SmallDelay);
                if (this.m_stopWatch.IsRunning)
                {
                    double t = this.Time.TotalSeconds;
                    foreach (var helm in this.m_helms.Values)
                        ((Ship)helm.Ship).Dynamics.UpdateTime(t);
                }
            }
        }
    }
}
