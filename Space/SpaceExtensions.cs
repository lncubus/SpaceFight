using System;
using System.Collections.Generic;
using System.Linq;

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

        public static double Radius(this IParticle particle)
        {
            var star = particle as Star;
            if (star != null)
                return star.Radius;
            return 0;
        }

        public static void ApplyNations(this IEnumerable<INationObject> objects, IDictionary<int, Nation> nations)
        {
            foreach (var INationObject in objects)
            {
                int id = INationObject.IdNation;
                INationObject.Nation = id == 0 ? null : nations[id];
            }
        }

        //public static double MissileRange(this IShip ship)
        //{
        //    //if (string.IsNullOrEmpty(ship.MissileName))
        //    //    return 0;
        //    //if (ship.Missile == null)
        //    //    return Catalog.Instance.MaximumMissileRange;
        //    //var t = ship.Missile.FlyTime;
        //    //return ship.Missile.Acceleration * t * t / 2;
        //}

        public static T ById<T>(this IDictionary<int, T> particles, int id)
        {
            if (id == 0)
                return default(T);
            T result;
            particles.TryGetValue(id, out result);
            return result;
        }

        //public static bool IsLight(this IShip ship)
        //{
        //    return ship.Class.Superclass == ShipSuperclass.LAC;
        //}

        //public static bool InSpace(this IHelm ship)
        //{
        //    return ship.State != ShipState.Annihilated && ship.State != ShipState.Hyperspace && string.IsNullOrEmpty(ship.Carrier);
        //}

        public static bool IsDead(this IShip ship)
        {
            return ship.HealthRate <= 0;
        }

        public static bool IsLeft(this IShip helm, IParticle target)
        {
            return Math.Sin((target.Position - helm.Position).Argument - helm.Heading) < 0;
        }

        public static bool IsLeftBoard(this IShip helm, IParticle target)
        {
            var left = helm.IsLeft(target);
            if (Math.Cos(helm.Roll) < 0)
                left = !left;
            return left;
        }
    }
}
