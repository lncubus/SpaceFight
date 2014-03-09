using System.Runtime.Serialization;
using System.Xml.Serialization;

namespace SF.Space
{
    [DataContract]
    public class ShipClass : INationObject, INamed
    {
        [DataMember]
        public int Id { get; set; }
        [DataMember]
        public ShipType Superclass { get; set; }
        [DataMember]
        public int IdNation { get; set; }
        [XmlIgnore]
        [IgnoreDataMember]
        public Nation Nation { get; set; }
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
        [DataMember]
        public double RechargeTime { get; set; }
        [DataMember]
        public int Missiles { get; set; }
        [DataMember]
        public MissileRack[] Right { get; set; }
        [DataMember]
        public MissileRack[] Left { get; set; }
    }
}
