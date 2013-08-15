
namespace SF.Space
{
    public interface IShip
    {
        ShipClass Class { get; }
        string Nation { get; }
        string Name { get; }
        string ClassName { get; }

        MissleClass Missle { get; }
        string MissleName { get; }
        int Missles { get; }

        double Heading { get; }
        double Roll { get; }
        double Acceleration { get; }

        Vector S { get; }
        Vector V { get; }
        Vector A { get; }
    }
}
