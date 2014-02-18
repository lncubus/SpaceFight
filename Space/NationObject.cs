using System.Runtime.Serialization;
using System.Xml.Serialization;

namespace SF.Space
{
    [DataContract]
    public class NationObject
    {
        [DataMember]
        public int IdNation { get; set; }
        [XmlIgnore]
        [IgnoreDataMember]
        public Nation Nation { get; set; }
    }
}
