using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SF.Space;
using System.ServiceModel;

namespace SF.ServiceLibrary
{
    public class SpaceClient : IDisposable
    {
        public static string Password = string.Empty;
        public readonly IServer Client;
        private readonly ChannelFactory<IServer> Factory;
        private IDictionary<string, IShipClass> Classes;
        private IDictionary<string, RemoteShip> Ships;
        private RemoteHelm Helm;

        public SpaceClient()
        {
            Factory = new ChannelFactory<IServer>(this.GetType().FullName);
            Client = Factory.CreateChannel();
            Client.Connect(Password);
        }

        public void Dispose()
        {
            if (Client != null)
                Client.Logout();
            Factory.Close();
            RemoteShip.Classes = null;
            Classes = null;
            Ships = null;
            Helm = null;
        }

        public TimeSpan Time
        {
            get { return Client.GetTime(); }
        }

        public ICollection<string> GetNations()
        {
            return Client.GetNations();
        }

        public ICollection<string> GetShipNames(string nation)
        {
            return Client.GetShipNames(nation);
        }

        public ICollection<IShipClass> GetShipClasses()
        {
            return Classes.Values;
        }

        public IHelm GetHelm(string nation, string name)
        {
            Client.Login(nation, name);
            Classes = Client.GetShipClasses().ToDictionary<IShipClass, string>(c => c.Name);
            RemoteShip.Classes = Classes;
            var shipInfo = Client.GetHelm();
            Helm = new RemoteHelm(Client, shipInfo);
            Ships = Client.GetVisibleShips().Select(def => new RemoteShip(def)).ToDictionary(s => s.Name);
            return Helm;
        }

        public ICollection<IShip> GetVisibleShips()
        {
            return Ships.Values.OfType<IShip>().ToList();
        }

        public void Update()
        {
            var helm = Client.GetHelm();
            Helm.Update(helm);
            var ships = Client.GetVisibleShips();
            foreach (var ship in ships)
                if (Ships.ContainsKey(ship.ShipName))
                    Ships[ship.ShipName].Update(ship);
                else
                    Ships.Add(ship.ShipName, new RemoteShip(ship));
        }
    }
}
