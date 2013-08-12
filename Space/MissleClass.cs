using System.Runtime.Serialization;

namespace SF.Space
{
    [DataContract]
    public class MissleClass
    {
        [DataMember]
        public string Nation { get; set; }
        [DataMember]
        public string Name { get; set; }
        [DataMember]
        public double MaximumAcceleration { get; set; }
        [DataMember]
        public double FlyTime { get; set; }
        [DataMember]
        public double Damage { get; set; }
        [DataMember]
        public double JammerResistance { get; set; }
    }
}
