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
        private int IdNext = 10001;

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
        public IDictionary<int, MissileControl> Missiles { get; private set; }

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
                foreach (var control in v.Ships)
                {
                    var ship = Ships[control.Id];
                    ship.ControlShip = control;
                    ship.Right = new MissileRacksState(ship.Class.Right);
                    ship.Left = new MissileRacksState(ship.Class.Left);
                    if (control.Right != null && control.Right.Length == ship.Right.Reloading.Length)
                        ship.Right.Reloading = control.Right;
                    else
                        control.Right = ship.Right.Reloading;
                    if (control.Left != null && control.Left.Length == ship.Left.Reloading.Length)
                        ship.Left.Reloading = control.Left;
                    else
                        control.Left = ship.Left.Reloading;
                }
                Missiles = v.Missiles.ToDictionary(missile => missile.Id);
                if (Missiles.Any())
                {
                    int idMax = Missiles.Keys.Max() + 1;
                    if (idMax > IdNext)
                        IdNext = idMax;
                }
            }
        }

        private void UpdateVolatileData(VolatileViewData v)
        {
            lock (m_locker)
            {
                foreach (var ship in Ships.Values)
                    ship.VolatileShip = null;
                foreach (var s in v.Ships)
                {
                    var ship = Ships[s.Id];
                    ship.VolatileShip = s;
                    if (ship.ControlShip == null)
                        ship.ControlShip = new ControlShipData(s, ship.Class);
                }
                foreach (var m in v.Missiles)
                {
                    var control = Missiles[m.Id];
                    control.Arrow = m;
                    control.Origin = Ships[control.IdOrigin];
                    control.Target = Ships[control.IdTarget];
                }
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
                    foreach (var missile in Missiles.Values)
                    {
                        missile.Move(t, dt);
                    }
                    CheckCollisions(t, dt);
                    tPrev = t;
                }
            }
        }

        private void CheckCollisions(double t, double dt)
        {
            if (dt < MathUtils.Epsilon)
                return;
            var ships = Ships.Values.Where(helm => helm.VolatileShip != null).ToArray();
            var missiles = Missiles.Values.Where(control => control.Arrow != null).ToArray();
            foreach (var ship in ships)
                foreach (var star in Stars.Values)
                    if (Collision(ship, star, dt, star.Radius + ship.Class.Wedge))
                    {
                        System.Diagnostics.Trace.WriteLine(string.Format(
                            "Корабль {0} врезался в планету {1}.", ship.Name, star.Name));
                        DestroyShip(ship);
                    }
            foreach (var missile in missiles)
                foreach (Star star in Stars.Values)
                    if (Collision(missile.Arrow, star, dt, star.Radius))
                    {
                        System.Diagnostics.Trace.WriteLine(string.Format(
                            "Ракета врезалась в планету {0}", star.Name));
                        missile.Arrow = null;
                    }
            foreach (var missile in missiles)
            {
                var target = (ServerShip)missile.Target;
                if (target.VolatileShip == null)
                {
                    System.Diagnostics.Trace.WriteLine(string.Format(
                        "Ракета потеряла цель {0}", target.Name));
                    missile.Arrow = null;
                }
                else if (Collision(missile.Arrow, target, dt, missile.HitDistance))
                {
                    System.Diagnostics.Trace.WriteLine(string.Format("Ракета поразила корабль {0}.", target.Name));
                    var v = missile.Arrow.Speed - target.Speed;
                    double angle = Math.Abs(target.Heading - v.Argument) % (2 * Math.PI);
                    if (angle > Math.PI)
                        angle = Math.PI - angle;
                    var throat = (Math.PI - angle) < Constants.DefaultThroatAngle / 2;
                    var skirt = angle < Constants.DefaultSkirtAngle / 2;
                    System.Diagnostics.Trace.WriteLine(string.Format("Угол {0}{1}.", MathUtils.ToDegreesInt(angle), throat ? " горло" : skirt ? " юбка" : string.Empty));
                    double severity;
                    if (throat || skirt)
                        severity = ThroatDamage();
                    else if (Random.NextDouble() <= target.Board())
                        severity = BoardDamage();
                    else
                        severity = 0;
                    if (severity > 0)
                        DamageShip(target, severity);
                    missile.Arrow = null;
                }
            }
            foreach (var one in ships)
                foreach (var two in ships)
                    if (one != two && Collision(one, two, dt, one.Class.Wedge + two.Class.Wedge))
                    {
                        System.Diagnostics.Trace.WriteLine(string.Format("Корабли {0} и {1} столкниулись.", one.Name, two.Name));
                        DamageShip(one, CollisionDamage());
                        DamageShip(two, CollisionDamage());
                    }
        }

        private double BoardDamage()
        {
            return Random.NextDouble();
        }

        private double ThroatDamage()
        {
            return 2*Random.NextDouble();
        }

        private double CollisionDamage()
        {
            return 4*Random.NextDouble();
        }

        private void DestroyShip(ServerShip ship)
        {
            ship.VolatileShip = null;
        }

        private void DamageShip(ServerShip ship, double damage)
        {
            var damages = new double[]
            {
                ship.ControlShip.EngineDamage,
                ship.ControlShip.NavigationDamage,
                ship.ControlShip.AttackDamage,
                ship.ControlShip.DefenseDamage,
            };
            for (int i = 0; i < damages.Length; i++)
            {
                damages[i] = Math.Min(1, damages[i] + damage*Random.NextDouble());
            }
            ship.ControlShip.EngineDamage = damages[0];
            ship.ControlShip.NavigationDamage = damages[1];
            ship.ControlShip.AttackDamage = damages[2];
            ship.ControlShip.DefenseDamage = damages[3];
            ship.VolatileShip.HealthRate = 1 - damages.Sum() / damages.Length;
            ship.UpdateHealth(Time.TotalSeconds);
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
                    Missiles = this.Missiles.Values.Select(control => control.Arrow).Where(missile => missile != null).ToArray(),
                };
            }
        }

        public void Fire(ServerShip ship, bool isLeft, int number, int idTarget, Ecm jammer)
        {
            lock (m_locker)
            {
                if (ship.Missiles == 0)
                    return;
                var target = Ships.ById(idTarget);
                if (target == null || isLeft != ship.IsLeftBoard(target))
                    return;
                var board = isLeft ? ship.Left : ship.Right;
                var missileClass = board.GetRack(number).MissileClass;
                if (!board.Fire(number))
                    return;
                ship.Missiles--;
                int id = Interlocked.Increment(ref IdNext);
                var missile = new Missile
                {
                    Id = id,
                    Position = ship.Position,
                    Speed = ship.Speed,
                    Acceleration = ship.Acceleration,
                };
                var control = new MissileControl
                {
                    Id = id,
                    Arrow = missile,
                    Origin = ship,
                    Target = target,
                    IdOrigin = ship.Id,
                    IdTarget = target.Id,
                    Jammer = jammer,
                    Thrust = missileClass.Acceleration,
                    Remaining = missileClass.FlyTime,
                    HitDistance = missileClass.HitDistance,
                    Started = Time.TotalSeconds,
                };
                Missiles.Add(id, control);
            }
        }

        private bool Collision(IParticle missile, IParticle target, double dt, double crossRadius = 0)
        {
            var S = missile.Position - target.Position;
            var V = missile.Speed - target.Speed;
            // Collision at t = 0
            if (S.Length <= crossRadius)
                return true;
            var Vx2Vy2 = V.SquareLength;
            var VxSxVySy = V * S;
            // No collision within time interval
            if (Vx2Vy2 * dt <= Math.Abs(VxSxVySy))
                return false;
            // Potential collision time
            var t = -VxSxVySy / Vx2Vy2;
            var s = S + V * t;
            // Was that near enough?
            return s.Length <= crossRadius;
        }

        private ControlsViewData GetControlsData()
        {
            lock (m_locker)
            {
                return new ControlsViewData
                {
                    Ships = Ships.Values.Select(ship => ship.ControlShip).ToArray(),
                    Missiles = Missiles.Values.ToArray(),
                };
            }
        }
    }
}
