using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;

namespace SF.Space
{
    internal class Dynamics
    {
        public const double TimeEpsilon = 1E-6; // 86 ms

        private sealed class LinearValue
        {
            private int direction;
            private double fromValue;
            private double toValue;
            private double fromTime;
            private double toTime;
            private readonly double speed;
            private readonly double modulo;

            public int Direction
            {
                get { return direction; }
            }

            public double ToValue
            {
                get { return toValue; }
            }

            public double FromValue
            {
                get { return fromValue; }
            }

            public double Speed
            {
                get { return speed; }
            }

            public LinearValue(double t, double Value, double ValueTo, double Speed, double Mod)
            {
                modulo = Mod;
                speed = Speed;
                Reset(t, Value);
                Set(t, ValueTo);
            }

            public void Reset(double t, double val)
            {
                direction = 0;
                fromTime = t;
                fromValue = val;
                toTime = double.PositiveInfinity;
                toValue = val;
            }

            public bool WillReset(double t)
            {
                if (double.IsInfinity(toTime) || double.IsNaN(toTime))
                    return false;
                if (t + TimeEpsilon >= toTime)
                    return true;
                return false;
            }

            public double Get(double t)
            {
                if (double.IsInfinity(toTime) || double.IsNaN(toTime))
                    return toValue;
                if (WillReset(t))
                {
                    Reset(t, toValue);
                    return toValue;
                }
                return fromValue + direction * speed * (t - fromTime);
            }

            public void Set(double t, double goesTo)
            {
                double currentValue = Get(t);
                double diff = goesTo - currentValue;
                direction = diff < 0 ? -1 : 1;
                diff = Math.Abs(diff);
                if (modulo > 0)
                {
                    if (diff > modulo)
                        diff -= Math.Floor(diff / modulo) * modulo;
                    if (diff > modulo / 2)
                    {
                        diff = modulo - diff;
                        direction = -direction;
                    }
                }
                double tau = diff / speed;
                if (tau <= TimeEpsilon)
                    Reset(t, goesTo);
                else
                {
                    fromTime = t;
                    fromValue = currentValue;
                    toTime = t + tau;
                    toValue = goesTo;
                }
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
            get { return headingTo ?? Heading.ToValue; }
            set { headingTo = value; }
        }

        public double AccelerateTo
        {
            get { return accelerateTo ?? Acceleration.ToValue; }
            set { accelerateTo = value; }
        }

        public double RollTo
        {
            get { return rollTo ?? Roll.ToValue; }
            set { rollTo = value; }
        }

        public double AccelerationValue { get; private set; }
        public double HeadingValue { get; private set; }
        public double RollValue { get; private set; }

        public Vector A
        {
            get
            {
                return AccelerationValue * Vector.Direction(HeadingValue);
            }
        }

        public Vector V { get; private set; }

        public Vector S { get; private set; }

        public Dynamics(IShipClass Class, SpaceShip Def, TimeSpan time)
        {
            t1 = t0 = time.TotalSeconds;
            S = Def.Position;
            V = v0 = Def.Speed;
            Roll = new LinearValue(t0, Def.Roll, Def.RollTo, 2 * Math.PI / Class.RoundRollTime, 2 * Math.PI);
            Heading = new LinearValue(t0, Def.Heading, Def.HeadingTo, 2 * Math.PI / Class.FullTurnTime, 2 * Math.PI);
            Acceleration = new LinearValue(t0, Def.Acceleration, Def.AccelerateTo, Class.MaximumAcceleration / Class.FullAccelerationTime, 0);
            AccelerationValue = Acceleration.FromValue;
            HeadingValue = Heading.FromValue;
            RollValue = Roll.FromValue;
        }

        public void UpdateTime(double time)
        {
            if (rollTo.HasValue)
            {
                Roll.Set(time, rollTo.Value);
                rollTo = null;
            }
            RollValue = Roll.Get(time);

            var t = time - t0;
            var t2 = t*t / 2;
            var a = Acceleration.FromValue;
            var phi = Heading.FromValue;
            var AS = Acceleration.Direction * Acceleration.Speed;
            var omega = Heading.Direction * Heading.Speed;
            var om2 = omega * omega;
            var a1 = a + AS * t;
            var phi1 = phi + omega * t;
            var v1 = V;
            if (Heading.Direction == 0)
            {
                // no rotation, linear acceleration
                // a = a_0 + A*t
                V = v0 + (a * t + AS * t2) *  Vector.Direction(phi);
            }
            else if (Acceleration.Direction == 0)
            {
                // constant acceleration, rotating ship
                // integrated by Wolfram Aloha
                V = v0 + (a / omega) * new Vector(Math.Cos(phi) - Math.Cos(phi1), -Math.Sin(phi) + Math.Sin(phi1));
            }
            else
            {
                // rotating and powering
                // running Archimedean spiral
                // integrated by Wolfram Aloha
                var dVx = (-a1 * Math.Cos(phi1) + a * Math.Cos(phi)) / omega + AS * (Math.Sin(phi1) - Math.Sin(phi)) / om2;
                var dVy = (a1 * Math.Sin(phi1) - a * Math.Sin(phi)) / omega  + AS * (Math.Cos(phi1) - Math.Cos(phi)) / om2;
                V = v0 + new Vector(dVx, dVy);
            }
            // I failed to integrate position properly
            // so we'll use trapezium rule
            var dt = time - t1;
            S = S + (V + v1) * (dt / 2);
            t1 = time;
            // end of the current arc
            bool changed = (headingTo.HasValue || accelerateTo.HasValue || Acceleration.WillReset(time) || Heading.WillReset(time));
            if (changed)
            {
                v0 = V;
                t0 = time;
                Heading.Set(time, HeadingTo);
                Acceleration.Set(time, AccelerateTo);
                headingTo = null;
                accelerateTo = null;
            }
            AccelerationValue = a1;
            HeadingValue = phi1;
        }
    }
}
