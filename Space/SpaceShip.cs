using System.Runtime.Serialization;

namespace SF.Space
{
    [DataContract]
    public class SpaceShip
    {
        public static SpaceShip Store(IHelm helm)
        {
            return new SpaceShip
            {
                AccelerateTo = helm.AccelerateTo,
                HeadingTo = helm.HeadingTo,
                RollTo = helm.RollTo,
                Acceleration = helm.Ship.Acceleration,
                Heading = helm.Ship.Heading,
                Roll = helm.Ship.Roll,
                ShipName = helm.Ship.Name,
                ClassName = helm.Ship.Class.Name,
                Nation = helm.Ship.Nation,
                Position = helm.Ship.S,
                Speed = helm.Ship.V,
            };
        }

        public static SpaceShip Store(IShip ship)
        {
            return new SpaceShip
            {
                AccelerateTo = ship.Acceleration,
                Acceleration = ship.Acceleration,
                HeadingTo = ship.Heading,
                Heading = ship.Heading,
                RollTo = ship.Roll,
                Roll = ship.Roll,
                ShipName = ship.Name,
                ClassName = ship.Class.Name,
                Nation = ship.Nation,
                Position = ship.S,
                Speed = ship.V,
            };
        }

        [DataMember]
        public string ClassName;
        [DataMember]
        public string Nation;
        [DataMember]
        public string ShipName;
        [DataMember]
        public double Heading;
        [DataMember]
        public double Roll;
        [DataMember]
        public double Acceleration;
        [DataMember]
        public double HeadingTo;
        [DataMember]
        public double RollTo;
        [DataMember]
        public double AccelerateTo;
        [DataMember]
        public Vector Position;
        [DataMember]
        public Vector Speed;
    }
}
