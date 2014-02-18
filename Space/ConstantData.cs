using System.Runtime.Serialization;

namespace SF.Space
{
    public class ConstantData
    {
        [DataMember]
        public double DefaultScale { get; set; }
        [DataMember]
        public double MaximumMissileRange { get; set; }
        [DataMember]
        public double DefaultThroatAngle { get; set; }
        [DataMember]
        public double DefaultSkirtAngle { get; set; }
        [DataMember]
        public double DefaultCarrierRange { get; set; }
    }
}
