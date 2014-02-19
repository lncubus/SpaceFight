using SF.Space;

namespace SF.ServerLibrary
{
    public class Ship : NationObject, IShip
    {
        private readonly PermanentShipData p;
        private VolatileShipData v;

        public Ship(PermanentShipData def)
        {
            this.p = def;
        }

        public void UpdateData(VolatileShipData update)
        {
            this.v = update;
        }

        public int Id
        {
            get { return this.p.Id; }
        }

        public string Name
        {
            get { return this.p.Name; }
        }

        public int IdClass
        {
            get { return this.p.IdClass; }
        }

        public ShipClass Class
        {
            get;
            set;
        }

        public Vector Position
        {
            get { return this.v == null ? Vector.Zero : this.v.Position; }
        }

        public Vector Speed
        {
            get { return this.v == null ? Vector.Zero : this.v.Speed; }
        }

        public Vector Acceleration
        {
            get { return this.v == null ? Vector.Zero : this.v.Acceleration; }
        }

        public double Heading
        {
            get { return this.v == null ? 0 : this.v.Heading; }
        }

        public double Roll
        {
            get { return this.v == null ? 0 : this.v.Roll; }
        }

        public double Thrust
        {
            get { return this.v == null ? 0 : this.v.Thrust; }
        }

        public double HealthRate
        {
            get { return this.v == null ? 0 : this.v.HealthRate; }
        }
    }
}
