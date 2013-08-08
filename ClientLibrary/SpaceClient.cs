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
        private IDictionary<string, ShipClass> Classes;
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
            RemoteShip.Classes = null;
            this.Classes = null;
            this.Ships = null;
            this.Helm = null;
        }

        public TimeSpan GetTime()
        {
            return this.Client.GetTime();
        }

        public IEnumerable<string> GetNations()
        {
            return this.Client.GetNations();
        }

        public IEnumerable<string> GetShipNames(string nation)
        {
            return this.Client.GetShipNames(nation);
        }

        public IHelm GetHelm(string nation, string name)
        {
            this.Client.Login(nation, name);
            this.Classes = this.Client.GetShipClasses().ToDictionary(c => c.Name);
            RemoteShip.Classes = this.Classes;
            var shipInfo = this.Client.GetHelm();
            this.Helm = new RemoteHelm(this.Client, shipInfo);
            this.Ships = this.Client.GetVisibleShips().Select(def => new RemoteShip(def)).ToDictionary(s => s.Name);
            return this.Helm;
        }

        public ICollection<IShip> GetVisibleShips()
        {
            return this.Ships.Values.OfType<IShip>().ToList();
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
