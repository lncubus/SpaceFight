using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SF.Space;

namespace SF.ServiceLibrary
{
    internal class RemoteShip : IShip
    {
        public static IDictionary<string, IShipClass> Classes;
        private SpaceShip that;

        public RemoteShip(SpaceShip def)
        {
            that = def;
        }

        public IShipClass Class
        {
            get
            {
                if (!Classes.ContainsKey(that.ClassName))
                    return null;
                return Classes[that.ClassName];
            }
        }

        public void Update(SpaceShip def)
        {
            that = def;
        }

        public string Nation
        {
            get { return that.Nation; }
        }

        public string Name
        {
            get { return that.ShipName; }
        }

        public string ClassName
        {
            get { return that.ClassName; }
        }

        public double Heading
        {
            get { return that.Heading; }
        }

        public double Roll
        {
            get { return that.Roll; }
        }

        public double Acceleration
        {
            get { return that.Acceleration; }
        }

        public Vector S
        {
            get { return that.Position; }
        }

        public Vector V
        {
            get { return that.Speed; }
        }

        public Vector A
        {
            get { return Acceleration*Vector.Direction(Heading); }
        }
    }
}
