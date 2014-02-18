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
            //UpdateVolatileData(view.VolatileView);
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
            Ships = permanentViewData.Ships.Select(CreateShip).ToDictionary(ship => ship.Id);
            ApplyNations(ShipClasses.Values);
            ApplyNations(MissileClasses.Values);
            ApplyNations(Stars.Values);
            ApplyNations(Ships.Values);
            foreach (var ship in Ships.Values)
                ship.Class = ShipClasses[ship.IdClass];
        }

        private Ship CreateShip(PermanentShipData def)
        {
            return new Ship(def);
        }

        private void ApplyNations(IEnumerable<NationObject> objects)
        {
            foreach (var nationObject in objects)
            {
                int id = nationObject.IdNation;
                nationObject.Nation = id == 0 ? null : Nations[id];
            }
        }
    }
}
