using System.Runtime.Serialization;
using System.Xml.Serialization;

namespace SF.Space
{
    [DataContract]
    public class MissileControl
    {
        [DataMember]
        public int Id { get; set; }
        [IgnoreDataMember]
        [XmlIgnore]
        public Missile Arrow { get; set; }
        [IgnoreDataMember]
        [XmlIgnore]
        public IShip Target { get; set; }
        [DataMember]
        public int IdTarget { get; set; }
        [IgnoreDataMember]
        [XmlIgnore]
        public IShip Origin { get; set; }
        [DataMember]
        public int IdOrigin { get; set; }
        [DataMember]
        public double Thrust { get; set; }
        [DataMember]
        public double Started { get; set; }
        [DataMember]
        public Ecm Jammer { get; set; }
    }
}
