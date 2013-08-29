using System;
using SF.Space;

namespace SF.ClientLibrary
{

    internal class RemoteHelm : IHelm
    {
        private readonly IServer m_client;
        private HelmDefinition m_that;

        public RemoteHelm(IServer client, HelmDefinition def)
        {
            m_that = def;
            m_client = client;
        }

        public void Update(HelmDefinition def)
        {
            m_that = def;
        }

        public double HeadingTo
        {
            get
            {
                return m_that.HeadingTo;
            }
            set
            {
                m_that.HeadingTo = value;
                m_client.SetHeadingTo(value);
            }
        }

        public double RollTo
        {
            get
            {
                return m_that.RollTo;
            }
            set
            {
                m_that.RollTo = value;
                m_client.SetRollTo(value);
            }
        }

        public double ThrustTo
        {
            get
            {
                return m_that.ThrustTo;
            }
            set
            {
                m_that.ThrustTo = value;
                m_client.SetThrustTo(value);
            }
        }

        public Guid Id
        {
            get { return m_that.Id; }
        }

        public string Nation
        {
            get { return m_that.Nation; }
        }

        public string Name
        {
            get { return m_that.Name; }
        }

        public double Weight
        {
            get { return m_that.Weight; }
        }

        public double Radius
        {
            get { return m_that.Radius; }
        }

        public Vector Position
        {
            get { return m_that.Position; }
        }

        public Vector Speed
        {
            get { return m_that.Speed; }
        }

        public Vector Acceleration
        {
            get { return m_that.Acceleration; }
        }

        public ShipClass Class
        {
            get { return m_that.Class; }
        }

        public string ClassName
        {
            get { return m_that.ClassName; }
        }

        public MissileClass Missile
        {
            get { return m_that.Missile; }
        }

        public string MissileName
        {
            get { return m_that.MissileName; }
        }

        public int Missiles
        {
            get { return m_that.Missiles; }
        }

        public double Heading
        {
            get { return m_that.Heading; }
        }

        public double Roll
        {
            get { return m_that.Roll; }
        }

        public double Thrust
        {
            get { return m_that.Thrust; }
        }

        public ShipState State
        {
            get { return m_that.State; }
        }
    }
}
