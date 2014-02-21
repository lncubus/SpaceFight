using SF.Space;

namespace SF.ClientLibrary
{
    public class Ship : INationObject, IShip
    {
        private readonly PermanentShipData p;
        private VolatileShipData v;

        public Ship(PermanentShipData def)
        {
            p = def;
        }

        public void UpdateData(VolatileShipData update)
        {
            v = update;
        }

        public int Id
        {
            get { return p.Id; }
        }

        public string Name
        {
            get { return p.Name; }
        }

        public int IdClass
        {
            get { return p.IdClass; }
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
