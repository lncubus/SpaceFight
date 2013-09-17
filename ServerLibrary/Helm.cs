using System;
using SF.Space;

namespace SF.ServerLibrary
{
    using System.Diagnostics;

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

        protected override double GetHealth()
        {
            return Health.Rate;
        }

        public Health Health { get; private set; }

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
                Health = that.Health,
                Right = that.Right.Launchers != null ? that.Right : new Board { Accumulator = 0, Launchers = new double[that.Missiles] },
                Left = that.Left.Launchers != null ? that.Left : new Board { Accumulator = 0, Launchers = new double[that.Missiles] },
            };
            return helm;
        }

        public void UpdateWeapons(double dt)
        {
            Left = UpdateBoard(Left, dt);
            Right = UpdateBoard(Right, dt);
        }

        private Board UpdateBoard(Board board, double dt)
        {
            var accumulator = board.Accumulator <= 0 ? board.Accumulator : Math.Max(board.Accumulator - dt*this.Health.Attack, 0);
            var launchers = board.Launchers;
            for (int i = 0; i < launchers.Length; i++)
                launchers[i] = launchers[i] <= 0 ? launchers[i] : Math.Max(launchers[i] - dt*this.Health.Attack, 0);
            return new Board
            {
                Accumulator = accumulator,
                Launchers = launchers,
            };
        }
    }
}
