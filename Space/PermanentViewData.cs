using System;
using System.Runtime.Serialization;
using System.Xml.Serialization;

namespace SF.Space
{
    [DataContract]
    public class PermanentViewData
    {
        [DataMember]
        public int Generation { get; set; }
        [DataMember]
        public TimeSpan Time { get; set; }

        [DataMember]
        public ConstantData Constants { get; set; }
        [DataMember]
        [XmlArrayItem("Nation")]
        public Nation[] Nations { get; set; }
        [DataMember]
        [XmlArrayItem("Star")]
        public Star[] Stars { get; set; }
        [DataMember]
        [XmlArrayItem("ShipClass")]
        public ShipClass[] ShipClasses { get; set; }
        [DataMember]
        [XmlArrayItem("MissileClass")]
        public MissileClass[] MissileClasses { get; set; }
        [DataMember]
        [XmlArrayItem("Ship")]
        public PermanentShipData[] Ships { get; set; }
    }
}
