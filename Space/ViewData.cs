using System.Runtime.Serialization;
using System.Xml.Serialization;

namespace SF.Space
{

    [DataContract]
    public class ViewData
    {
        [DataMember]
        [XmlElement("Permanent")]
        public PermanentViewData PermanentView;
        [DataMember]
        [XmlElement("Volatile")]
        public VolatileViewData VolatileView;
    }
}
