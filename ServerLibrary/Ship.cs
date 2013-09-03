using System;
using SF.Space;

namespace SF.ServerLibrary
{

    internal class Ship : IShip
    {
        public Guid Id { get; set; }
        public string Nation { get; set; }
        public string Name { get; set; }
        public Dynamics Dynamics;
        public double Heading { get { return Dynamics.HeadingValue; } }
        public double Roll { get { return Dynamics.RollValue; } }
        public double Thrust { get { return Dynamics.AccelerationValue; } }

        public ShipClass Class { get; set; }
        public string ClassName
        {
            get { return Class.Name; }
        }

        public MissileClass Missile { get; set; }
        public int Missiles { get; set; }
        public string MissileName
        {
            get { return Missile.Name; }
        }

        public ShipState State { get; set; }

        public Vector Position
        {
            get { return Dynamics.S; }
        }
        public Vector Speed
        {
            get { return Dynamics.V; }
        }
        public Vector Acceleration
        {
            get { return Dynamics.A; }
        }

        public double Weight
        {
            get { return Class.Weight; }
        }

        public double Radius
        {
            get { return Class.Wedge; }
        }
    }
}
