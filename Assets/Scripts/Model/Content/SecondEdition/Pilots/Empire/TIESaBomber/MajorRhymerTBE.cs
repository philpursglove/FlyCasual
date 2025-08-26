using Arcs;
using Content;
using Ship;
using System.Collections.Generic;
using Upgrade;
using UpgradesList.SecondEdition;

namespace Ship
{
    namespace SecondEdition.TIESaBomber
    {
        public class MajorRhymerTBE : TIESaBomber
        {
            public MajorRhymerTBE() 
            {
                PilotInfo = new PilotCardInfo25(
                    "Major Rhymer",
                    "Precision Destruction",
                    Faction.Imperial,
                    4,
                    5,
                    loadoutValue:0,
                    isStandardLayout: true,
                    tags: new List<Tags>
                    {
                        Tags.Tie
                    },
                    isLimited: true,
                    abilityType: typeof(Abilities.SecondEdition.MajorRhymerTBEAbility),
                    extraUpgradeIcons: new List<UpgradeType>
                    {
                        UpgradeType.Torpedo,
                        UpgradeType.Modification,
                        UpgradeType.Modification
                    }
                );
                PilotNameCanonical = "majorrhymer-swz98";
                
                MustHaveUpgrades.Add(typeof(AdvProtonTorpedoes));
                MustHaveUpgrades.Add(typeof(AfterBurners));
                MustHaveUpgrades.Add(typeof(AutomatedLoaders));

                ImageUrl = "https://infinitearenas.com/xw2/images/quickbuilds/majorrhymer-swz98.png";
            }
        }
    }
}

namespace Abilities.SecondEdition
{
    //While you perform a Torpedo attack, if the defender is in your bullseye, change 1 Focus result to a Crit result.
    public class MajorRhymerTBEAbility : GenericAbility
    {
        public override void ActivateAbility()
        {
            AddDiceModification(
                HostShip.PilotInfo.PilotName,
                IsDiceModificationAvailable,
                GetDiceModificationAiPriority,
                DiceModificationType.Change,
                1,
                new List<DieSide>() { DieSide.Focus },
                DieSide.Crit
            );
        }

        public override void DeactivateAbility()
        {
            RemoveDiceModification();
        }

        private bool IsDiceModificationAvailable()
        {
            return (Combat.AttackStep == CombatStep.Attack
                && Combat.Attacker == HostShip
                && Combat.ChosenWeapon.WeaponType == WeaponTypes.Torpedo
                && Combat.DiceRollAttack.Focuses > 0
                && Combat.Attacker.SectorsInfo.IsShipInSector(Combat.Defender, ArcType.Bullseye)
            );
        }

        private int GetDiceModificationAiPriority()
        {
            return 70;
        }
    }
}