using System;

namespace SF.Space
{
    public interface IParticle
    {
        Guid Id { get; }
        string Nation { get; }
        string Name { get; }
        double Weight { get; }
        double Radius { get; }
        Vector Position { get; }
        Vector Speed { get; }
        Vector Acceleration { get; }
    }
}
