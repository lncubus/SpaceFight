using System.Runtime.Serialization;
using System.Xml.Serialization;

namespace SF.Space
{
    [DataContract]
    public class Star : INationObject, IParticle
    {
        [DataMember]
        public int Id { get; set; }
        [DataMember]
        public int IdNation { get; set; }
        [DataMember]
        public StarType StarClass { get; set; }
        [DataMember]
        public string Name { get; set; }
        [DataMember]
        public double Radius { get; set; }
        [DataMember]
        public Vector Position { get; set; }
        [XmlIgnore]
        [IgnoreDataMember]
        public Nation Nation { get; set; }
        public Vector Speed
        {
            get { return Vector.Zero; }
        }
        public Vector Acceleration
        {
            get { return Vector.Zero; }
        }

    }
}
