using System.Runtime.Serialization;
using System.Xml.Serialization;

namespace SF.Space
{
    [DataContract]
    public class MissileClass : INationObject, INamed
    {
        [DataMember]
        public int Id { get; set; }
        [DataMember]
        public int IdNation { get; set; }
        [XmlIgnore]
        [IgnoreDataMember]
        public Nation Nation { get; set; }
        [DataMember]
        public string Name { get; set; }
        [DataMember]
        public double Acceleration { get; set; }
        [DataMember]
        public double FlyTime { get; set; }
        [DataMember]
        public double Targeting { get; set; }
        [DataMember]
        public double HitDistance { get; set; }
    }
}
