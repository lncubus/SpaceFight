namespace SF.Space
{
    public class Ship : INationObject, IShip
    {
        public readonly PermanentShipData PermanentShip;
        public VolatileShipData VolatileShip;
        public ControlShipData ControlShip;
        public MissileRacksState Right;
        public MissileRacksState Left;

        public Ship(PermanentShipData def)
        {
            PermanentShip = def;
        }

        public int Id
        {
            get { return PermanentShip.Id; }
        }

        public string Name
        {
            get { return PermanentShip.Name; }
        }

        public int IdNation 
        {
            get { return PermanentShip.IdNation; }
        }

        public Nation Nation
        {
            get { return PermanentShip.Nation; }
            set { PermanentShip.Nation = value; }
        }

        public int IdClass
        {
            get { return PermanentShip.IdClass; }
        }

        public ShipClass Class { get; set; }

        public Vector Position
        {
            get { return VolatileShip == null ? Vector.Zero : VolatileShip.Position; }
        }

        public Vector Speed
        {
            get { return VolatileShip == null ? Vector.Zero : VolatileShip.Speed; }
        }

        public Vector Acceleration
        {
            get { return VolatileShip == null ? Vector.Zero : VolatileShip.Acceleration; }
        }

        public double Heading
        {
            get { return VolatileShip == null ? 0 : VolatileShip.Heading; }
        }

        public double Roll
        {
            get { return VolatileShip == null ? 0 : VolatileShip.Roll; }
        }

        public double Thrust
        {
            get { return VolatileShip == null ? 0 : VolatileShip.Thrust; }
        }

        public double HealthRate
        {
            get { return VolatileShip == null ? 0 : VolatileShip.HealthRate; }
        }

        public double RollTo
        {
            get
            {
                return ControlShip == null ? Roll : ControlShip.RollTo;
            }
            set
            {
                if (ControlShip != null)
                    ControlShip.RollTo = value;
            }
        }

        public double HeadingTo
        {
            get
            {
                return ControlShip == null ? Heading : ControlShip.HeadingTo;
            }
            set
            {
                if (ControlShip != null)
                    ControlShip.HeadingTo = value;
            }
        }

        public double ThrustTo
        {
            get
            {
                return ControlShip == null ? Thrust : ControlShip.ThrustTo;
            }
            set
            {
                if (ControlShip != null)
                    ControlShip.ThrustTo = value;
            }
        }

        public int Missiles
        {
            get
            {
                return ControlShip == null ? 0 : ControlShip.Missiles;
            }
            set
            {
                if (ControlShip != null)
                    ControlShip.Missiles = value;
            }
        }
    }
}
