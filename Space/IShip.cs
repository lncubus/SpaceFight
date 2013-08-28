
namespace SF.Space
{
    public interface IShip : IParticle
    {
        ShipClass Class { get; }
        string ClassName { get; }

        MissileClass Missile { get; }
        string MissileName { get; }
        int Missiles { get; }

        double Heading { get; }
        double Roll { get; }
        double Thrust { get; }
    }
}
