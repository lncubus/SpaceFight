using System;
using System.Collections.Generic;
using System.Linq;

using SF.Space;

namespace SF.ClientLibrary
{
    public class UniverseView
    {
        public TimeSpan Time;
        public int Generation;
        public ConstantData Constants;
        public IDictionary<int, Nation> Nations;
        public IDictionary<int, ShipClass> ShipClasses;
        public IDictionary<int, MissileClass> MissileClasses;
        public IDictionary<int, Star> Stars;
        public IDictionary<int, Ship> Ships;
        public void UpdateData(ViewData view)
        {
            if (view.PermanentView != null)
                UpdatePermanentData(view.PermanentView);
            if (view.VolatileView != null)
                UpdateVolatileData(view.VolatileView);
        }

        private void UpdateVolatileData(VolatileViewData volatileViewData)
        {
            foreach (var ship in volatileViewData.Ships)
                Ships[ship.Id].VolatileShip = ship;
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
