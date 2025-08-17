using Content;
using Ship;
using System.Collections.Generic;
using Upgrade;

namespace Ship
{
    namespace SecondEdition.QuadrijetTransferSpacetug
    {
        public class UnkarPlutt : QuadrijetTransferSpacetug
        {
            public UnkarPlutt() : base()
            {
                PilotInfo = new PilotCardInfo25
                (
                    "Unkar Plutt",
                    "Miserly Portion Master",
                    Faction.Scum,
                    2,
                    4,
                    7,
                    isLimited: true,
                    abilityType: typeof(Abilities.SecondEdition.UnkarPluttAbility),
                    extraUpgradeIcons: new List<UpgradeType>()
                    {
                        UpgradeType.Crew,
                        UpgradeType.Illicit,
                        UpgradeType.Modification,
                        UpgradeType.Tech,
                        UpgradeType.Device                        
                    },
                    seImageNumber: 163,
                    legality: new List<Legality>() { Legality.ExtendedLegal }
                );
            }
        }

        public class UnkarPluttXWA : UnkarPlutt
        {
            public UnkarPluttXWA() : base()
            {
                (PilotInfo as PilotCardInfo25).Cost = 3;
                (PilotInfo as PilotCardInfo25).LoadoutValue = 9;
                (PilotInfo as PilotCardInfo25).ExtraUpgrades = new List<UpgradeType>
                {
                    UpgradeType.ForcePower,
                };
                (PilotInfo as PilotCardInfo25).LegalityInfo = new List<Legality> { Legality.XWA };
            }
        }
    }
}

namespace Abilities.SecondEdition
{
    public class UnkarPluttAbility : GenericAbility
    {
        public override void ActivateAbility()
        {
            Phases.Events.OnCombatPhaseStart_Triggers += TryRegisterPilotAbility;
        }

        public override void DeactivateAbility()
        {
            Phases.Events.OnCombatPhaseStart_Triggers -= TryRegisterPilotAbility;
        }

        private void TryRegisterPilotAbility()
        {
            if (TargetsForAbilityExist(HostShip.ShipsBumped.Contains))
            {
                AssignTokensToBumpedEnemies();
            }
        }

        protected virtual void AssignTokensToBumpedEnemies()
        {
            foreach (GenericShip ship in HostShip.ShipsBumped)
            {
                Triggers.RegisterTrigger(new Trigger()
                {
                    Name = "Assign tractor beam to " + ship.PilotInfo.PilotName,
                    TriggerType = TriggerTypes.OnCombatPhaseStart,
                    TriggerOwner = HostShip.Owner.PlayerNo,
                    EventHandler = delegate { AssignTractorBeamToken(ship); }
                });
            }

            Triggers.RegisterTrigger(new Trigger()
            {
                Name = "Assign tractor beam to " + HostShip.PilotInfo.PilotName,
                TriggerType = TriggerTypes.OnCombatPhaseStart,
                TriggerOwner = HostShip.Owner.PlayerNo,
                EventHandler = delegate { AssignTractorBeamToken(HostShip); }
            });
        }

        protected void AssignTractorBeamToken(GenericShip bumpedShip)
        {
            Selection.ChangeActiveShip(HostShip);
            Messages.ShowInfo(HostShip.PilotInfo.PilotName + " assigns a Tractor Beam Token\nto " + bumpedShip.PilotInfo.PilotName);

            bumpedShip.Tokens.AssignToken(new Tokens.TractorBeamToken(bumpedShip, HostShip.Owner), Triggers.FinishTrigger);
        }
    }
}