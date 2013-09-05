using System.Runtime.Serialization;

namespace SF.Space
{
    [DataContract]
    public class ShipClass
    {
        [DataMember]
        public ShipSuperclass Superclass { get; set; }
        [DataMember]
        public string Nation { get; set; }
        [DataMember]
        public string Name { get; set; }
        [DataMember]
        public double MaximumAcceleration { get; set; }
        [DataMember]
        public double FullAccelerationTime { get; set; }
        [DataMember]
        public double RoundRollTime { get; set; }
        [DataMember]
        public double FullTurnTime { get; set; }
        [DataMember]
        public double Weight { get; set; }
        [DataMember]
        public double Wedge { get; set; }
    }

    public enum ShipSuperclass
    {
        LAC = 0,
        CLAC = 11,
    }
}
