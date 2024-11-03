using Content;
using Ship;
using System;
using System.Collections.Generic;
using Upgrade;

namespace Ship
{
    namespace SecondEdition.ARC170Starfighter
    {
        public class JagSoC : ARC170Starfighter
        {
            public JagSoC() : base()
            {
                PilotInfo = new PilotCardInfo25(
                    "\"Jag\"",
                    "Siege of Coruscant",
                    Faction.Republic,
                    3,
                    4,
                    0,
                    isLimited: true,
                    extraUpgradeIcons: new List<UpgradeType>
                    {
                        UpgradeType.Gunner,
                        UpgradeType.Astromech,
                        UpgradeType.Modification
                    },
                    abilityType: typeof(Abilities.SecondEdition.JagSoCAbility),
                    tags: new List<Tags>
                    {
                        Tags.Clone
                    },
                    isStandardLayout: true
                );

                MustHaveUpgrades.Add(typeof(UpgradesList.SecondEdition.VeteranTailGunner));
                MustHaveUpgrades.Add(typeof(UpgradesList.SecondEdition.R4PAstromech));
                MustHaveUpgrades.Add(typeof(UpgradesList.SecondEdition.SynchronizedConsole));

                ShipAbilities.Add(new Abilities.SecondEdition.BornForThisAbility());

                PilotNameCanonical = "jag-siegeofcoruscant";

                ModelInfo.SkinName = "Red";
            }
        }
    }
}

namespace Abilities.SecondEdition
{
    //After a friendly ship at range 1-2 in your left or right arc performs an attack, if you are not strained, you may acquire a lock on the defender.
    public class JagSoCAbility : GenericAbility
    {
        public override void ActivateAbility()
        {
            GenericShip.OnAttackFinishGlobal += CheckAbility;
        }
        public override void DeactivateAbility()
        {
            GenericShip.OnAttackFinishGlobal -= CheckAbility;
        }

        private void CheckAbility(GenericShip ship)
    {
            if (Tools.IsFriendly(Combat.Attacker, HostShip)
                && !HostShip.IsStrained
                && ((HostShip.SectorsInfo.IsShipInSector(Combat.Attacker, Arcs.ArcType.Left) && HostShip.SectorsInfo.RangeToShipBySector(Combat.Attacker, Arcs.ArcType.Left) <= 2)
                    || (HostShip.SectorsInfo.IsShipInSector(Combat.Attacker, Arcs.ArcType.Right) && HostShip.SectorsInfo.RangeToShipBySector(Combat.Attacker, Arcs.ArcType.Right) <= 2)))
            {
                RegisterAbilityTrigger(TriggerTypes.OnAttackFinish, delegate
                {
                    AskToUseAbility(
                        HostShip.PilotInfo.PilotName,
                        AlwaysUseByDefault,
                        AcquireLock,
                        descriptionLong: "Do you want to acquire a lock on the Defender?",
                        imageHolder: HostShip
                    );
                });                
            }
        }

        private void AcquireLock(object sender, EventArgs e)
        {
            Messages.ShowInfo(HostName + ": Acquires lock on " + Combat.Defender.PilotInfo.PilotName);
            ActionsHolder.AcquireTargetLock(HostShip, Combat.Defender, SubPhases.DecisionSubPhase.ConfirmDecision, SubPhases.DecisionSubPhase.ConfirmDecision);
        }
    }
}