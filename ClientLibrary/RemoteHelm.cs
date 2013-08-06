using SF.Space;

namespace SF.ClientLibrary
{
    internal class RemoteHelm : IHelm
    {
        private readonly IServer m_client;
        private readonly RemoteShip m_ship;
        private SpaceShip m_that;

        public RemoteHelm(IServer client, SpaceShip def)
        {
            this.m_that = def;
            this.m_client = client;
            this.m_ship = new RemoteShip(def);
        }

        public IShip Ship
        {
            get { return this.m_ship; }
        }

        public void Update(SpaceShip def)
        {
            this.m_ship.Update(def);
            this.m_that = def;
        }

        public double HeadingTo
        {
            get
            {
                return this.m_that.HeadingTo;
            }
            set
            {
                this.m_that.HeadingTo = value;
                this.m_client.SetHeadingTo(value);
            }
        }

        public double RollTo
        {
            get
            {
                return this.m_that.RollTo;
            }
            set
            {
                this.m_that.RollTo = value;
                this.m_client.SetRollTo(value);
            }
        }

        public double AccelerateTo
        {
            get
            {
                return this.m_that.AccelerateTo;
            }
            set
            {
                this.m_that.AccelerateTo = value;
                this.m_client.SetAccelerateTo(value);
            }
        }
    }
}
