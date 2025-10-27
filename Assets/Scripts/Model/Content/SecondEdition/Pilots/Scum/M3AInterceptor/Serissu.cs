using ActionsList;
using Content;
using Ship;
using System.Collections.Generic;
using Upgrade;

namespace Ship
{
    namespace SecondEdition.M3AInterceptor
    {
        public class Serissu : M3AInterceptor
        {
            public Serissu() : base()
            {
                PilotInfo = new PilotCardInfo25
                (
                    "Serissu",
                    "Flight Instructor",
                    Faction.Scum,
                    5,
                    4,
                    12,
                    isLimited: true,
                    abilityType: typeof(Abilities.SecondEdition.SerissuAbility),
                    extraUpgradeIcons: new List<UpgradeType>
                    {
                        UpgradeType.Talent,
                        UpgradeType.Talent,
                        UpgradeType.Cannon,
                        UpgradeType.Modification,
                        UpgradeType.Modification
                    },
                    seImageNumber: 183,
                    legality: new List<Legality> { Legality.StandardLegal, Legality.ExtendedLegal }
                );
            }
        }

        public class SerissuXWA : Serissu
        {
            public SerissuXWA() : base()
            {
                (PilotInfo as PilotCardInfo25).Cost = 10;
                (PilotInfo as PilotCardInfo25).LoadoutValue = 11;
                (PilotInfo as PilotCardInfo25).LegalityInfo = new List<Legality> { Legality.XWA };
                (PilotInfo as PilotCardInfo25).ExtraUpgrades = new List<UpgradeType>
                {
                    UpgradeType.Talent,
                    UpgradeType.Modification
                };
            }
        }
    }
}

namespace Abilities.SecondEdition
{
    // When another friendly ship at Range 0-1 is defending, it may reroll 1 defense die.
    public class SerissuAbility : GenericAbility
    {
        public override void ActivateAbility()
        {
            GenericShip.OnGenerateDiceModificationsGlobal += AddSerissuAbility;
        }

        public override void DeactivateAbility()
        {
            GenericShip.OnGenerateDiceModificationsGlobal -= AddSerissuAbility;
        }

        private void AddSerissuAbility(GenericShip ship)
        {
            Combat.Defender.AddAvailableDiceModification(new SerissuAction() { ImageUrl = HostShip.ImageUrl }, HostShip);
        }

        protected class SerissuAction : FriendlyRerollAction
        {
            public SerissuAction() : base(1, 1, true, RerollTypeEnum.DefenseDice)
            {
                Name = DiceModificationName = "Serissu";
            }
        }
    }
}