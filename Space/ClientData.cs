using System.Runtime.Serialization;

namespace SF.Space
{
    [DataContract]
    public class ClientData
    {
        [DataMember]
        public PermanentViewData Permanent;
        [DataMember]
        public VolatileViewData Volatile;
        [DataMember]
        public ControlShipData Navigation;
    }
}
