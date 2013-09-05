using System;
using SF.Space;

namespace SF.ServerLibrary
{
    public class Collider
    {
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
    }
}
