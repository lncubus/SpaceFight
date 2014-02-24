﻿using System.Runtime.Serialization;

namespace SF.Space
{
    [DataContract]
    public class HelmDefinition : ShipDefinition
    {
        [DataMember]
        public double HeadingTo;
        [DataMember]
        public double RollTo;
        [DataMember]
        public double ThrustTo;
        [DataMember]
        public Board Right;
        [DataMember]
        public Board Left;
        [DataMember]
        public string Carrier;
        [DataMember]
        public Health Health;

        public static HelmDefinition Store(IHelm helm)
        {
            return new HelmDefinition
            {
                Id = helm.Id,
                ThrustTo = helm.ThrustTo,
                HeadingTo = helm.HeadingTo,
                RollTo = helm.RollTo,
                Thrust = helm.Thrust,
                Heading = helm.Heading,
                Roll = helm.Roll,
                Name = helm.Name,
                ClassName = helm.ClassName,
                Nation = helm.Nation,
                Position = helm.Position,
                Speed = helm.Speed,
                MissileName = helm.MissileName,
                Missiles = helm.Missiles,
                State = helm.State,
                Radius = helm.Radius,
                Weight = helm.Weight,
                Description = helm.Description,
                Right = helm.Right,
                Left = helm.Left,
                Carrier = helm.Carrier,
                Health = helm.Health,
                HealthRate = helm.Health.Rate,
            };
        }
    }
}