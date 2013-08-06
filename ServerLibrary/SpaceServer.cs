using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using SF.Space;

namespace SF.ServerLibrary
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "SpaceServer" in both code and config file together.
    [ServiceBehavior(InstanceContextMode = InstanceContextMode.PerSession, ConcurrencyMode = ConcurrencyMode.Multiple)]
    public class SpaceServer : IServer
    {
        public static readonly Universe Instance = new Universe();

        private IHelm m_helm;

        public bool Login(string nation, string ship)
        {
            this.m_helm = Instance.GetHelm(nation, ship);
            return this.m_helm != null;
        }

        public void Logout()
        {
            this.m_helm = null;
        }

        public void Connect(string password)
        {
            this.m_helm = null;
        }

        public ICollection<string> GetNations()
        {
            return Instance.GetNations();
        }

        public ICollection<string> GetShipNames(string nation)
        {
            return Instance.GetShipNames(nation);
        }

        public TimeSpan GetTime()
        {
            return Instance.Time;
        }

        public SpaceShip GetHelm()
        {
            return SpaceShip.Store(this.m_helm);
        }

        public ICollection<ShipClass> GetShipClasses()
        {
            return Instance.GetShipClasses(this.m_helm.Ship.Nation).ToList();
        }

        public ICollection<SpaceShip> GetVisibleShips()
        {
            return Instance.GetVisibleShips(this.m_helm).Select(SpaceShip.Store).ToList();
        }

        public void SetHeadingTo(double value)
        {
            this.m_helm.HeadingTo = value;
        }

        public void SetRollTo(double value)
        {
            this.m_helm.RollTo = value;
        }

        public void SetAccelerateTo(double value)
        {
            if (value < 0)
                value = 0;
            if (value > this.m_helm.Ship.Class.MaximumAcceleration)
                value = this.m_helm.Ship.Class.MaximumAcceleration;
            this.m_helm.AccelerateTo = value;
        }
    }
}
