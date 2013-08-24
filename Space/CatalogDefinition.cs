using System.Linq;
using System.Runtime.Serialization;
using System.Xml.Serialization;

namespace SF.Space
{
    [DataContract]
    [XmlRoot("Catalog")]
    public class CatalogDefinition
    {
        [DataMember]
        public double DefaultScale { get; set; }
        [DataMember]
        public double MaximumMissileRange { get; set; }
        [DataMember]
        public double ThroatAngle { get; set; }
        [DataMember]
        public double SkirtAngle { get; set; }
        [DataMember]
        public ShipClass[] ShipClasses { get; set; }
        [DataMember]
        public MissileClass[] MissileClasses { get; set; }
    }
}
