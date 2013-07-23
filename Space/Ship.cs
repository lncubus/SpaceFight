using System;

namespace SF.Space
{
    internal class Ship : IShip
    {
        public IShipClass Class { get; set; }
        public string Nation { get; set; }
        public string Name { get; set; }
        public Dynamics Dynamics;
        public double Heading { get { return Dynamics.HeadingValue; } }
        public double Roll { get { return Dynamics.RollValue; } }
        public double Acceleration { get { return Dynamics.AccelerationValue; } }

        private string m_className;
        public string ClassName
        {
            get { return Class == null ? m_className : Class.Name; }
            set { m_className = value; }
        }

        public Vector S
        {
            get { return Dynamics.S; }
        }
        public Vector V
        {
            get { return Dynamics.V; }
        }
        public Vector A
        {
            get { return Dynamics.A; }
        }
    }
}
