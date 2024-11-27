using Abilities.SecondEdition;
using ActionsList;
using BoardTools;
using Bombs;
using Conditions;
using Content;
using GameCommands;
using GameModes;
using Movement;
using Newtonsoft.Json.Linq;
using Ship;
using SubPhases;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using Tokens;
using UnityEngine;
using UnityEngine.UIElements.Experimental;
using Upgrade;

namespace Ship
{
    namespace SecondEdition.ASF01BWing
    {
        public class GinaMoonsongBattleOverEndor : ASF01BWing
        {
            public GinaMoonsongBattleOverEndor() : base()
            {
                PilotInfo = new PilotCardInfo25
                (
                    "Gina Moonsong",
                    "Battle Over Endor",
                    Faction.Rebel,
                    5,
                    5,
                    0,
                    isLimited: true,
                    abilityType: typeof(GinaMoonsongBattleOverEndorAbility),
                    extraUpgradeIcons: new List<UpgradeType>
                    {
                        UpgradeType.Talent,
                        UpgradeType.Talent,
                        UpgradeType.Torpedo,
                        UpgradeType.Device
                    },
                    tags: new List<Tags>
                    {
                        Tags.BWing
                    },
                    skinName: "Gina Moonsong",
                    charges: 2,
                    regensCharges: 1,
                    isStandardLayout: true
                );

                ShipInfo.Shields++;

                ImageUrl = "https://infinitearenas.com/xw2/images/quickbuilds/ginamoonsong-battleoverendor.png";

                MustHaveUpgrades.Add(typeof(UpgradesList.SecondEdition.ItsATrap));
                MustHaveUpgrades.Add(typeof(UpgradesList.SecondEdition.Juke));
                MustHaveUpgrades.Add(typeof(UpgradesList.SecondEdition.ProtonTorpedoes));
                MustHaveUpgrades.Add(typeof(UpgradesList.SecondEdition.IonBombs));

                ShipAbilities.Add(new GyroCockpit());

                PilotNameCanonical = "ginamoonsong-battleoverendor";

                DefaultUpgrades.Remove(typeof(UpgradesList.SecondEdition.StabilizedSFoilsOpen));
            }
        }
    }
}

namespace Abilities.SecondEdition
{
    public class GinaMoonsongBattleOverEndorAbility : GenericAbility
    {
        public override void ActivateAbility()
        {
            Phases.Events.OnCombatPhaseStart_Triggers += CheckAbility;
        }

        public override void DeactivateAbility()
        {
            Phases.Events.OnCombatPhaseStart_Triggers -= CheckAbility;
        }
        protected virtual void CheckAbility()
        {
            List<GenericShip> friendlyShipsAtRange = Board.GetShipsAtRange(HostShip, new Vector2(0, 2), Team.Type.Friendly);
            List<GenericShip> enemyShipsAtRange = Board.GetShipsAtRange(HostShip, new Vector2(0, 3), Team.Type.Enemy);

            foreach(GenericShip ship in friendlyShipsAtRange)
            {
                if (ship.PilotInfo.PilotName.Equals("Braylen Stramm") && ship.IsStressed  && enemyShipsAtRange.Count > 0)
                {
                    RegisterAbilityTrigger(TriggerTypes.OnCombatPhaseStart, SelectTarget);
                }
            }
        }

        private void SelectTarget(object sender, EventArgs e)
        {
            SelectTargetForAbility(
                AcquireLock,
                FilterTargets,
                GetAiPriority,
                HostShip.Owner.PlayerNo,
                HostShip.PilotInfo.PilotName,
                "You may acquire a lock",
                HostShip,
                showSkipButton: true
            );
        }

        private int GetAiPriority(GenericShip ship)
        {
            return HostShip.Tokens.HasToken<BlueTargetLockToken>() ? 0 : 1000;
        }

        private bool FilterTargets(GenericShip ship)
        {
            return FilterByTargetType(ship, new List<TargetTypes>() { TargetTypes.Enemy }) && FilterTargetsByRange(ship, 0, 3);
        }

        private void AcquireLock()
        {
            ActionsHolder.AcquireTargetLock(
                HostShip,
                TargetShip,
                DecisionSubPhase.ConfirmDecision,
                DecisionSubPhase.ConfirmDecision
            );
        }
    }

    public class ItsATrapAbility : GenericAbility
    {
        public override void ActivateAbility()
        {
            HostShip.OnAttackStartAsDefender += RegisterAbility;
        }

        public override void DeactivateAbility()
        {
            HostShip.OnAttackStartAsDefender -= RegisterAbility;
        }

        private void RegisterAbility()
        {
            AddDiceModification(
                "It's a Trap! ability",
                IsDiceModificationAvailable,
                GetDiceModificationAiPriority,
                DiceModificationType.Reroll,
                1,
                new List<DieSide> { DieSide.Blank }
            );
        }

        private bool IsDiceModificationAvailable()
        {
            List<GenericShip> friendlyShipsInRangeOne = Board.GetShipsAtRange(HostShip, new Vector2(0, 1), Team.Type.Friendly).Where(ship => ship != HostShip).ToList();
            List<GenericShip> enemyShipsInRangeOne = Board.GetShipsAtRange(HostShip, new Vector2(0, 1), Team.Type.Enemy);

            return friendlyShipsInRangeOne.Count > enemyShipsInRangeOne.Count && Combat.DiceRollDefence.Blanks > 0;
        }

        private int GetDiceModificationAiPriority()
        {
            return 1000;
        }
    }

    public class GyroCockpit : GenericAbility
    {
        // After you gain a stress token, you may spend 2 charges to gain an evade token.
        // When you drop a device, you may spend 1 charge to set the template with its middle line aligned with the hashmark on your ship's left or right side instead of your rear guides
        public override void ActivateAbility()
        {
            HostShip.OnTokenIsAssigned += RegisterEvadeAbility;
            HostShip.BeforeBombWillBeDropped += RegisterDeviceDropAbility;
        }

        public override void DeactivateAbility()
        {
            HostShip.OnTokenIsAssigned -= RegisterEvadeAbility;
            HostShip.BeforeBombWillBeDropped -= RegisterDeviceDropAbility;
        }

        private void RegisterEvadeAbility(GenericShip ship, GenericToken token)
        {
            if(token.GetType() == typeof(StressToken))
            { 
                RegisterAbilityTrigger(TriggerTypes.OnTokenIsAssigned, AskUseEvadeAbility);
            }
        }

        private void AskUseEvadeAbility(object sender, EventArgs e)
        {
            if (HostShip.State.Charges >= 2)
            {
                AskToUseAbility(
                    descriptionShort: "Do you want to spend 2 charges to gain an evade token",
                    useByDefault: NeverUseByDefault,
                    useAbility: UseEvadeAbility,
                    imageHolder: HostShip
                );
            }
            else
            {
                Triggers.FinishTrigger();
            }
        }

        private void UseEvadeAbility(object sender, EventArgs e)
        {
            HostShip.Tokens.AssignToken(new EvadeToken(HostShip), DecisionSubPhase.ConfirmDecision);
            HostShip.SpendCharges(2);
        }

        private void RegisterDeviceDropAbility()
        {
            if (HostShip.State.Charges > 0)
            {
                RegisterAbilityTrigger(TriggerTypes.BeforeBombWillBeDropped, AskToUseDeviceDropAbility);
            }
        }

        private void AskToUseDeviceDropAbility(object sender, EventArgs e)
        {
            AskToUseAbility(
                descriptionShort: HostShip.PilotName,
                descriptionLong: "Spend 1 ship charge to drop device using left or right side instead of rear guides?",
                useByDefault: NeverUseByDefault,
                useAbility: UseDeviceAbility,
                imageHolder: HostShip
            );
        }

        private void UseDeviceAbility(object sender, EventArgs e)
        {
            HostShip.OnGetAvailableBombDropTemplatesOneCondition += GetDeviceTemplates;
            HostShip.SpendCharge();
            Triggers.FinishTrigger();
        }

        private void GetDeviceTemplates(List<ManeuverTemplate> availableTemplates, GenericUpgrade upgrade)
        {
            availableTemplates.Clear();
            availableTemplates.Add(new ManeuverTemplate(ManeuverBearing.Straight, ManeuverDirection.Left, ManeuverSpeed.Speed1, true, true));
            availableTemplates.Add(new ManeuverTemplate(ManeuverBearing.Straight, ManeuverDirection.Right, ManeuverSpeed.Speed1, true, true));
            HostShip.OnGetAvailableBombDropTemplatesOneCondition -= GetDeviceTemplates;
        }
    }
}

namespace UpgradesList.SecondEdition
{
    public class ItsATrap : GenericUpgrade
    {
        public ItsATrap() : base()
        {
            UpgradeInfo = new UpgradeCardInfo(
                "It's a Trap!",
                UpgradeType.Talent,
                cost: 0,
                abilityType: typeof(ItsATrapAbility)
            );

            IsHidden = true;

            ImageUrl = HostShip != null? HostShip.ImageUrl : "https://infinitearenas.com/xw2/images/quickbuilds/ginamoonsong-battleoverendor.png";
        }
    }
}