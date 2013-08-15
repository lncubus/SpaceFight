using System.Runtime.Serialization;

namespace SF.Space
{
    [DataContract]
    public class ShipDefinition
    {
        [DataMember]
        public string ClassName;
        [DataMember]
        public string Nation;
        [DataMember]
        public int MissleNumber;
        [DataMember]
        public string MissleName;
        [DataMember]
        public string ShipName;
        [DataMember]
        public double Heading;
        [DataMember]
        public double Roll;
        [DataMember]
        public double Acceleration;
        [DataMember]
        public Vector Position;
        [DataMember]
        public Vector Speed;

        public static ShipDefinition Store(IShip ship)
        {
            return new ShipDefinition
            {
                Acceleration = ship.Acceleration,
                Heading = ship.Heading,
                Roll = ship.Roll,
                ShipName = ship.Name,
                ClassName = ship.Class.Name,
                Nation = ship.Nation,
                Position = ship.S,
                Speed = ship.V,
            };
        }
    }
}
