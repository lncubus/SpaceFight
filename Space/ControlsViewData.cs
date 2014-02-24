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

        //[DataMember]
        //[XmlArrayItem("Missile")]
        //public ControlMissileData[] Missiles { get; set; }
    }
}
