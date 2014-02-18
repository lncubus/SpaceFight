namespace SF.Space
{
    public interface IParticle
    {
        int Id { get; }
        Vector Position { get; }
        Vector Speed { get; }
        Vector Acceleration { get; }
    }
}
