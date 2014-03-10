using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;
using System.Text;
using SF.Space;

namespace SF.ServerLibrary
{
    public sealed partial class Universe
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
        public IDictionary<int, ServerShip> Ships { get; private set; }
        public IDictionary<int, Missile> Missiles { get; private set; }

        private Universe()
        {
            m_backgroundWorker = new Thread(TimingThreadStart) { IsBackground = true };
        }

        public static Universe Load(string filename)
        {
            string xml = File.ReadAllText(filename, Encoding.Unicode);
            ServerData view = Xml.DeserializeObject<ServerData>(xml);
            PermanentViewData p = view.Permanent;
            Universe u = new Universe
            {
               Constants = p.Constants,
               Generation = p.Generation,
               Nations = p.Nations.ToDictionary(nation => nation.Id),
               Stars = p.Stars.ToDictionary(star => star.Id),
               ShipClasses = p.ShipClasses.ToDictionary(shipClass => shipClass.Id),
               MissileClasses = p.MissileClasses.ToDictionary(missileClass => missileClass.Id),
               Ships = p.Ships.Select(data => new ServerShip(data)).ToDictionary(ship => ship.Id),
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
                string xml = Xml.SerializeObject(view);
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
            foreach (var shipClass in ShipClasses.Values)
            {   
                foreach (var missileRack in shipClass.Right)
                    missileRack.MissileClass = MissileClasses[missileRack.IdMissileClass];
                foreach (var missileRack in shipClass.Left)
                    missileRack.MissileClass = MissileClasses[missileRack.IdMissileClass];
            }
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
                {
                    var s = Ships[ship.Id];
                    s.ControlShip = ship;
                    s.Right = new MissileRacksState(s.Class.Right);
                    s.Left = new MissileRacksState(s.Class.Left);
                    s.Right.SetStatePairs(ship.Right);
                    s.Left.SetStatePairs(ship.Left);
                }
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
                        s.ControlShip = new ControlShipData(ship, s.Class);
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
                    foreach (var ship in Ships.Values)
                    {
                        ship.Move(t, dt);
                        ship.Reload(dt);
                    }
                    tPrev = t;
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
                    Ships = Ships.Values.Select(ship => ship.ControlShip).ToArray(),
                };
            }
        }
    }
}
