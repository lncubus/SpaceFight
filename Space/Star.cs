using System.Runtime.Serialization;

namespace SF.Space
{
    [DataContract]
    public class Star
    {
        [DataMember]
        public string Nation;
        [DataMember]
        public string Name;
        [DataMember]
        public double Radius;
        [DataMember]
        public Vector Position;
        [DataMember]
        public Vector Speed;
        [DataMember]
        public StarType StarClass;
    }

    public enum StarType
    {
        Planet = 0,
        Habitable = 1,
        Inhabited = 3,
        Gas = 4,
        Star = 8,
    }
}
