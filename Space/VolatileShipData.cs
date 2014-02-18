using System.Runtime.Serialization;

namespace SF.Space
{
    [DataContract]
    public class VolatileShipData
    {
        [DataMember]
        public int Id { get; set; }
        [DataMember]
        public double Heading { get; set; }
        [DataMember]
        public double Roll { get; set; }
        [DataMember]
        public double Thrust { get; set; }
        [DataMember]
        public Vector Position { get; set; }
        [DataMember]
        public Vector Speed { get; set; }

        public Vector Acceleration
        {
            get { return Thrust * Vector.Direction(Heading); }
        }

        [DataMember]
        public double HealthRate { get; set; }
    }
}
