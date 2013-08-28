using System;
using System.Runtime.Serialization;
using System.Xml.Serialization;

namespace SF.Space
{
    [DataContract]
    public class ShipDefinition : IShip
    {
        [XmlIgnore]
        [DataMember]
        public Guid Id { get; set; }

        [DataMember]
        public string ClassName { get; set; }

        [DataMember]
        public string Nation { get; set; }
        [DataMember]
        public string MissileName { get; set; }
        [DataMember]
        public int Missiles { get; set; }
        [DataMember]
        public string Name { get; set; }
        [DataMember]
        public double Weight { get; set; }
        [DataMember]
        public double Radius { get; set; }

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

        [DataMember]
        public ShipState State { get; set; }

        public Vector Acceleration
        {
            get { return Thrust*Vector.Direction(Heading); }
        }

        public MissileClass Missile
        {
            get { return Catalog.Instance.GetMissileClass(MissileName); }
        }

        public ShipClass Class
        {
            get { return Catalog.Instance.GetShipClass(ClassName); }
        }

        public static ShipDefinition Store(IShip ship)
        {
            return new ShipDefinition
            {
                Id = ship.Id,
                Thrust = ship.Thrust,
                Heading = ship.Heading,
                Roll = ship.Roll,
                Name = ship.Name,
                ClassName = ship.Class.Name,
                Nation = ship.Nation,
                Position = ship.Position,
                Speed = ship.Speed,
                MissileName = ship.MissileName,
                Missiles = ship.Missiles
            };
        }

        public enum ShipState
        {
            Normal = 0,
            Drifting,
            Sail,
            Hyperspace,
            Junk,
            Annihilated,
        }
    }
}
