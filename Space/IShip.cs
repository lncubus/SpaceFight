namespace SF.Space
{
    public interface IShip : IParticle
    {
        double Heading { get; }
        double Roll { get; }
        double Thrust { get; }
        double HealthRate { get; }
    }
}
