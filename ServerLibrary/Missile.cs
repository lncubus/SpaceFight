using System.IO;
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

        public IShip Target { get; private set; }
        public IShip Launcher { get; private set; }

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
            Launcher = from;
        }

        public void UpdateTime(double time)
        {
            if (time - Started > Class.FlyTime)
            {
                Exhausted = true;
                return;
            }
            var t = time - t0;
            Speed = v0 + Acceleration*t;
            Position = s0 + v0*t + Acceleration*t*t/2;
            if (t < Class.Targeting)
                return;
            v0 = Speed;
            s0 = Position;
            t0 = time;
//            heading = s.Argument;
            var s = Target.Position - Position;
            var v = Target.Speed - Speed;
            var a = Target.Acceleration;
            var h = s.Argument;
            double eta = 0;
            var h1 = Vector.Direction(h);
            var a1 = a*h1 - Class.Acceleration;
            //for (int i = 0; i < 3; i++)
            //{
            //    // this can't happen
            //    if (MathUtils.NearlyEqual(a1, 0))
            //        break;
            var va1 = v*h1/a1;
            var sa1 = s*h1/a1;
            //    // t^2 + 2 va1 t + 2 sa1
            var d = va1*va1 - 2*sa1;
            if (d < 0)
                return;
            d = Math.Sqrt(d);
            eta = va1 >= d ? va1 - d : va1 + d;
            var h2 = h1.Rotate(Math.PI/2);
            var full = s + (v*h2*eta + a*h2*eta*eta/2)*h2;
            h = full.Argument;
            //}
            heading = h;
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
