using Content;
using System.Collections.Generic;
using Upgrade;

namespace Ship.SecondEdition.TIESeBomber
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
            (PilotInfo as PilotCardInfo25).Cost = 10;
            (PilotInfo as PilotCardInfo25).LoadoutValue = 15;
            (PilotInfo as PilotCardInfo25).ExtraUpgrades = new List<UpgradeType>
            {
                UpgradeType.Gunner,
                UpgradeType.Modification,
                UpgradeType.Tech,
                UpgradeType.Device,
                UpgradeType.Device,
                UpgradeType.Missile,
                UpgradeType.Torpedo
            };
            (PilotInfo as PilotCardInfo25).LegalityInfo = new List<Legality> { Legality.XWA };
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
