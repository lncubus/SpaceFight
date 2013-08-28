namespace SF.Space
{
    public interface IMissile : IParticle
    {
        MissileClass Class { get; }
        string ClassName { get; }
        bool IsDead { get; }
    }
}
