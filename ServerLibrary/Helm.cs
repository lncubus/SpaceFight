using System;
using System.Collections.Generic;
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
            return new Helm
            {
                Id = that.Id == Guid.Empty ? Guid.NewGuid() : that.Id,
                Class = shipClass,
                Name = that.Name,
                Nation = that.Nation,
                Dynamics = shipDynamics,
                Missile = missileClass,
                Missiles = that.Missiles,
                State =  that.State,
            };
        }
    }
}
