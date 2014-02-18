﻿using System.Runtime.Serialization;
using System.Xml.Serialization;

namespace SF.Space
{
    [DataContract]
    public class ShipClass : NationObject, INamed
    {
        [DataMember]
        public int Id { get; set; }
        [DataMember]
        public ShipType Superclass { get; set; }
        [DataMember]
        public string Name { get; set; }
        [DataMember]
        public double MaximumAcceleration { get; set; }
        [DataMember]
        public double FullAccelerationTime { get; set; }
        [DataMember]
        public double RoundRollTime { get; set; }
        [DataMember]
        public double FullTurnTime { get; set; }
        [DataMember]
        public double Weight { get; set; }
        [DataMember]
        public double Wedge { get; set; }
        [DataMember]
        public double ReloadTime { get; set; }
        [DataMember]
        public double RechargeTime { get; set; }
    }
}
