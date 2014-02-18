using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.ServiceModel;

namespace SF.ClientLibrary
{
    using SF.Space;

    public class SpaceClient : IDisposable
    {
        public static string Password = string.Empty;
        public const int Version = 0x0023;
        private readonly IServer Client;
        private readonly ChannelFactory<IServer> Factory;
        private UniverseView Universe;

        public SpaceClient()
        {
            Factory = new ChannelFactory<IServer>(GetType().FullName);
            Client = Factory.CreateChannel();
            int version = Client.Connect(Password);
            if (version != Version)
                throw new NotSupportedException("Server version mismatch");
            Universe = new UniverseView();
            ViewData view = Client.GetView(-1);
            Universe.UpdateData(view);
        }

        public void Dispose()
        {
            if (Client != null)
                Client.Logout();
            Factory.Close();
            Universe = null;
        }

        public IDictionary<string, string[]> GetShipNames()
        {
            var result = new SortedDictionary<string, string[]>();
            foreach (var n in Universe.Nations.Values)
            {
                var nation = n;
                var key = nation.Name;
                var ships = Universe.Ships.Values.Where(ship => ship.Nation == nation);
                var names = ships.Select(ship => ship.Name).ToList();
                names.Sort();
                if (names.Any())
                    result.Add(key, names.ToArray());
            }
            return result;
        }

        public bool Login(Ship ship)
        {
            var accepted = Client.Login(ship.Id);
            if (!accepted)
                return false;
            ViewData view = Client.GetView(Universe.Generation);
            Universe.UpdateData(view);
            return true;
        }
    }
}
