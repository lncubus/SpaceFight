using System;
using SF.Space;

namespace SF.ServerLibrary
{
    internal class Helm : Ship, IHelm
    {
        public double RollTo
        {
            get
            {
                return Dynamics.RollTo;
            }
            set
            {
                if (this.IsDead())
                    return;
                Dynamics.RollTo = value;
            }
        }

        public double AttackHealth
        {
            get; set;
        }

        public double DefenseHealth
        {
            get; set;
        }

        public double EngineHealth
        {
            get
            {
                return Dynamics.EngineHealth;
            }
            set
            {
                Dynamics.HealthChanged = true;
                Dynamics.EngineHealth = value;
            }
        }
        //    if (Damage[Subsystem.Reactor] == 100)
        //        State = ShipState.Junk;
        //    if (Damage[Subsystem.Attack] == 100 && Damage[Subsystem.Defense] == 100 && Damage[Subsystem.Navigation] == 100 && Damage[Subsystem.Wedge] == 100)
        //        State = ShipState.Junk;
        //    HealthChanged = false;
        //    var engineDamage = this.IsDead() ? 1.0 : Math.Max(Damage[Subsystem.Wedge], Damage[Subsystem.Reactor]) / 100.0;
        //    var steeringDamage = this.IsDead() ? 1.0 : Math.Max(Damage[Subsystem.Navigation], Damage[Subsystem.Reactor]) / 100.0;
        //    AttackHealth = this.IsDead() ? 0.0 : 1.0 - Math.Max(Damage[Subsystem.Attack], Damage[Subsystem.Reactor]) / 100.0;
        //    if (GateHealth >= 0)
        //        GateHealth = this.IsDead() ? 0.0 : 1.0 - Math.Max(Damage[Subsystem.Gate], Damage[Subsystem.Reactor]) / 100.0;
        //    1 - engineDamage;
        //    Dynamics.SteeringHealth = 1 - steeringDamage;
        //    Dynamics.UpdateHealth(, Class);
        //}

        public double NavigationHealth
        {
            get
            {
                return Dynamics.NavigationHealth;
            }
            set
            {
                Dynamics.HealthChanged = true;
                Dynamics.NavigationHealth = value;
            }
        }

        public double HeadingTo
        {
            get
            {
                return Dynamics.HeadingTo;
            }
            set
            {
                if (this.IsDead())
                    return;
                Dynamics.HeadingTo = value;
            }
        }

        public double ThrustTo
        {
            get
            {
                return Dynamics.AccelerateTo;
            }
            set
            {
                if (this.IsDead())
                    return;
                Dynamics.AccelerateTo = value;
            }
        }

        public IShip CarrierShip { get; set; }

        public string Carrier
        {
            get
            {
                return CarrierShip == null ? null : CarrierShip.Name;
            }
        }

        public Board Right
        {
            get; set;
        }

        public Board Left
        {
            get; set;
        }

        public static IHelm Load(HelmDefinition that)
        {
            var shipClass = Catalog.Instance.GetShipClass(that.ClassName);
            if (shipClass == null)
                throw new NullReferenceException("Undefined ship class " + that.ClassName);
            MissileClass missileClass = null;
            if (!string.IsNullOrEmpty(that.MissileName))
            {
                missileClass = Catalog.Instance.GetMissileClass(that.MissileName);
                if (missileClass == null)
                    throw new NullReferenceException("Undefined Missile class " + that.MissileName);
            }
            var shipDynamics = new Dynamics(shipClass, that, TimeSpan.Zero);
            var helm = new Helm
            {
                Id = that.Id == Guid.Empty ? Guid.NewGuid() : that.Id,
                Class = shipClass,
                Name = that.Name,
                Nation = that.Nation,
                Dynamics = shipDynamics,
                Missile = missileClass,
                Missiles = that.Missiles,
                State =  that.State,
                Right = that.Right.Launchers != null ? that.Right : new Board { Accumulator = 0, Launchers = new double[that.Missiles] },
                Left = that.Left.Launchers != null ? that.Left : new Board { Accumulator = 0, Launchers = new double[that.Missiles] },
            };
            helm.AttackHealth = that.AttackHealth;
            helm.DefenseHealth = that.DefenseHealth;
            helm.EngineHealth = that.EngineHealth;
            helm.NavigationHealth = that.EngineHealth;
            return helm;
        }

        public void UpdateWeapons(double dt)
        {
            Left = UpdateBoard(Left, dt);
            Right = UpdateBoard(Right, dt);
        }

        private Board UpdateBoard(Board board, double dt)
        {
            var accumulator = board.Accumulator <= 0 ? board.Accumulator : Math.Max(board.Accumulator - dt*AttackHealth, 0);
            var launchers = board.Launchers;
            for (int i = 0; i < launchers.Length; i++)
                launchers[i] = launchers[i] <= 0 ? launchers[i] : Math.Max(launchers[i] - dt*AttackHealth, 0);
            return new Board
            {
                Accumulator = accumulator,
                Launchers = launchers,
            };
        }
    }
}
