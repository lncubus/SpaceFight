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
            var navi = view.Navigation;
            if (navi != null)
            {
                Ship = Ships[view.Navigation.Id];
                Ship.ControlShip = navi;
                if (Ship.Right == null)
                    Ship.Right = new MissileRacksState(Ship.Class.Right);
                if (Ship.Left == null)
                    Ship.Left = new MissileRacksState(Ship.Class.Left);
                Ship.Right.Reloading = navi.Right;
                Ship.Left.Reloading = navi.Left;
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
            foreach (var shipClass in ShipClasses.Values)
            {
                foreach (var missileRack in shipClass.Right)
                    missileRack.MissileClass = MissileClasses[missileRack.IdMissileClass];
                foreach (var missileRack in shipClass.Left)
                    missileRack.MissileClass = MissileClasses[missileRack.IdMissileClass];
            }
            foreach (var ship in Ships.Values)
                ship.Class = ShipClasses[ship.IdClass];
        }

        public IParticle ById(ParticleType type, int id)
        {
            if (id == 0)
                return null;
            switch (type)
            {
                case ParticleType.Star:
                    return Stars.ById(id);
                case ParticleType.Ship:
                    return Ships.ById(id);
                case ParticleType.Missile:
                    return Missiles.ById(id);
            }
            throw new IndexOutOfRangeException("Unknown particle type.");
        }
    }
}
