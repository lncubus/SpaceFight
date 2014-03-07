using System;
using SF.Space;

namespace SF.ServerLibrary
{
    public class ServerShip : Ship
    {
        private bool initialized = false;

        /// <summary>
        /// Roll angle.
        /// </summary>
        private LinearValue roll;

        /// <summary>
        /// Heading angle.
        /// </summary>
        private LinearValue heading;

        /// <summary>
        /// Acceleration.
        /// </summary>
        private LinearValue thrust;

        private double t0;
        private Vector v0;

        public ServerShip(PermanentShipData def)
            : base(def)
        {
        }

        public void Move(double time, double dt)
        {
            if (VolatileShip == null || ControlShip == null)
                return;
            if (!initialized)
            {
                InternalInitialize(time);
                return;
            }
            InternalMove(time, dt);
        }

        private void InternalMove(double time, double dt)
        {
            if (!MathUtils.NearlyEqual(RollTo, roll.ToValue))
                roll.Set(time, RollTo);
            bool changed =
                !MathUtils.NearlyEqual(ThrustTo, thrust.ToValue) ||
                !MathUtils.NearlyEqual(HeadingTo, heading.ToValue) ||
                thrust.WillReset(time) ||
                heading.WillReset(time);
            var t = time - t0;
            var t2 = t * t / 2;
            var a = thrust.FromValue;
            var phi = heading.FromValue;
            var AS = thrust.Direction * thrust.Speed;
            var omega = heading.Direction * heading.Speed;
            var om2 = omega * omega;
            var a1 = a + AS * t;
            var phi1 = phi + omega * t;
            var v1 = VolatileShip.Speed;
            if (heading.Direction == 0)
            {
                // no rotation, linear acceleration
                // a = a_0 + A*t
                VolatileShip.Speed = v0 + (a * t + AS * t2) * Vector.Direction(phi);
            }
            else if (thrust.Direction == 0)
            {
                // constant acceleration, rotating ship
                // integrated by Wolfram Aloha
                VolatileShip.Speed = v0 + (a / omega) * new Vector(Math.Cos(phi) - Math.Cos(phi1), -Math.Sin(phi) + Math.Sin(phi1));
            }
            else
            {
                // rotating and powering
                // running Archimedean spiral
                // integrated by Wolfram Aloha
                var dVx = (-a1 * Math.Cos(phi1) + a * Math.Cos(phi)) / omega + AS * (Math.Sin(phi1) - Math.Sin(phi)) / om2;
                var dVy = (a1 * Math.Sin(phi1) - a * Math.Sin(phi)) / omega + AS * (Math.Cos(phi1) - Math.Cos(phi)) / om2;
                VolatileShip.Speed = v0 + new Vector(dVx, dVy);
            }
            // I failed to integrate position properly
            // so we'll use trapezium rule
            VolatileShip.Position += (VolatileShip.Speed + v1) * (dt / 2);
            if (changed)
            {
                thrust.Set(time, ThrustTo);
                heading.Set(time, HeadingTo);
                v0 = VolatileShip.Speed;
                t0 = time;
            }
            VolatileShip.Thrust = thrust.Get(time);
            VolatileShip.Heading = heading.Get(time);
            VolatileShip.Roll = roll.Get(time);
        }

        private void InternalInitialize(double time)
        {
            roll = new LinearValue(time, Roll, RollTo, 2 * Math.PI/Class.RoundRollTime, 2*Math.PI);
            heading = new LinearValue(time, Heading, HeadingTo, 2*Math.PI/Class.FullTurnTime, 2*Math.PI);
            thrust = new LinearValue(time, Thrust, ThrustTo, Class.MaximumAcceleration/Class.FullAccelerationTime, 0);
            v0 = Speed;
            t0 = time;
            initialized = true;
        }
    }
}
