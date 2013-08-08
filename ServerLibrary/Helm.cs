using System;
using System.Collections.Generic;
using SF.Space;

namespace SF.ServerLibrary
{
    internal class Helm : IHelm
    {
        protected Dynamics Dynamics { get { return ((Ship)this.Ship).Dynamics; } }

        public double RollTo { get { return this.Dynamics.RollTo; } set { this.Dynamics.RollTo = value; } }
        public double HeadingTo { get { return this.Dynamics.HeadingTo; } set { this.Dynamics.HeadingTo = value; } }
        public double AccelerateTo { get { return this.Dynamics.AccelerateTo; } set { this.Dynamics.AccelerateTo = value; } }
        public IShip Ship { get; private set; }

        public static IHelm Load(HelmDefinition that)
        {
            var shipClass  Catalog.Instance.GetShipClass(that.ClassName);
            var shipDynamics = new Dynamics(shipClass, that, TimeSpan.Zero);
            return new Helm
            {
                Ship = new Ship
                {
                    Name = that.ShipName,
                    Class = shipClass,
                    Nation = that.Nation,
                    Dynamics = shipDynamics,
                },
            };
        }
    }
}
