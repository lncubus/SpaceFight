using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System.Text;
using System.Xml.Serialization;
using System.Threading;

namespace SF.Space
{
    public sealed class Universe
    {
        private static readonly Random Random = new Random();

        private static readonly SynchronizationContext Context = SynchronizationContext.Current;

        public const int SmallDelay = 100;

        private readonly System.Diagnostics.Stopwatch m_stopWatch = new System.Diagnostics.Stopwatch();
        public readonly SortedDictionary<string, IShipClass> Classes = new SortedDictionary<string, IShipClass>();
        public readonly SortedDictionary<string, IHelm> Helms = new SortedDictionary<string, IHelm>();
        private readonly Thread BackgroundWorker;

        private string SerializeCollection<T, U>(ICollection<T> collection) where U : T
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
            foreach (var i in DeserializeCollection<ShipClass>(classes))
                Classes.Add(i.Name, i);
            var helms = File.ReadAllText("helms.xml");
            foreach (var def in DeserializeCollection<SpaceShip>(helms))
            {
                var helm = SpaceShip.LoadHelm(Classes, def);
                Helms.Add(helm.Ship.Name, helm);
            }
            BackgroundWorker = new Thread(TimingThreadStart);
            BackgroundWorker.IsBackground = true;
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
                    if (!BackgroundWorker.IsAlive)
                        BackgroundWorker.Start();
                }
                else
                    m_stopWatch.Stop();
            }
        }

        public TimeSpan Time
        {
            get { return m_stopWatch.Elapsed; }
        }

        public IHelm GetHelm(string nation, string name)
        {
            var helm = Helms[name];
            return helm.Ship.Nation != nation ? null : helm;
        }

        public ICollection<IShip> GetVisibleShips(IHelm me)
        {
            return Helms.Where(i => i.Value != me).Select(i => i.Value.Ship).ToList(); 
        }

        public ICollection<string> GetNations()
        {
            return Helms.Select(i => i.Value.Ship.Nation).Distinct().ToList();
        }

        public ICollection<string> GetShipNames(string nation)
        {
            return Helms.Where(i => i.Value.Ship.Nation == nation).Select(i => i.Value.Ship.Name).Distinct().ToList();
        }

        public ICollection<IShipClass> GetShipClasses(string nation)
        {
            var ourClasses = Classes.Values.Where(c => c.Nation == nation);
            var ourShipClasses = Helms.Where(i => i.Value.Ship.Nation == nation).Select(i => i.Value.Ship.Class);
            var result = ourClasses.Union(ourShipClasses).Distinct().ToList();
            return result;
        }

        private void TimingThreadStart()
        {
            while (true)
            {
                Thread.Sleep(SmallDelay);
                if (m_stopWatch.IsRunning)
                {
                    double t = Time.TotalSeconds;
                    foreach (var helm in Helms.Values)
                        ((Ship)helm.Ship).Dynamics.UpdateTime(t);
                }
            }
        }

        public void Update()
        {
            throw new InvalidOperationException();
        }
    }
}
