using Content;
using Editions;
using Ship;
using System.Collections.Generic;

namespace SquadBuilderNS
{
    public class ShipRecord
    {
        public GenericShip Instance { get; }
        public string ShipName => Instance.ShipInfo.ShipName;
        public string ShipNameCanonical => Instance.ShipTypeCanonical;
        public string ShipNamespace { get; }
        public List<Legality> AllowableFormats => ((ShipCardInfo25)Instance.ShipInfo)?.LegalityInfo;

        public ShipRecord(string shipNamespace)
        {
            ShipNamespace = shipNamespace;

            // shipTypeNameFull must have format like Ship.SecondEdition.XWing.XWing
            string shipTypeNameFull = shipNamespace + shipNamespace.Substring(shipNamespace.LastIndexOf('.'));
            Instance = (GenericShip) System.Activator.CreateInstance(System.Type.GetType(shipTypeNameFull));
            Edition.Current.AdaptShipToRules(Instance);
        }
    }
}
