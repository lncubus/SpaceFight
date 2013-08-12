using System.Runtime.Serialization;

namespace SF.Space
{
    [DataContract]
    public class MissleDefinition
    {
        [DataMember]
        public string ClassName;
        [DataMember]
        public string Nation;
        [DataMember]
        public Vector Acceleration;
        [DataMember]
        public Vector Position;
        [DataMember]
        public Vector Speed;

        public static MissleDefinition Store(IMissle missle)
        {
            return new MissleDefinition
            {
                ClassName = missle.Class.Name,
                Nation = missle.Nation,
                Acceleration = missle.A,
                Position = missle.S,
                Speed = missle.V,
            };
        }
    }
}
