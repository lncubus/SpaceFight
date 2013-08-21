using System;
using System.Runtime.Serialization;

namespace SF.Space
{
    /// <summary>
    /// The view from the ship.
    /// </summary>
    [DataContract]
    public class View
    {
        [DataMember]
        public TimeSpan Time;

        [DataMember]
        public HelmDefinition Helm { get; set; }

        [DataMember]
        public ShipDefinition[] Ships { get; set; }

        [DataMember]
        public MissileDefinition[] Missiles { get; set; }

        [DataMember]
        public Star[] Stars { get; set; }
    }
}
