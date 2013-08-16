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
        private IList<RemoteMissile> Missiles;

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
            this.Missiles = null;
            this.Helm = null;
        }

        public IDictionary<string, string[]> GetShipNames()
        {
            var result = new SortedDictionary<string, string[]>();
            foreach (var pair in this.Client.GetShipNames())
                result.Add(pair.Key, pair.Value);
            return result;
        }

        public void Login(string nation, string name)
        {
            this.Client.Login(nation, name);
            Catalog.Create(this.Client.GetCatalog());
            var view = this.Client.GetView();
            this.Helm = new RemoteHelm(this.Client, view.Helm);
            this.Ships = view.Ships.Select(def => new RemoteShip(def)).ToDictionary(s => s.Name);
        }

        public IHelm GetHelm()
        {
            return this.Helm;
        }

        public IEnumerable<IShip> GetVisibleShips()
        {
            return this.Ships.Values;
        }

        public IEnumerable<IMissile> GetVisibleMissiles()
        {
            return this.Missiles;
        }

        public void Update()
        {
            var view = this.Client.GetView();
            this.Helm.Update(view.Helm);
            foreach (var ship in view.Ships)
                if (this.Ships.ContainsKey(ship.ShipName))
                    this.Ships[ship.ShipName].Update(ship);
                else
                    this.Ships.Add(ship.ShipName, new RemoteShip(ship));
            Missiles = view.Missiles.Select(def => new RemoteMissile(def)).ToList();
        }

        public void Fire(IShip ship, bool left)
        {
            Client.Fire(left, ship.Name, 1);
        }
    }
}
