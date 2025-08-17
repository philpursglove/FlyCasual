using Content;
using System.Collections.Generic;
using Upgrade;

namespace Ship
{
    namespace SecondEdition.EscapeCraft
    {
        public class LandoCalrissianEscapeCraft : EscapeCraft
        {
            public LandoCalrissianEscapeCraft() : base()
            {
                PilotInfo = new PilotCardInfo25
                (
                    "Lando Calrissian",
                    "Smooth-talking Gambler",
                    Faction.Scum,
                    4,
                    3,
                    4,
                    isLimited: true,
                    abilityType: typeof(Abilities.SecondEdition.LandoCalrissianScumPilotAbility),
                    extraUpgradeIcons: new List<UpgradeType>
                    {
                        UpgradeType.Talent,
                        UpgradeType.Crew,
                        UpgradeType.Modification
                    },
                    seImageNumber: 226,
                    legality: new List<Legality> { Legality.StandardLegal, Legality.ExtendedLegal }
                );

                PilotNameCanonical = "landocalrissian-escapecraft";

                ShipAbilities.Add(new Abilities.SecondEdition.CoPilotAbility());
            }
        }

        public class LandoCalrissianEscapeCraftXWA : LandoCalrissianEscapeCraft
        {
            public LandoCalrissianEscapeCraftXWA() : base()
            {
                (PilotInfo as PilotCardInfo25).Cost = 3;
                (PilotInfo as PilotCardInfo25).LoadoutValue = 10;
                (PilotInfo as PilotCardInfo25).LegalityInfo = new List<Legality> { Legality.XWA };
            }
        }
    }
}