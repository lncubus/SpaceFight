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
            m_helm = Instance.GetHelm(nation, ship);
            return m_helm != null;
        }

        public void Logout()
        {
            m_helm = null;
        }

        public void Connect(string password)
        {
            m_helm = null;
        }

        public KeyValuePair<string, string[]>[] GetShipNames()
        {
            return Instance.GetShipNames();
        }

        public CatalogDefinition GetCatalog()
        {
            return Instance.GetCatalog(m_helm.Nation);
        }

        public View GetView()
        {
            return Instance.GetView(m_helm);
        }

        public void SetHeadingTo(double value)
        {
            m_helm.HeadingTo = value;
        }

        public void SetRollTo(double value)
        {
            m_helm.RollTo = value;
        }

        public void SetThrustTo(double value)
        {
            if (value < 0)
                value = 0;
            if (value > m_helm.Class.MaximumAcceleration)
                value = m_helm.Class.MaximumAcceleration;
            m_helm.ThrustTo = value;
        }

        public void Fire(bool left, string to, int number)
        {
            Instance.Fire(m_helm, left, to, number);
        }
    }
}
