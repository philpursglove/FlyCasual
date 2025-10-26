using Content;
using Ship;
using System.Collections.Generic;
using System.Linq;
using Upgrade;

namespace Ship
{
    namespace SecondEdition.ARC170Starfighter
    {
        public class Sinker : ARC170Starfighter
        {
            public Sinker() : base()
            {
                PilotInfo = new PilotCardInfo25
                (
                    "\"Sinker\"",
                    "Wolfpack Veteran",
                    Faction.Republic,
                    3,
                    5,
                    15,
                    isLimited: true,
                    abilityType: typeof(Abilities.SecondEdition.SinkerAbility),
                    extraUpgradeIcons: new List<UpgradeType>
                    {
                        UpgradeType.Talent,
                        UpgradeType.Torpedo,
                        UpgradeType.Astromech,
                        UpgradeType.Crew,
                        UpgradeType.Gunner,
                        UpgradeType.Gunner,
                        UpgradeType.Modification
                    },
                    tags: new List<Tags>
                    {
                        Tags.Clone
                    },
                    skinName: "Red",
                    legality: new List<Legality> { Legality.StandardLegal, Legality.ExtendedLegal }
                );
            }
        }

        public class SinkerXWA : Sinker
        {
            public SinkerXWA() : base()
            {
                (PilotInfo as PilotCardInfo25).Cost = 11;
                (PilotInfo as PilotCardInfo25).LoadoutValue = 6;
                (PilotInfo as PilotCardInfo25).LegalityInfo = new List<Legality> { Legality.XWA };
                (PilotInfo as PilotCardInfo25).ExtraUpgrades = new List<UpgradeType>
                {
                    UpgradeType.Talent,
                    UpgradeType.Astromech,
                    UpgradeType.Crew,
                    UpgradeType.Gunner,
                    UpgradeType.Modification,
                    UpgradeType.Torpedo
                };
            }
        }
    }
}

namespace Abilities.SecondEdition
{
    //While a friendly ship at range 1-2 in your left or right arc performs a primary attack, it may reroll 1 attack die.
    public class SinkerAbility : GenericAbility
    {
        public override void ActivateAbility()
        {
            AddDiceModification(
                HostName,
                IsAvailable,
                AiPriority,
                DiceModificationType.Reroll,
                1,
                isGlobal: true
            );
        }

        public override void DeactivateAbility()
        {
            RemoveDiceModification();
        }

        protected virtual bool IsAvailable()
        {
            var rangeLeft = HostShip.SectorsInfo.RangeToShipBySector(Combat.Attacker, Arcs.ArcType.Left);
            var rangeRight = HostShip.SectorsInfo.RangeToShipBySector(Combat.Attacker, Arcs.ArcType.Right);

            return
                Combat.AttackStep == CombatStep.Attack
                && Combat.Attacker.Owner == HostShip.Owner
                && Combat.ChosenWeapon.WeaponType == WeaponTypes.PrimaryWeapon
                && ((HostShip.SectorsInfo.IsShipInSector(Combat.Attacker, Arcs.ArcType.Left) && rangeLeft >= 1 && rangeLeft <= 2)
                    || (HostShip.SectorsInfo.IsShipInSector(Combat.Attacker, Arcs.ArcType.Right) && rangeRight >= 1 && rangeRight <= 2));
        }

        private int AiPriority()
        {
            int result = 0;

            if (Combat.AttackStep == CombatStep.Attack)
            {
                var friendlyShip = Combat.Attacker;
                int focuses = Combat.DiceRollAttack.FocusesNotRerolled;
                int blanks = Combat.DiceRollAttack.BlanksNotRerolled;

                if (friendlyShip.GetDiceModificationsGenerated().Count(n => n.IsTurnsAllFocusIntoSuccess) > 0)
                {
                    if (blanks > 0) result = 90;
                }
                else
                {
                    if (blanks + focuses > 0) result = 90;
                }
            }

            return result;
        }
    }
}