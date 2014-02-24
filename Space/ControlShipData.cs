using System.Runtime.Serialization;

namespace SF.Space
{
    public class ControlShipData
    {
        public ControlShipData()
        {
        }

        public ControlShipData(IShip ship)
        {
            Id = ship.Id;
            HeadingTo = ship.Heading;
            RollTo = ship.Roll;
            ThrustTo = ship.Thrust;
        }

        [DataMember]
        public int Id { get; set; }
        [DataMember]
        public double HeadingTo { get; set; }
        [DataMember]
        public double RollTo { get; set; }
        [DataMember]
        public double ThrustTo { get; set; }
    }
}
