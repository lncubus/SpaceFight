using System;
using System.Runtime.Serialization;

namespace SF.Space
{
    [DataContract]
    public class Star : IParticle
    {
        [DataMember]
        public Guid Id { get; set; }
        [DataMember]
        public StarType StarClass { get; set; }
        [DataMember]
        public string Nation { get; set; }
        [DataMember]
        public string Name { get; set; }
        [DataMember]
        public double Radius { get; set; }
        [DataMember]
        public Vector Position { get; set; }
        [DataMember]
        public double Weight { get; set; }
        public Vector Speed
        {
            get { return Vector.Zero; }
        }
        public Vector Acceleration
        {
            get { return Vector.Zero; }
        }
    }

    public enum StarType
    {
        Planet = 0,
        Habitable = 1,
        Inhabited = 3,
        Gas = 4,
        Star = 8,
    }
}
