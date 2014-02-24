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
        public const int Version = 0x0023; 
        public static Universe Universe;
        private string Password;
        private Ship ship;

        public bool Login(int idShip)
        {
            if (Universe == null)
                return false;
            Universe.Ships.TryGetValue(idShip, out ship);
            return ship != null;
        }

        public void Logout()
        {
            ship = null;
        }

        public int Connect(string password)
        {
            Password = password;
            return Version;
        }

        public ViewData GetView(int generation)
        {
            if (Universe == null)
                return null;
            PermanentViewData p = Universe.Generation == generation ? null : Universe.GetPermanentData();
            VolatileViewData v = ship == null ? null : Universe.GetVolatileData();
            return new ViewData
            {
                PermanentView = p, 
                VolatileView = v,
            };
        }

        public void SetHeadingTo(double value)
        {
            throw new NotImplementedException();
        }

        public void SetRollTo(double value)
        {
            throw new NotImplementedException();
        }

        public void SetThrustTo(double value)
        {
            throw new NotImplementedException();
        }

        public void Fire(int idShipTo, int[] launchers)
        {
            throw new NotImplementedException();
        }

        public void Launch(int idShip)
        {
            throw new NotImplementedException();
        }
    }
}
