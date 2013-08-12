using System;
using SF.Space;

namespace SF.ServerLibrary
{
    public class Dynamics
    {
        public const double TimeEpsilon = 1E-6; // 86 ms

        private sealed class LinearValue
        {
            private double m_fromTime;
            private double m_toTime;

            public int Direction { get; private set; }

            public double ToValue { get; private set; }

            public double FromValue { get; private set; }

            public double Speed { get; private set; }

            public double Modulo { get; private set; }

            public LinearValue(double t, double Value, double ValueTo, double Speed, double Mod)
            {
                this.Modulo = Mod;
                this.Speed = Speed;
                this.Reset(t, Value);
                this.Set(t, ValueTo);
            }

            public bool WillReset(double t)
            {
                if (double.IsInfinity(this.m_toTime) || double.IsNaN(this.m_toTime))
                    return false;
                if (t + TimeEpsilon >= this.m_toTime)
                    return true;
                return false;
            }

            public double Get(double t)
            {
                if (double.IsInfinity(this.m_toTime) || double.IsNaN(this.m_toTime))
                    return this.ToValue;
                if (this.WillReset(t))
                {
                    this.Reset(t, this.ToValue);
                    return this.ToValue;
                }
                return this.FromValue + this.Direction * this.Speed * (t - this.m_fromTime);
            }

            public void Set(double t, double goesTo)
            {
                double currentValue = this.Get(t);
                double diff = goesTo - currentValue;
                this.Direction = diff < 0 ? -1 : 1;
                diff = Math.Abs(diff);
                if (this.Modulo > 0)
                {
                    if (diff > this.Modulo)
                        diff -= Math.Floor(diff / this.Modulo) * this.Modulo;
                    if (diff > this.Modulo / 2)
                    {
                        diff = this.Modulo - diff;
                        this.Direction = -this.Direction;
                    }
                }
                double tau = diff / this.Speed;
                if (tau <= TimeEpsilon)
                    this.Reset(t, goesTo);
                else
                {
                    this.m_fromTime = t;
                    this.FromValue = currentValue;
                    this.m_toTime = t + tau;
                    this.ToValue = goesTo;
                }
            }

            private void Reset(double t, double val)
            {
                this.Direction = 0;
                this.m_fromTime = t;
                this.FromValue = val;
                this.m_toTime = double.PositiveInfinity;
                this.ToValue = val;
            }
        }

        /// <summary>
        /// v_0 - the original speed
        /// </summary>
        private Vector v0;
        /// <summary>
        /// t_0 - time for the origin
        /// </summary>
        private double t0;
        /// <summary>
        /// t_1  - time for previous point
        /// </summary>
        private double t1;

        /// <summary>
        /// Roll angle.
        /// This is public independent value.
        /// </summary>
        private readonly LinearValue Roll;

        /// <summary>
        /// Heading angle.
        /// </summary>
        private readonly LinearValue Heading;

        /// <summary>
        /// Acceleration.
        /// </summary>
        private readonly LinearValue Acceleration;

        private double? headingTo;
        private double? rollTo;
        private double? accelerateTo;

        public double HeadingTo
        {
            get { return this.headingTo ?? this.Heading.ToValue; }
            set { this.headingTo = value; }
        }

        public double AccelerateTo
        {
            get { return this.accelerateTo ?? this.Acceleration.ToValue; }
            set { this.accelerateTo = value; }
        }

        public double RollTo
        {
            get { return this.rollTo ?? this.Roll.ToValue; }
            set { this.rollTo = value; }
        }

        public double AccelerationValue { get; private set; }
        public double HeadingValue { get; private set; }
        public double RollValue { get; private set; }

        public Vector A
        {
            get
            {
                return this.AccelerationValue * Vector.Direction(this.HeadingValue);
            }
        }

        public Vector V { get; private set; }

        public Vector S { get; private set; }

        public Dynamics(ShipClass Class, HelmDefinition Def, TimeSpan time)
        {
            this.t1 = this.t0 = time.TotalSeconds;
            this.S = Def.Position;
            this.V = this.v0 = Def.Speed;
            this.Roll = new LinearValue(this.t0, Def.Roll, Def.RollTo, 2 * Math.PI / Class.RoundRollTime, 2 * Math.PI);
            this.Heading = new LinearValue(this.t0, Def.Heading, Def.HeadingTo, 2 * Math.PI / Class.FullTurnTime, 2 * Math.PI);
            this.Acceleration = new LinearValue(this.t0, Def.Acceleration, Def.AccelerateTo, Class.MaximumAcceleration / Class.FullAccelerationTime, 0);
            this.AccelerationValue = this.Acceleration.FromValue;
            this.HeadingValue = this.Heading.FromValue;
            this.RollValue = this.Roll.FromValue;
        }

        public void UpdateTime(double time)
        {
            if (this.rollTo.HasValue)
            {
                this.Roll.Set(time, this.rollTo.Value);
                this.rollTo = null;
            }
            this.RollValue = this.Roll.Get(time);

            var t = time - this.t0;
            var t2 = t*t / 2;
            var a = this.Acceleration.FromValue;
            var phi = this.Heading.FromValue;
            var AS = this.Acceleration.Direction * this.Acceleration.Speed;
            var omega = this.Heading.Direction * this.Heading.Speed;
            var om2 = omega * omega;
            var a1 = a + AS * t;
            var phi1 = phi + omega * t;
            var v1 = this.V;
            if (this.Heading.Direction == 0)
            {
                // no rotation, linear acceleration
                // a = a_0 + A*t
                this.V = this.v0 + (a * t + AS * t2) *  Vector.Direction(phi);
            }
            else if (this.Acceleration.Direction == 0)
            {
                // constant acceleration, rotating ship
                // integrated by Wolfram Aloha
                this.V = this.v0 + (a / omega) * new Vector(Math.Cos(phi) - Math.Cos(phi1), -Math.Sin(phi) + Math.Sin(phi1));
            }
            else
            {
                // rotating and powering
                // running Archimedean spiral
                // integrated by Wolfram Aloha
                var dVx = (-a1 * Math.Cos(phi1) + a * Math.Cos(phi)) / omega + AS * (Math.Sin(phi1) - Math.Sin(phi)) / om2;
                var dVy = (a1 * Math.Sin(phi1) - a * Math.Sin(phi)) / omega  + AS * (Math.Cos(phi1) - Math.Cos(phi)) / om2;
                this.V = this.v0 + new Vector(dVx, dVy);
            }
            // I failed to integrate position properly
            // so we'll use trapezium rule
            var dt = time - this.t1;
            this.S = this.S + (this.V + v1) * (dt / 2);
            this.t1 = time;
            // end of the current arc
            bool changed = (this.headingTo.HasValue || this.accelerateTo.HasValue || this.Acceleration.WillReset(time) || this.Heading.WillReset(time));
            if (changed)
            {
                this.v0 = this.V;
                this.t0 = time;
                this.Heading.Set(time, this.HeadingTo);
                this.Acceleration.Set(time, this.AccelerateTo);
                this.headingTo = null;
                this.accelerateTo = null;
            }
            this.AccelerationValue = a1;
            this.HeadingValue = phi1;
        }
    }
}
