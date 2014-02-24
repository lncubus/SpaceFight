using System.Runtime.Serialization;

namespace SF.Space
{
    [DataContract]
    public class ServerData
    {
        [DataMember]
        public PermanentViewData Permanent;
        [DataMember]
        public VolatileViewData Volatile;
        [DataMember]
        public ControlsViewData Controls;
    }
}
