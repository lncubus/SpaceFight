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
            var nations = Instance.GetNations().ToList();
            nations.Sort();
            int n = nations.Count;
            var result = new KeyValuePair<string, string[]>[n];
            for (int i = 0; i < n; i++)
            {
                var nation = nations[i];
                var ships = Instance.GetShipNames(nation).ToList();
                ships.Sort();
                result[i] = new KeyValuePair<string, string[]>(nation, ships.ToArray());
            }
            return result;
        }

        public CatalogDefinition GetCatalog()
        {
            return Instance.GetCatalog(m_helm.Ship.Nation);
        }

        public View GetView()
        {
            return new View
            {
                Time = Instance.Time,
                Helm = HelmDefinition.Store(this.m_helm),
                Ships = Instance.GetVisibleShips(this.m_helm).Select(ShipDefinition.Store).ToArray(),
                Missles = Instance.GetVisibleMissles(this.m_helm).Select(MissleDefinition.Store).ToArray(),
            };
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
