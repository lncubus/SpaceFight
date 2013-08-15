using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SF.Space
{
    public class Catalog
    {
        public IDictionary<string, ShipClass> ShipClasses { get; private set; }
        public IDictionary<string, MissileClass> MissileClasses { get; private set; }
        public static Catalog Instance { get; private set; }

        public ShipClass GetShipClass(string name)
        {
            ShipClass result;
            return !string.IsNullOrEmpty(name) && ShipClasses.TryGetValue(name, out result) ? result : null;
        }

        public MissileClass GetMissileClass(string name)
        {
            MissileClass result;
            return !string.IsNullOrEmpty(name) && MissileClasses.TryGetValue(name, out result) ? result : null;
        }

        private Catalog(CatalogDefinition def)
        {
            ShipClasses = (def.ShipClasses ?? new ShipClass[0]) .ToDictionary(c => c.Name);
            MissileClasses = (def.MissileClasses ?? new MissileClass[0]).ToDictionary(c => c.Name); 
        }

        public static void Create(CatalogDefinition def)
        {
            if (Instance != null)
                throw new InvalidOperationException("Catalog was initialized");
            Instance = new Catalog(def);
        }
    }
}
