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

        public KeyValuePair<string, string[]>[] GetShipNames()
        {
            return Instance.GetShipNames();
        }

        public CatalogDefinition GetCatalog()
        {
            return Instance.GetCatalog(m_helm.Ship.Nation);
        }

        public View GetView()
        {
            return Instance.GetView(m_helm);
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

        public void Fire(bool left, string to, int number)
        {
            Instance.Fire(m_helm.Ship, left, to, number);
        }
    }
}
