using System;
namespace SF.Space
{
    public interface IMissle
    {
        MissleClass Class { get; }
        string Nation { get; }
        string ClassName { get; }

        Vector S { get; }
        Vector V { get; }
        Vector A { get; }
    }
}
