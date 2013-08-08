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

        public IEnumerable<KeyValuePair<string, IEnumerable<string>>> GetShipNames()
        {
            return Instance.GetNations().Select(nation => new KeyValuePair<string, IEnumerable<string>>(nation, Instance.GetShipNames(nation)));
        }

        public TimeSpan GetTime()
        {
            return Instance.Time;
        }

        public CatalogDefinition GetCatalog()
        {
            return CatalogDefinition.Store(Catalog.Instance);
        }

        public HelmDefinition GetHelm()
        {
            return HelmDefinition.Store(this.m_helm);
        }

        public IEnumerable<ShipDefinition> GetVisibleShips()
        {
            return Instance.GetVisibleShips(this.m_helm).Select(ShipDefinition.Store);
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
