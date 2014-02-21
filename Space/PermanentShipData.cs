using System.Runtime.Serialization;
using System.Xml.Serialization;

namespace SF.Space
{


    [DataContract]
    public class PermanentShipData : INationObject, INamed
    {
        [DataMember]
        public int Id { get; set; }
        [DataMember]
        public string Name { get; set; }
        [DataMember]
        public int IdClass { get; set; }
        [DataMember]
        public int IdNation { get; set; }
        [XmlIgnore]
        [IgnoreDataMember]
        public Nation Nation { get; set; }
    }
}
