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

        public ServerShip(PermanentShipData def)
            : base(def)
        {
        }

        public void Move(double t, double dt)
        {
            if (VolatileShip == null || ControlShip == null)
                return;
            if (!initialized)
                Initialize(t);

        }

        private void Initialize(double time)
        {
            roll = new LinearValue(time, Roll, RollTo, 2 * Math.PI/Class.RoundRollTime, 2*Math.PI);
            heading = new LinearValue(time, Heading, HeadingTo, 2*Math.PI/Class.FullTurnTime, 2*Math.PI);
            thrust = new LinearValue(time, Thrust, ThrustTo, Class.MaximumAcceleration/Class.FullAccelerationTime, 0);
        }
    }
}
