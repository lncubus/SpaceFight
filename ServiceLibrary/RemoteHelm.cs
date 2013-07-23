using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SF.Space;

namespace SF.ServiceLibrary
{
    internal class RemoteHelm : IHelm
    {
        public readonly IServer Client;
        private SpaceShip that;
        public RemoteHelm(IServer client, SpaceShip def)
        {
            that = def;
            Client = client;
            ship = new RemoteShip(def);
        }

        public IShip Ship
        {
            get { return ship; }
        }
        private RemoteShip ship;

        public void Update(SpaceShip def)
        {
            ship.Update(def);
            that = def;
        }

        public double HeadingTo
        {
            get
            {
                return that.HeadingTo;
            }
            set
            {
                that.HeadingTo = value;
                Client.SetHeadingTo(value);
            }
        }

        public double RollTo
        {
            get
            {
                return that.RollTo;
            }
            set
            {
                that.RollTo = value;
                Client.SetRollTo(value);
            }
        }

        public double AccelerateTo
        {
            get
            {
                return that.AccelerateTo;
            }
            set
            {
                that.AccelerateTo = value;
                Client.SetAccelerateTo(value);
            }
        }
    }
}
