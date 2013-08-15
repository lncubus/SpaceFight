using SF.Space;

namespace SF.ServerLibrary
{
    internal class Ship : IShip
    {
        public string Nation { get; set; }
        public string Name { get; set; }
        public Dynamics Dynamics;
        public double Heading { get { return this.Dynamics.HeadingValue; } }
        public double Roll { get { return this.Dynamics.RollValue; } }
        public double Acceleration { get { return this.Dynamics.AccelerationValue; } }

        public ShipClass Class { get; set; }
        public string ClassName
        {
            get { return this.Class.Name; }
        }

        public MissileClass Missile { get; set; }
        public int Missiles { get; set; }
        public string MissileName
        {
            get { return this.Missile.Name; }
        }

        public Vector S
        {
            get { return this.Dynamics.S; }
        }
        public Vector V
        {
            get { return this.Dynamics.V; }
        }
        public Vector A
        {
            get { return this.Dynamics.A; }
        }
    }
}
