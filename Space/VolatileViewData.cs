using System;
using System.Runtime.Serialization;
using System.Xml.Serialization;

namespace SF.Space
{

    [DataContract]
    public class VolatileViewData
    {
        [DataMember]
        public TimeSpan Time { get; set; }

        [DataMember]
        [XmlArrayItem("Ship")]
        public VolatileShipData[] Ships { get; set; }

        [DataMember]
        [XmlArrayItem("Missile")]
        public Missile[] Missiles { get; set; }
    }
}
