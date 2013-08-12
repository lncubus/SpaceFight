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
        public ShipClass[] ShipClasses { get; set; }
        [DataMember]
        public MissleClass[] MissleClasses { get; set; }
    }
}
