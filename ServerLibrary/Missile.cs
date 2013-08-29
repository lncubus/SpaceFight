using SF.Space;

namespace SF.ServerLibrary
{
    using System;

    public class Missile : IMissile
    {
        public Guid Id { get; set; }

        public MissileClass Class { get; set; }
        public string Nation { get; set; }

        private string m_className;
        public string ClassName
        {
            get { return Class == null ? m_className : Class.Name; }
            set { m_className = value; }
        }

        public IShip Target { get; set; }

        public int Number { get; set; }

        public bool Exploded { get; set; }
        public bool Exhausted { get; set; }

        public bool IsDead
        {
            get
            {
                return Exhausted || Exploded;
            }
        }

        public Vector Position { get; private set; }
        public Vector Speed { get; private set; }

        public Vector Acceleration
        {
            get { return Class.Acceleration * Vector.Direction(heading); }
        }

        public readonly double Started;

        private double t0;
        private Vector v0;
        private Vector s0;
        private double heading;

        public Missile(IShip from, bool left, IShip to, int number, TimeSpan time)
        {
            Id = Guid.NewGuid();
            Class = from.Missile;
            s0 = Position = from.Position;
            v0 = Speed = from.Speed;
            t0 = Started = time.TotalSeconds;
            Number = Math.Min(number, from.Missiles);
            heading = Math.IEEERemainder(from.Heading + (left ? -Math.PI / 2 : Math.PI / 2), 2 * Math.PI);
            Target = to;
        }

        public void UpdateTime(double time)
        {
            if (time - Started > Class.FlyTime)
            {
                Exhausted = true;
                return;
            }
            var t = time - t0;
            var t2 = t * t / 2;
            Speed = v0 + Acceleration * t;
            Position = s0 + v0 * t + Acceleration * t2;
            var s = Target.Position - Position;
            var v = Target.Speed - Speed;
            if (t < Class.Targeting)
                return;
            v0 = Speed;
            s0 = Position;
            t0 = time;
            heading = s.Argument;
            //if (MathUtils.NearlyEqual(h, heading))
            //    return;
            //var diff = heading - h;
            //// end of the current arc
            //bool changed = (headingTo.HasValue || accelerateTo.HasValue || Acceleration.WillReset(time) || Heading.WillReset(time));
            //if (changed)
            //{
            //    v0 = V;
            //    t0 = time;
            //    Heading.Set(time, HeadingTo);
            //    Acceleration.Set(time, AccelerateTo);
            //    headingTo = null;
            //    accelerateTo = null;
            //}
            //AccelerationValue = a1;
            //HeadingValue = phi1;
        }

        public string Name
        {
            get { return ClassName; }
        }

        public double Weight
        {
            get { return Class.Weight; }
        }

        public double Radius
        {
            get { return 0; }
        }
    }
}
