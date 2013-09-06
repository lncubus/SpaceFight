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
        private IDictionary<string, IShip> Ships;
        private RemoteHelm Helm;
        private IList<IMissile> Missiles;
        private IDictionary<string, Star> Stars;

        public SpaceClient()
        {
            Factory = new ChannelFactory<IServer>(GetType().FullName);
            Client = Factory.CreateChannel();
            Client.Connect(Password);
        }

        public void Dispose()
        {
            if (Client != null)
                Client.Logout();
            Factory.Close();
            Ships = null;
            Missiles = null;
            Helm = null;
        }

        public IDictionary<string, string[]> GetShipNames()
        {
            var result = new SortedDictionary<string, string[]>();
            foreach (var pair in Client.GetShipNames())
                result.Add(pair.Key, pair.Value);
            return result;
        }

        public void Login(string nation, string name)
        {
            Client.Login(nation, name);
            Catalog.Create(Client.GetCatalog());
            var view = Client.GetView();
            Helm = new RemoteHelm(Client, view.Helm);
            Ships = view.Ships.OfType<IShip>().ToDictionary(s => s.Name);
            Missiles = view.Missiles.OfType<IMissile>().ToList();
            Stars = view.Stars.ToDictionary(s => s.Name);
        }

        public IHelm GetHelm()
        {
            return Helm;
        }

        public ICollection<IShip> GetVisibleShips()
        {
            return Ships.Values;
        }

        public ICollection<Star> GetStars()
        {
            return Stars.Values;
        }

        public ICollection<IMissile> GetVisibleMissiles()
        {
            return Missiles;
        }

        public void Update()
        {
            var view = Client.GetView();
            Helm.Update(view.Helm);
            Ships = view.Ships.OfType<IShip>().ToDictionary(s => s.Name);
            Missiles = view.Missiles.OfType<IMissile>().ToList();
            Stars = view.Stars.ToDictionary(s => s.Name);
        }

        public void Fire(IShip ship, int[] launchers)
        {
            Client.Fire(ship.Name, launchers);
        }
    }
}
