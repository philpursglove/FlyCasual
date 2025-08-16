using Content;
using Ship;
using System;
using System.Collections.Generic;
using Tokens;
using Upgrade;

namespace Ship.SecondEdition.Delta7Aethersprite
{
    public class ObiWanKenobi : Delta7Aethersprite
    {
        public ObiWanKenobi()
        {
            PilotInfo = new PilotCardInfo25
            (
                "Obi-Wan Kenobi",
                "Guardian of the Republic",
                Faction.Republic,
                5,
                5,
                10,
                true,
                force: 3,
                abilityType: typeof(Abilities.SecondEdition.ObiWanKenobiAbility),
                extraUpgradeIcons: new List<UpgradeType>
                {
                    UpgradeType.ForcePower,
                    UpgradeType.ForcePower,
                    UpgradeType.Talent,
                    UpgradeType.Astromech,
                    UpgradeType.Configuration,
                    UpgradeType.Modification,
                    UpgradeType.Modification
                },
                tags: new List<Tags>
                {
                    Tags.Jedi,
                    Tags.LightSide
                },
                legality: new List<Legality> { Legality.StandardLegal, Legality.ExtendedLegal }
            );
        }
    }

    public class ObiWanKenobiXWA : ObiWanKenobi
    {
        public ObiWanKenobiXWA() : base()
        {
            (PilotInfo as PilotCardInfo25).Cost = 5;
            (PilotInfo as PilotCardInfo25).LoadoutValue = 10;
            (PilotInfo as PilotCardInfo25).ExtraUpgrades = new List<UpgradeType>
            {
                    UpgradeType.ForcePower,
                    UpgradeType.ForcePower,
                    UpgradeType.Astromech,
                    UpgradeType.Modification,
                    UpgradeType.Modification,
                    UpgradeType.Configuration
            };
            (PilotInfo as PilotCardInfo25).LegalityInfo = new List<Legality> { Legality.XWA };
        }
    }
}

namespace Abilities.SecondEdition
{
    //After a friendly ship at range 0-2 spends a focus token, you may spend force. If you do, that ship gains 1 focus token.
    public class ObiWanKenobiAbility : GenericAbility
    {
        public override void ActivateAbility()
        {
            GenericShip.OnTokenIsSpentGlobal += RegisterAbility;
        }

        public override void DeactivateAbility()
        {
            GenericShip.OnTokenIsSpentGlobal -= RegisterAbility;
        }

        private void RegisterAbility(GenericShip ship, GenericToken token)
        {
            if (HostShip.State.Force > 0 
                && ship.Owner == HostShip.Owner 
                && token is FocusToken
                && new BoardTools.DistanceInfo(ship, HostShip).Range < 3)
            {
                TargetShip = ship;
                RegisterAbilityTrigger(TriggerTypes.OnTokenIsSpent, AskToUseObiWanAbility);
            }
        }

        private void AskToUseObiWanAbility(object sender, EventArgs e)
        {
            AskToUseAbility(
                HostShip.PilotInfo.PilotName,
                AlwaysUseByDefault,
                UseAbility,
                descriptionLong: "Do you want to spend 1 Force? (If you do, that ship gains 1 Focus Token)",
                imageHolder: HostShip
            );
        }

        private void UseAbility(object sender, EventArgs e)
        {
            Messages.ShowInfo(HostShip.PilotInfo.PilotName + ": " + TargetShip.PilotInfo.PilotName + " gains Focus token");
            HostShip.State.SpendForce(
                1,
                delegate {
                    TargetShip.Tokens.AssignToken(
                        new Tokens.FocusToken(TargetShip),
                        SubPhases.DecisionSubPhase.ConfirmDecision
                    );
                }
            );
            
        }
    }
}
