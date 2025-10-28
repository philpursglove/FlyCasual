using Abilities.SecondEdition;
using Content;
using Ship.CardInfo;
using System;
using System.Collections.Generic;
using System.Linq;
using Upgrade;
using UpgradesList.SecondEdition;

namespace Ship.SecondEdition.BTANR2WYWing
{
    public class BTANR2WYWing : BTANR2YWing.BTANR2YWing
    {
        public BTANR2WYWing() : base()
        {
            ShipCardInfo25 shipInfo = ShipInfo as ShipCardInfo25;
            shipInfo.ShipName = "BTA-NR2-W Y-wing";
            shipInfo.LegalityInfo = new List<Legality>() { Legality.XWA };
            shipInfo.FactionData = new FactionData(new Dictionary<Faction, Type> { { Faction.Resistance, typeof(ZoriiBliss) } });
            shipInfo.Shields = 5;

            ShipAbilities.Remove(ShipAbilities.First(n => n.GetType() == typeof(IntuitiveInterfaceAbility)));
            ShipAbilities.Add(new DevastatingBarrageAbility());
        }
    }
}