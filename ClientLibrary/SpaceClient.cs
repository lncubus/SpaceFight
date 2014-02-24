using System;
using System.Collections.Generic;
using System.Linq;
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
        public UniverseView Universe { get; private set; }

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

        public IDictionary<string, IDictionary<string, int>> GetShipRegistry()
        {
            var result = new SortedDictionary<string, IDictionary<string, int>>();
            foreach (var n in Universe.Nations.Values)
            {
                var nation = n;
                var key = nation.Name;
                var ships = Universe.Ships.Values.Where(ship => ship.Nation == nation).ToArray();
                if (ships.Any())
                {
                    var registry = new SortedDictionary<string, int>();
                    foreach (var ship in ships)
                    {
                        registry.Add(ship.Name, ship.Id);
                    }
                    result.Add(key, registry);
                }
            }
            return result;
        }

        public bool Login(int idShip)
        {
            var accepted = Client.Login(idShip);
            if (!accepted)
                return false;
            Universe.Ship = Universe.Ships[idShip];
            return true;
        }

        public void UpdateView()
        {
            var w = new System.Diagnostics.Stopwatch();
            w.Start();
            ViewData view = Client.GetView(Universe.Generation);
            w.Stop();
            System.Diagnostics.Debug.WriteLine("Client.GetView : {0} ms", w.ElapsedMilliseconds);
            Universe.UpdateData(view);
        }
    }
}
