using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SF.Space
{
    public static class SpaceExtensions
    {
        /// <summary>
        /// Opened board part.
        /// </summary>
        /// <param name="ship"></param>
        /// <returns>absolute cosie of roll angle</returns>
        public static double Board(this IShip ship)
        {
            return Math.Abs(Math.Cos(ship.Roll));
        }
    }
}
