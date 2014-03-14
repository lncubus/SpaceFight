using System.Runtime.Serialization;
using System.Xml.Serialization;

namespace SF.Space
{
    [DataContract]
    public class ControlsViewData
    {
        [DataMember]
        [XmlArrayItem("Ship")]
        public ControlShipData[] Ships { get; set; }

        [DataMember]
        [XmlArrayItem("Missile")]
        public MissileControl[] Missiles { get; set; }
    }
}
