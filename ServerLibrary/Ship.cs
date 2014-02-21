using SF.Space;

namespace SF.ServerLibrary
{
    public class Ship : NationObject, IShip
    {
        public readonly PermanentShipData PermanentShip;
        private VolatileShipData v;

        public Ship(PermanentShipData def)
        {
            PermanentShip = def;
            IdNation = PermanentShip.IdNation;
        }

        public void UpdateData(VolatileShipData update)
        {
            v = update;
        }

        public int Id
        {
            get { return PermanentShip.Id; }
        }

        public string Name
        {
            get { return PermanentShip.Name; }
        }

        public int IdClass
        {
            get { return PermanentShip.IdClass; }
        }

        public ShipClass Class
        {
            get;
            set;
        }

        public Vector Position
        {
            get { return v == null ? Vector.Zero : v.Position; }
        }

        public Vector Speed
        {
            get { return v == null ? Vector.Zero : v.Speed; }
        }

        public Vector Acceleration
        {
            get { return v == null ? Vector.Zero : v.Acceleration; }
        }

        public double Heading
        {
            get { return v == null ? 0 : v.Heading; }
        }

        public double Roll
        {
            get { return v == null ? 0 : v.Roll; }
        }

        public double Thrust
        {
            get { return v == null ? 0 : v.Thrust; }
        }

        public double HealthRate
        {
            get { return v == null ? 0 : v.HealthRate; }
        }
    }
}
