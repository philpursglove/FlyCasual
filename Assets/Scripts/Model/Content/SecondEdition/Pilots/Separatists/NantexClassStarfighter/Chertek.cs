using Content;
using Ship;
using System.Collections.Generic;
using Upgrade;

namespace Ship
{
    namespace SecondEdition.NantexClassStarfighter
    {
        public class Chertek : NantexClassStarfighter
        {
            public Chertek() : base()
            {
                PilotInfo = new PilotCardInfo25
                (
                    "Chertek",
                    "Opportunistic Ace",
                    Faction.Separatists,
                    4,
                    4,
                    10,
                    isLimited: true,
                    abilityType: typeof(Abilities.SecondEdition.ChertekAbility),
                    abilityText: "While you perform a primary attack, if the defender is tractored, you may reroll up to 2 attack dice.",
                    extraUpgradeIcons: new List<UpgradeType>
                    {
                        UpgradeType.Talent,
                        UpgradeType.Talent
                    },
                    legality: new List<Legality> { Legality.StandardLegal, Legality.ExtendedLegal }
                );
            }
        }

        public class ChertekXWA : Chertek
        {
            public ChertekXWA() : base()
            {
                (PilotInfo as PilotCardInfo25).Cost = 10;
                (PilotInfo as PilotCardInfo25).LoadoutValue = 14;
                (PilotInfo as PilotCardInfo25).ExtraUpgrades = new List<UpgradeType>
                {
                    UpgradeType.Talent,
                    UpgradeType.Talent,
                    UpgradeType.Modification,
                    UpgradeType.Modification,
                    UpgradeType.Configuration
                };
                (PilotInfo as PilotCardInfo25).LegalityInfo = new List<Legality> { Legality.XWA };
            }
        }
    }
}

namespace Abilities.SecondEdition
{
    public class ChertekAbility : GenericAbility
    {
        public override void ActivateAbility()
        {
            AddDiceModification(
                HostShip.PilotInfo.PilotName,
                IsAvailable,
                GetAiPriority,
                DiceModificationType.Reroll,
                2
            );
        }

        private bool IsAvailable()
        {
            return Combat.AttackStep == CombatStep.Attack
                && Combat.ChosenWeapon.WeaponType == WeaponTypes.PrimaryWeapon
                && Combat.Defender.IsTractored;
        }

        private int GetAiPriority()
        {
            return 90;
        }

        public override void DeactivateAbility()
        {
            RemoveDiceModification();
        }

    }
}