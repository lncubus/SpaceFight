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

        public static MissleDefinition Store(IMissile missile)
        {
            return new MissleDefinition
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
