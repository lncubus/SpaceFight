using System;
using System.Collections.Generic;
using System.Linq;

using SF.Space;

namespace SF.ClientLibrary
{
    public class UniverseView
    {
        public TimeSpan Time { get; private set; }
        public int Generation { get; private set; }
        public ConstantData Constants { get; private set; }
        public IDictionary<int, Nation> Nations { get; private set; }
        public IDictionary<int, ShipClass> ShipClasses { get; private set; }
        public IDictionary<int, MissileClass> MissileClasses { get; private set; }
        public IDictionary<int, Star> Stars { get; private set; }
        public IDictionary<int, Ship> Ships { get; private set; }
        public IDictionary<int, Missile> Missiles { get; private set; }
        public Ship Ship { get; private set; }

        public void UpdateData(ClientData view)
        {
            if (view.Permanent != null)
                UpdatePermanentData(view.Permanent);
            if (view.Volatile != null)
                UpdateVolatileData(view.Volatile);
            if (view.Navigation != null)
            {
                Ship = Ships[view.Navigation.Id];
                Ship.ControlShip = view.Navigation;
            }
        }

        private void UpdateVolatileData(VolatileViewData volatileViewData)
        {
            Time = volatileViewData.Time;
            foreach (var ship in Ships.Values)
                ship.VolatileShip = null;
            foreach (var ship in volatileViewData.Ships)
                Ships[ship.Id].VolatileShip = ship;
            Missiles = volatileViewData.Missiles.ToDictionary(missile => missile.Id);
        }

        private void UpdatePermanentData(PermanentViewData permanentViewData)
        {
            Generation = permanentViewData.Generation;
            Time = permanentViewData.Time;
            Constants = permanentViewData.Constants;
            Nations = permanentViewData.Nations.ToDictionary(data => data.Id);
            ShipClasses = permanentViewData.ShipClasses.ToDictionary(data => data.Id);
            MissileClasses = permanentViewData.MissileClasses.ToDictionary(data => data.Id);
            Stars = permanentViewData.Stars.ToDictionary(data => data.Id);
            Ships = permanentViewData.Ships.Select(data => new Ship(data)).ToDictionary(ship => ship.Id);
            ShipClasses.Values.ApplyNations(Nations);
            MissileClasses.Values.ApplyNations(Nations);
            Stars.Values.ApplyNations(Nations);
            Ships.Values.ApplyNations(Nations);
            foreach (var ship in Ships.Values)
                ship.Class = ShipClasses[ship.IdClass];
        }
    }
}
