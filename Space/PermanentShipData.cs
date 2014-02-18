using System.Runtime.Serialization;

namespace SF.Space
{
    [DataContract]
    public class PermanentShipData : NationObject, INamed
    {
        [DataMember]
        public int Id { get; set; }
        [DataMember]
        public string Name { get; set; }
        [DataMember]
        public int IdClass { get; set; }
    }
}
