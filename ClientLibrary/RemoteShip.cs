using System.Collections.Generic;
using SF.Space;

namespace SF.ClientLibrary
{
    internal class RemoteShip : IShip
    {
        private ShipDefinition m_that;

        public RemoteShip(ShipDefinition def)
        {
            this.m_that = def;
        }

        public ShipClass Class
        {
            get
            {
                return Catalog.Instance.GetShipClass(this.ClassName);
            }
        }

        public void Update(ShipDefinition def)
        {
            this.m_that = def;
        }

        public string Nation
        {
            get { return this.m_that.Nation; }
        }

        public string Name
        {
            get { return this.m_that.ShipName; }
        }

        public string ClassName
        {
            get { return this.m_that.ClassName; }
        }

        public MissileClass Missile
        {
            get { return Catalog.Instance.GetMissileClass(m_that.MissileName); }
        }

        public string MissileName
        {
            get { return this.m_that.MissileName; }
        }

        public int Missiles
        {
            get { return m_that.MissileNumber; }
        }

        public double Heading
        {
            get { return this.m_that.Heading; }
        }

        public double Roll
        {
            get { return this.m_that.Roll; }
        }

        public double Acceleration
        {
            get { return this.m_that.Acceleration; }
        }

        public Vector S
        {
            get { return this.m_that.Position; }
        }

        public Vector V
        {
            get { return this.m_that.Speed; }
        }

        public Vector A
        {
            get { return Acceleration*Vector.Direction(Heading); }
        }
    }
}
