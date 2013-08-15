namespace SF.Space
{
    public interface IMissile
    {
        MissileClass Class { get; }
        string Nation { get; }
        string ClassName { get; }
        bool IsDead { get; }

        Vector S { get; }
        Vector V { get; }
        Vector A { get; }

    }
}
