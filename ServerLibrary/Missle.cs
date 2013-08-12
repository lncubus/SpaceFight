using SF.Space;

namespace SF.ServerLibrary
{
    using System;

    public class Missle : IMissle
    {
        public MissleClass Class { get; set; }
        public string Nation { get; set; }

        private string m_className;
        public string ClassName
        {
            get { return this.Class == null ? this.m_className : this.Class.Name; }
            set { this.m_className = value; }
        }

        public IShip Target { get; set; }

        public int Number { get; set; }

        public bool Fired { get; private set; }
        public bool Exhausted { get; private set; }

        public Vector S { get; private set; }
        public Vector V { get; private set; }

        public Vector A
        {
            get
            {
                return this.Class.Acceleration * Vector.Direction(heading);
            }
        }

        private double t0;
        private double t1;
        private Vector v0;
        private Vector s0;
        private double heading;

        public Missle(MissleClass missleClass, IHelm from, bool left, IShip to, int number, TimeSpan time)
        {
            this.Class = missleClass;
            this.s0 = this.S = from.Ship.S;
            this.v0 = this.V = from.Ship.V;
            this.t1 = this.t0 = time.TotalSeconds;
            this.Number = number;
            this.heading = from.Ship.Heading + (left ? -Math.PI / 2 : Math.PI / 2);
        }

        public void UpdateTime(double time)
        {
            var t = time - this.t0;
            var t2 = t * t / 2;
            this.V = this.v0 + A * t;
            this.S = this.s0 + this.v0 * t + this.A * t2;
            //// end of the current arc
            //bool changed = (this.headingTo.HasValue || this.accelerateTo.HasValue || this.Acceleration.WillReset(time) || this.Heading.WillReset(time));
            //if (changed)
            //{
            //    this.v0 = this.V;
            //    this.t0 = time;
            //    this.Heading.Set(time, this.HeadingTo);
            //    this.Acceleration.Set(time, this.AccelerateTo);
            //    this.headingTo = null;
            //    this.accelerateTo = null;
            //}
            //this.AccelerationValue = a1;
            //this.HeadingValue = phi1;
        }
    }
}
