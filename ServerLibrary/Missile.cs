using SF.Space;

namespace SF.ServerLibrary
{
    using System;

    public class Missile : IMissile
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

        public bool Exploded { get; private set; }
        public bool Exhausted { get; private set; }

        public bool IsDead
        {
            get
            {
                return Exhausted || Exploded;
            }
        }

        public Vector S { get; private set; }
        public Vector V { get; private set; }

        public Vector A
        {
            get
            {
                return this.Class.Acceleration * Vector.Direction(heading);
            }
        }

        public readonly double Started;

        private double t0;
        private Vector v0;
        private Vector s0;
        private double heading;

        public Missile(IShip from, bool left, IShip to, int number, TimeSpan time)
        {
            this.Class = from.Missle;
            this.s0 = this.S = from.S;
            this.v0 = this.V = from.V;
            this.t0 = this.Started = time.TotalSeconds;
            this.Number = Math.Min(number, from.Missles);
            this.heading = Math.IEEERemainder(from.Heading + (left ? -Math.PI / 2 : Math.PI / 2), 2 * Math.PI);
            Target = to;
        }

        public void UpdateTime(double time)
        {
            if (time - this.Started > this.Class.FlyTime)
            {
                Exhausted = true;
                return;
            }
            var t = time - this.t0;
            var t2 = t * t / 2;
            this.V = this.v0 + A * t;
            this.S = this.s0 + this.v0 * t + this.A * t2;
            var s = Target.S - this.S;
            var v = Target.V - this.V;
            if (s.Length < Class.HitDistance)
            {
                Exploded = true;
                return;
            }
            if (t < this.Class.Targeting)
                return;
            this.v0 = this.V;
            this.s0 = this.S;
            this.t0 = time;
            this.heading = s.Argument;
            //if (MathUtils.NearlyEqual(h, this.heading))
            //    return;
            //var diff = this.heading - h;
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
