using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using SF.Space;

namespace SF.ServiceLibrary
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "SpaceServer" in both code and config file together.
    [ServiceBehavior(InstanceContextMode = InstanceContextMode.PerSession, ConcurrencyMode = ConcurrencyMode.Multiple)]
    public class SpaceServer : IServer
    {
        public static Universe Instance = new Universe();

        private IHelm helm;
        public bool Login(string nation, string ship)
        {
            helm = Instance.GetHelm(nation, ship);
            return helm != null;
        }

        public void Logout()
        {
            helm = null;
        }

        public void Connect(string password)
        {
            helm = null;
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
            return SpaceShip.Store(helm);
        }

        public ICollection<ShipClass> GetShipClasses()
        {
            return Instance.GetShipClasses(helm.Ship.Nation).OfType<ShipClass>().ToList();
        }

        public ICollection<SpaceShip> GetVisibleShips()
        {
            return Instance.GetVisibleShips(helm).Select(s => SpaceShip.Store(s)).ToList();
        }

        public void SetHeadingTo(double value)
        {
            helm.HeadingTo = value;
        }

        public void SetRollTo(double value)
        {
            helm.RollTo = value;
        }

        public void SetAccelerateTo(double value)
        {
            if (value < 0)
                value = 0;
            if (value > helm.Ship.Class.MaximumAcceleration)
                value = helm.Ship.Class.MaximumAcceleration;
            helm.AccelerateTo = value;
        }
    }
}
