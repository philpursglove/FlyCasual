using Abilities.SecondEdition;
using ActionsList;
using Content;
using Ship;
using System.Collections.Generic;
using Upgrade;

namespace Ship
{
    namespace SecondEdition.TIELnFighter
    {
        public class DelMeeko : TIELnFighter
        {
            public DelMeeko() : base()
            {
                PilotInfo = new PilotCardInfo25
                (
                    "Del Meeko",
                    "Inferno Three",
                    Faction.Imperial,
                    4,
                    3,
                    10,
                    isLimited: true,
                    abilityType: typeof(DelMeekoAbility),
                    extraUpgradeIcons: new List<UpgradeType>
                    {
                        UpgradeType.Talent,
                        UpgradeType.Cannon,
                        UpgradeType.Modification
                    },
                    tags: new List<Tags>
                    {
                        Tags.Tie
                    },
                    seImageNumber: 85,
                    skinName: "Inferno",
                    legality: new List<Legality> { Legality.StandardLegal, Legality.ExtendedLegal }
                );
            }
        }

        public class DelMeekoXWA : DelMeeko
        {
            public DelMeekoXWA() : base()
            {
                (PilotInfo as PilotCardInfo25).Cost = 3;
                (PilotInfo as PilotCardInfo25).LoadoutValue = 12;
                (PilotInfo as PilotCardInfo25).LegalityInfo = new List<Legality> { Legality.XWA };
            }
        }
    }
}

namespace Abilities.SecondEdition
{
    // When another friendly ship at Range 2 is defending against a damaged ship, it may reroll 1 defense die.
    public class DelMeekoAbility : GenericAbility
    {
        public override void ActivateAbility()
        {
            GenericShip.OnGenerateDiceModificationsGlobal += AddDelMeekoAbility;
        }

        public override void DeactivateAbility()
        {
            GenericShip.OnGenerateDiceModificationsGlobal -= AddDelMeekoAbility;
        }

        private void AddDelMeekoAbility(GenericShip ship)
        {
            Combat.Defender.AddAvailableDiceModification(
                new DelMeekoAction() {
                    ImageUrl = HostShip.ImageUrl
                },
                HostShip
            );
        }

        private class DelMeekoAction : FriendlyRerollAction
        {
            public DelMeekoAction() : base(1, 2, true, RerollTypeEnum.DefenseDice)
            {
                Name = DiceModificationName = "Del Meeko";
            }

            public override bool IsDiceModificationAvailable()
            {
                if (!Combat.Attacker.Damage.IsDamaged)
                    return false;
                else
                    return base.IsDiceModificationAvailable();
            }
        }
    }
}
