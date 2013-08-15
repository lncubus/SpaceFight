using System.Runtime.Serialization;

namespace SF.Space
{
    [DataContract]
    public class MissileDefinition
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

        public static MissileDefinition Store(IMissile missile)
        {
            return new MissileDefinition
            {
                ClassName = missile.Class.Name,
                Nation = missile.Nation,
                Acceleration = missile.A,
                Position = missile.S,
                Speed = missile.V,
            };
        }
    }
}
