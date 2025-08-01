using Content;
using System.Collections.Generic;
using Upgrade;

namespace Ship
{
    namespace SecondEdition.ARC170Starfighter
    {
        public class NorraWexley : ARC170Starfighter
        {
            public NorraWexley() : base()
            {
                PilotInfo = new PilotCardInfo25
                (
                    "Norra Wexley",
                    "Gold Nine",
                    Faction.Rebel,
                    5,
                    5,
                    8,
                    isLimited: true,
                    abilityType: typeof(Abilities.SecondEdition.NorraWexleyAbility),
                    extraUpgradeIcons: new List<UpgradeType>
                    {
                        UpgradeType.Talent,
                        UpgradeType.Talent,
                        UpgradeType.Torpedo,
                        UpgradeType.Gunner,
                        UpgradeType.Astromech,
                        UpgradeType.Modification
                    },
                    seImageNumber: 65,
                    legality: new List<Legality> { Legality.StandardLegal, Legality.ExtendedLegal }
                );
            }
        }

        public class NorraWexleyXWA : ARC170Starfighter
        {
            public NorraWexleyXWA() : base()
            {
                PilotInfo = new PilotCardInfo25
                (
                    "Norra Wexley",
                    "Gold Nine",
                    Faction.Rebel,
                    5,
                    5,
                    12,
                    isLimited: true,
                    abilityType: typeof(Abilities.SecondEdition.NorraWexleyAbility),
                    extraUpgradeIcons: new List<UpgradeType>
                    {
                        UpgradeType.Talent,
                        UpgradeType.Talent,
                        UpgradeType.Astromech,
                        UpgradeType.Gunner,
                        UpgradeType.Modification,
                        UpgradeType.Torpedo                        
                    },
                    seImageNumber: 65,
                    legality: new List<Legality> { Legality.XWA }
                );
            }
        }
    }
}