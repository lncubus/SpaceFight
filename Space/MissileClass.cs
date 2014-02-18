using System.Runtime.Serialization;

namespace SF.Space
{
    [DataContract]
    public class MissileClass : NationObject, INamed
    {
        [DataMember]
        public int Id { get; set; }
        [DataMember]
        public string Name { get; set; }
        [DataMember]
        public double Acceleration { get; set; }
        [DataMember]
        public double FlyTime { get; set; }
        [DataMember]
        public double Targeting { get; set; }
        [DataMember]
        public double HitDistance { get; set; }
    }
}
