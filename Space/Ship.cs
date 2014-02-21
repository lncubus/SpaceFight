namespace SF.Space
{
    public class Ship : INationObject, IShip
    {
        public readonly PermanentShipData PermanentShip;
        public VolatileShipData VolatileShip;

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
    }
}
