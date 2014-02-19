using System.Runtime.Serialization;

namespace SF.Space
{
    [DataContract]
    public class VolatileMissileData : IParticle
    {
        [DataMember]
        public int Id { get; set; }
        [DataMember]
        public Vector Position { get; set; }
        [DataMember]
        public Vector Speed { get; set; }
        [DataMember]
        public Vector Acceleration { get; set; }
    }
}
