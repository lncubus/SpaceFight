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
    using System.Text;

    public sealed class Universe
    {
        private static readonly Random Random = new Random();
        private static readonly SynchronizationContext Context = SynchronizationContext.Current;
        private object m_locker = new object();

        public const int SmallDelay = 100;

        private readonly System.Diagnostics.Stopwatch m_stopWatch = new System.Diagnostics.Stopwatch();

        private readonly Thread m_backgroundWorker;

        public ConstantData Constants { get; private set; }
        public int Generation { get; private set; }
        public IDictionary<int, Nation> Nations { get; private set; }
        public IDictionary<int, Star> Stars { get; private set; }
        public IDictionary<int, ShipClass> ShipClasses { get; private set; }
        public IDictionary<int, MissileClass> MissileClasses { get; private set; }
        public IDictionary<int, Ship> Ships { get; private set; }
        public IDictionary<int, Missile> Missiles { get; private set; }

        private static string SerializeObject<T>(T instance)
        {
            var serializer = new XmlSerializer(instance.GetType());
            var writer = new StringWriter();
            serializer.Serialize(writer, instance);
            writer.Close();
            return writer.ToString();
        }

        private static T DeserializeObject<T>(string source)
        {
            var serializer = new XmlSerializer(typeof(T));
            var reader = new StringReader(source);
            var read = (T)serializer.Deserialize(reader);
            reader.Close();
            return read;
        }

        private static string SerializeCollection<T>(IEnumerable<T> collection)
        {
            var list = collection.ToArray();
            var serializer = new XmlSerializer(list.GetType());
            var writer = new StringWriter();
            serializer.Serialize(writer, list);
            writer.Close();
            return writer.ToString();
        }

        private static U[] DeserializeCollection<U>(string source)
        {
            var serializer = new XmlSerializer(typeof(U[]));
            var reader = new StringReader(source);
            var read = (U[])serializer.Deserialize(reader);
            return read;
        }

        private Universe()
        {
            m_backgroundWorker = new Thread(TimingThreadStart) { IsBackground = true };
        }

        public static Universe Load(string filename)
        {
            string xml = File.ReadAllText(filename, Encoding.Unicode);
            ServerData view = DeserializeObject<ServerData>(xml);
            PermanentViewData p = view.Permanent;
            Universe u = new Universe
            {
               Constants = p.Constants,
               Generation = p.Generation,
               Nations = p.Nations.ToDictionary(nation => nation.Id),
               Stars = p.Stars.ToDictionary(star => star.Id),
               ShipClasses = p.ShipClasses.ToDictionary(shipClass => shipClass.Id),
               MissileClasses = p.MissileClasses.ToDictionary(missileClass => missileClass.Id),
               Ships = p.Ships.Select(data => new Ship(data)).ToDictionary(ship => ship.Id),
            };
            u.Initialize();
            u.UpdateControlsData(view.Controls);
            u.UpdateVolatileData(view.Volatile);
            return u;
        }

        public void Save(string filename)
        {
            lock (m_locker)
            {
                var view = new ServerData
                {
                    Permanent = GetPermanentData(),
                    Volatile = GetVolatileData(),
                    Controls = GetControlsData(),
                };
                string xml = SerializeObject(view);
                File.WriteAllText(filename, xml, Encoding.Unicode);
            }
        }

        private void Initialize()
        {
            Stars.Values.ApplyNations(Nations);
            ShipClasses.Values.ApplyNations(Nations);
            MissileClasses.Values.ApplyNations(Nations);
            Stars.Values.ApplyNations(Nations);
            Ships.Values.ApplyNations(Nations);
            foreach (var ship in Ships.Values)
                ship.Class = ShipClasses[ship.IdClass];
        }

        private void UpdateControlsData(ControlsViewData v)
        {
            lock (m_locker)
            {
                foreach (var ship in Ships.Values)
                    ship.ControlShip = null;
                foreach (var ship in v.Ships)
                    Ships[ship.Id].ControlShip = ship;
//              Missiles = v.Missiles.ToDictionary(missile => missile.Id);
            }
        }

        private void UpdateVolatileData(VolatileViewData v)
        {
            lock (m_locker)
            {
                foreach (var ship in Ships.Values)
                    ship.VolatileShip = null;
                foreach (var ship in v.Ships)
                {
                    var s = Ships[ship.Id];
                    s.VolatileShip = ship;
                    if (s.ControlShip == null)
                        s.ControlShip = new ControlShipData(ship);
                }
                Missiles = v.Missiles.ToDictionary(missile => missile.Id);
            }
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
                }
            }
        }

        public static void InternalTest()
        {
        }

        public PermanentViewData GetPermanentData()
        {
            lock (m_locker)
            {
                return new PermanentViewData
                {
                    Generation = this.Generation,
                    Time = this.Time,
                    Constants = this.Constants,
                    Nations = this.Nations.Values.ToArray(),
                    Stars = this.Stars.Values.ToArray(),
                    ShipClasses = this.ShipClasses.Values.ToArray(),
                    MissileClasses = this.MissileClasses.Values.ToArray(),
                    Ships = this.Ships.Values.Select(ship => ship.PermanentShip).ToArray(),
                };
            }
        }

        public VolatileViewData GetVolatileData()
        {
            lock (m_locker)
            {
                return new VolatileViewData
                {
                    Time = this.Time,
                    Ships = this.Ships.Values.Select(ship => ship.VolatileShip).ToArray(),
                    Missiles = this.Missiles.Values.ToArray(),
                };
            }
        }

        private ControlsViewData GetControlsData()
        {
            lock (m_locker)
            {
                return new ControlsViewData
                {
                    Ships = this.Ships.Values.Select(ship => ship.ControlShip).ToArray(),
                };
            }
        }
    }
}
