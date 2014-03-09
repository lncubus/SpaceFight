using System.Runtime.Serialization;
using System.Xml.Serialization;

namespace SF.Space
{
    [DataContract]
    public class MissileRack
    {
        [DataMember]
        public int IdMissileClass { get; set; }
        [DataMember]
        public int Count { get; set; }
        [XmlIgnore]
        [IgnoreDataMember]
        public MissileClass MissileClass { get; set; }
    }
}
