
namespace SF.Space
{
    public interface IShip : IParticle
    {
        ShipClass Class { get; }
        string ClassName { get; }

        MissileClass Missile { get; }
        string MissileName { get; }
        int Missiles { get; }

        ShipState State { get; }

        double Heading { get; }
        double Roll { get; }
        double Thrust { get; }
    }

    public enum ShipState
    {
        Normal = 0,
        Drifting,
        Sailing,
        Hyperspace,
        Junk,
        Annihilated,
    }
}
