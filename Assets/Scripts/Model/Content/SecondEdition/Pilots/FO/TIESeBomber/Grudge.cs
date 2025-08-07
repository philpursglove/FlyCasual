using Content;
using System.Collections.Generic;
using Upgrade;

namespace Ship
{
    namespace SecondEdition.TIESeBomber
    {
        public class Grudge : TIESeBomber
        {
            public Grudge() : base()
            {
                IsWIP = true;

                PilotInfo = new PilotCardInfo25
                (
                    "\"Grudge\"",
                    "Hateful Harrier",
                    Faction.FirstOrder,
                    2,
                    4,
                    15,
                    isLimited: true,
                    abilityType: typeof(Abilities.SecondEdition.GrudgePilotAbility),
                    extraUpgradeIcons: new List<UpgradeType>()
                    {
                        UpgradeType.Talent,
                        UpgradeType.Tech,
                        UpgradeType.Missile,
                        UpgradeType.Gunner,
                        UpgradeType.Device,
                        UpgradeType.Device,
                        UpgradeType.Modification
                    },
                    tags: new List<Tags>
                    {
                        Tags.Tie
                    },
                    legality: new List<Legality> { Legality.StandardLegal, Legality.ExtendedLegal }
                );
            }
        }

        public class GrudgeXWA : Grudge
        {
            public GrudgeXWA() : base()
            {
                var pilot = (PilotCardInfo25)PilotInfo;
                pilot.Cost = 3;
                pilot.LoadoutValue = 7;
                pilot.LegalityInfo = new List<Legality> { Legality.XWA };
                pilot.ExtraUpgrades = new List<UpgradeType>
                {
                    UpgradeType.Talent,
                    UpgradeType.Gunner,
                    UpgradeType.Modification,
                    UpgradeType.Tech,
                    UpgradeType.Device,
                    UpgradeType.Device,
                    UpgradeType.Missile,
                };
            }
        }
    }
}

namespace Abilities.SecondEdition
{
    public class GrudgePilotAbility : GenericAbility
    {
        public override void ActivateAbility()
        {
            
        }

        public override void DeactivateAbility()
        {
            
        }
    }
}
