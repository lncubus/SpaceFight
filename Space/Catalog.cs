using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SF.Space
{
    public class Catalog
    {
        public IDictionary<string, ShipClass> ShipClasses { get; private set; }
        public IDictionary<string, MissleClass> MissleClasses { get; private set; }
        public static Catalog Instance { get; private set; }

        public ShipClass GetShipClass(string name)
        {
            ShipClass result;
            return !string.IsNullOrEmpty(name) && ShipClasses.TryGetValue(name, out result) ? result : null;
        }

        public MissleClass GetMissleClass(string name)
        {
            MissleClass result;
            return !string.IsNullOrEmpty(name) && MissleClasses.TryGetValue(name, out result) ? result : null;
        }

        private Catalog(CatalogDefinition def)
        {
            ShipClasses = (def.ShipClasses ?? new ShipClass[0]) .ToDictionary(c => c.Name);
            MissleClasses = (def.MissleClasses ?? new MissleClass[0]).ToDictionary(c => c.Name); 
        }

        public static void Create(CatalogDefinition def)
        {
            if (Instance != null)
                throw new InvalidOperationException("Catalog was initialized");
            Instance = new Catalog(def);
        }
    }
}
