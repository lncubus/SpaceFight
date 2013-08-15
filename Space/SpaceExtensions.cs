using System;

namespace SF.Space
{
    public static class SpaceExtensions
    {
        public const double MaximumMissileRange = 10000000;
        public const double ThroatAngle = 45 * Math.PI / 180;
        public const double SkirtAngle = 15 * Math.PI / 180;

        /// <summary>
        /// Opened board part.
        /// </summary>
        /// <param name="ship"></param>
        /// <returns>absolute cosie of roll angle</returns>
        public static double Board(this IShip ship)
        {
            return Math.Abs(Math.Cos(ship.Roll));
        }

        public static double MissileRange(this IShip ship)
        {
            if (string.IsNullOrEmpty(ship.MissileName))
                return 0;
            if (ship.Missile == null)
                return MaximumMissileRange;
            var t = ship.Missile.FlyTime;
            return ship.Missile.Acceleration * t * t / 2;
        }
    }
}
