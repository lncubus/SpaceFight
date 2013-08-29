using System;
using SF.Space;

namespace SF.ServerLibrary
{
    public class Collider
    {
        public const double Megatonn = 4184; // 4.184 E 15, Дж = кг м2 с-2 -> 1E-9 т км2 с-2  

        public bool HaveCollision(IParticle missile, IParticle target, double dt, double hitDistance = 0)
        {
            var S = missile.Position - target.Position;
            var V = missile.Speed - target.Speed;
            var r = missile.Radius + target.Radius + hitDistance;
            // Collision at t = 0
            if (S.Length <= r)
                return true;
            var Vx2Vy2 = V.SquareLength;
            var VxSxVySy = V * S;
            // No collision within time interval
            if (Vx2Vy2*dt <= Math.Abs(VxSxVySy))
                return false;
            // Potential collision time
            var t = -VxSxVySy / Vx2Vy2;
            var s = S + V * t;
            // Was that near enough?
            return s.Length <= r;
        }

        public double PowerOfCollision(IParticle missile, IParticle target)
        {
            var v2 = (missile.Speed - target.Speed).SquareLength; 
            return (missile.Weight * v2 / 2) / Megatonn;
        }
    }
}
