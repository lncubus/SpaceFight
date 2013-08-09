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
        private readonly IServer Client;
        private readonly ChannelFactory<IServer> Factory;
        private IDictionary<string, RemoteShip> Ships;
        private RemoteHelm Helm;

        public SpaceClient()
        {
            this.Factory = new ChannelFactory<IServer>(this.GetType().FullName);
            this.Client = this.Factory.CreateChannel();
            this.Client.Connect(Password);
        }

        public void Dispose()
        {
            if (this.Client != null)
                this.Client.Logout();
            this.Factory.Close();
            this.Ships = null;
            this.Helm = null;
        }

        public TimeSpan GetTime()
        {
            return this.Client.GetTime();
        }

        public IDictionary<string, string[]> GetShipNames()
        {
            var result = new SortedDictionary<string, string[]>();
            foreach (var pair in this.Client.GetShipNames())
                result.Add(pair.Key, pair.Value);
            return result;
        }

        public IHelm GetHelm(string nation, string name)
        {
            this.Client.Login(nation, name);
            Catalog.Create(this.Client.GetCatalog());
            var shipInfo = this.Client.GetHelm();
            this.Helm = new RemoteHelm(this.Client, shipInfo);
            this.Ships = this.Client.GetVisibleShips().Select(def => new RemoteShip(def)).ToDictionary(s => s.Name);
            return this.Helm;
        }

        public IEnumerable<IShip> GetVisibleShips()
        {
            return this.Ships.Values;
        }

        public void Update()
        {
            var helm = this.Client.GetHelm();
            this.Helm.Update(helm);
            var ships = this.Client.GetVisibleShips();
            foreach (var ship in ships)
                if (this.Ships.ContainsKey(ship.ShipName))
                    this.Ships[ship.ShipName].Update(ship);
                else
                    this.Ships.Add(ship.ShipName, new RemoteShip(ship));
        }
    }
}
