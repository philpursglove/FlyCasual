using Content;
using System.Collections.Generic;
using Upgrade;

namespace Ship
{
    namespace SecondEdition.EscapeCraft
    {
        public class L337EscapeCraft : EscapeCraft
        {
            public L337EscapeCraft() : base()
            {
                PilotInfo = new PilotCardInfo25
                (
                    "L3-37",
                    "Droid Revolutionary",
                    Faction.Scum,
                    2,
                    3,
                    4,
                    isLimited: true,
                    abilityType: typeof(Abilities.SecondEdition.L337Ability),
                    extraUpgradeIcons: new List<UpgradeType>
                    {
                        UpgradeType.Talent,
                        UpgradeType.Crew,
                        UpgradeType.Modification
                    },
                    tags: new List<Tags>
                    {
                        Tags.Droid
                    },
                    seImageNumber: 228,
                    legality: new List<Legality> { Legality.StandardLegal, Legality.ExtendedLegal }
                );

                PilotNameCanonical = "l337-escapecraft";

                ShipInfo.ActionIcons.SwitchToDroidActions();

                ShipAbilities.Add(new Abilities.SecondEdition.CoPilotAbility());
            }
        }

        public class L337EscapeCraftXWA : L337EscapeCraft
        {
            public L337EscapeCraftXWA() : base()
            {
                (PilotInfo as PilotCardInfo25).Cost = 7;
                (PilotInfo as PilotCardInfo25).LoadoutValue = 10;
                (PilotInfo as PilotCardInfo25).LegalityInfo = new List<Legality> { Legality.XWA };
                (PilotInfo as PilotCardInfo25).ExtraUpgrades = new List<UpgradeType>
                {
                    UpgradeType.Crew,
                    UpgradeType.Modification,
                    UpgradeType.Illicit
                };
            }
        }
    }
}