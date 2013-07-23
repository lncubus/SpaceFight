using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SF.Space
{
    public interface IShipClass
    {
        string Nation { get; }
        string Name { get; }
        double MaximumAcceleration { get; }
        double FullAccelerationTime { get; }
        double RoundRollTime { get; }
        double FullTurnTime { get; }
        double MissleRange { get; }
    }
}
