using System.Runtime.Serialization;
using System.Xml.Serialization;

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
        public double AccelerateTo;

        public static HelmDefinition Store(IHelm helm)
        {
            return new HelmDefinition
            {
                AccelerateTo = helm.AccelerateTo,
                HeadingTo = helm.HeadingTo,
                RollTo = helm.RollTo,
                Acceleration = helm.Ship.Acceleration,
                Heading = helm.Ship.Heading,
                Roll = helm.Ship.Roll,
                ShipName = helm.Ship.Name,
                ClassName = helm.Ship.ClassName,
                Nation = helm.Ship.Nation,
                Position = helm.Ship.S,
                Speed = helm.Ship.V,
                MissileName = helm.Ship.MissileName,
                MissileNumber = helm.Ship.Missiles,
            };
        }
    }
}
