﻿using System;
using System.Runtime.Serialization;

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
        public byte[] Damage;

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
                Damage = helm.Damage ?? new byte[Subsytsem.Length],
                Description = helm.Description,
            };
        }
    }
}
