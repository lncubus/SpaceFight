using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace SF.Space
{
    [DataContract]
    public class CatalogDefinition
    {
        [DataMember]
        public ShipClass[] ShipClasses { get; set; }

        public static CatalogDefinition Store(Catalog catalog)
        {
            return new CatalogDefinition
            {
                ShipClasses = catalog.ShipClasses.Values.ToArray(),
            };
        }
    }
}
