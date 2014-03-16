using System.Runtime.Serialization;

namespace SF.Space
{
    public class ControlShipData
    {
        public ControlShipData()
        {
        }

        public ControlShipData(IShip ship, ShipClass shipClass)
        {
            Id = ship.Id;
            HeadingTo = ship.Heading;
            RollTo = ship.Roll;
            ThrustTo = ship.Thrust;
            Missiles = shipClass.Missiles;
        }

        [DataMember]
        public int Id { get; set; }
        [DataMember]
        public double HeadingTo { get; set; }
        [DataMember]
        public double RollTo { get; set; }
        [DataMember]
        public double ThrustTo { get; set; }
        [DataMember]
        public int Missiles { get; set; }
        [DataMember]
        public double[] Right { get; set; }
        [DataMember]
        public double[] Left { get; set; }
        [DataMember]
        public double EngineDamage { get; set; }
        [DataMember]
        public double NavigationDamage { get; set; }
        [DataMember]
        public double AttackDamage { get; set; }
        [DataMember]
        public double DefenseDamage { get; set; }
    }
}
