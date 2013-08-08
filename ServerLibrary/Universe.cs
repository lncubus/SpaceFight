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
        public readonly SortedDictionary<string, IHelm> Helms = new SortedDictionary<string, IHelm>();
        private readonly Thread m_backgroundWorker;

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
            var classes = File.ReadAllText("classes.xml");
            foreach (var i in this.DeserializeCollection<ShipClass>(classes))
                this.Classes.Add(i.Name, i);
            var helms = File.ReadAllText("helms.xml");
            foreach (var def in this.DeserializeCollection<HelmDefinition>(helms))
            {
                var helm = Helm.Load(this.Classes, def);
                this.Helms.Add(helm.Ship.Name, helm);
            }
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

        public IHelm GetHelm(string nation, string name)
        {
            var helm = this.Helms[name];
            return helm.Ship.Nation != nation ? null : helm;
        }

        public IEnumerable<IShip> GetVisibleShips(IHelm me)
        {
            return this.Helms.Where(i => i.Value != me).Select(i => i.Value.Ship).ToList(); 
        }

        public ICollection<string> GetNations()
        {
            return this.Helms.Select(i => i.Value.Ship.Nation).Distinct().ToList();
        }

        public ICollection<string> GetShipNames(string nation)
        {
            return this.Helms.Where(i => i.Value.Ship.Nation == nation).Select(i => i.Value.Ship.Name).Distinct().ToList();
        }

        public IEnumerable<ShipClass> GetShipClasses(string nation)
        {
            var ourClasses = this.Classes.Values.Where(c => c.Nation == nation);
            var ourShipClasses = this.Helms.Where(i => i.Value.Ship.Nation == nation).Select(i => i.Value.Ship.Class);
            var result = ourClasses.Union(ourShipClasses).Distinct().ToList();
            return result;
        }

        private void TimingThreadStart()
        {
            while (true)
            {
                Thread.Sleep(SmallDelay);
                if (this.m_stopWatch.IsRunning)
                {
                    double t = this.Time.TotalSeconds;
                    foreach (var helm in this.Helms.Values)
                        ((Ship)helm.Ship).Dynamics.UpdateTime(t);
                }
            }
        }
    }
}
