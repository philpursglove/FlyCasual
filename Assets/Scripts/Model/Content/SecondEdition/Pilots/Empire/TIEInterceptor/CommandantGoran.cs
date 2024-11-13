using Abilities.SecondEdition;
using BoardTools;
using Content;
using Ship;
using SubPhases;
using System;
using System.Collections.Generic;
using System.Linq;
using Tokens;
using UnityEngine;
using Upgrade;

namespace Ship.SecondEdition.TIEInterceptor
{
    public class CommandantGoran : TIEInterceptor
    {
        public CommandantGoran() : base()
        {
            PilotInfo = new PilotCardInfo25
            (
                "Commandant Goran",
                "Skystrike Superintendent",
                Faction.Imperial,
                4,
                4,
                8,
                isLimited: true,
                abilityType: typeof(CommandantGoranAbility),
                extraUpgradeIcons: new List<UpgradeType>()
                {
                    UpgradeType.Talent,
                    UpgradeType.Modification,
                    UpgradeType.Configuration
                },
                tags: new List<Tags>
                {
                    Tags.Tie
                },
                skinName: "Skystrike Academy"
            );

            PilotNameCanonical = "commandantgoran";

            ImageUrl = "https://infinitearenas.com/xw2/images/pilots/commandantgoran.png";
        }
    }
}

namespace Abilities.SecondEdition
{
    public class CommandantGoranAbility : GenericAbility
    {
        public override void ActivateAbility()
        {
            Phases.Events.OnCombatPhaseStart_Triggers += RegisterAbility;
        }

        public override void DeactivateAbility()
        {
            Phases.Events.OnCombatPhaseStart_Triggers -= RegisterAbility;
        }

        public void RegisterAbility()
        {
            RegisterAbilityTrigger(TriggerTypes.OnCombatPhaseStart, AskUseAbility);
        }

        public void AskUseAbility(object sender, EventArgs e)
        {
            if (Board.GetShipsAtRange(HostShip, new Vector2(0, 2), Team.Type.Friendly).Count > 0)
            {
                SelectTargetForAbility
                (
                    AssignEvadeToken,
                    MeetsCriteria,
                    GetAiPriority,
                    HostShip.Owner.PlayerNo,
                    HostShip.PilotInfo.PilotName,
                    "You may assign an evade and remove a non-stress red token."
                );
            }
            else
            {
                Messages.ShowInfo($"{HostShip.PilotInfo.PilotName}: There are no targets for ability");
                Triggers.FinishTrigger();
            }
        }

        public void AssignEvadeToken()
        {
            TargetShip.Tokens.AssignToken(new EvadeToken(TargetShip), RemoveRedToken);
        }

        public void RemoveRedToken()
        {
            List<GenericToken> redtokens = (List<GenericToken>)TargetShip.Tokens.GetTokensByColor(TokenColors.Red).Where(t => t.GetType() != typeof(StressToken));

            if (redtokens.Count > 0)
            {
                DecisionSubPhase pilotAbilityDecision = (DecisionSubPhase)Phases.StartTemporarySubPhaseNew(
                    HostShip.PilotName,
                    typeof(CommandantgoranDecisionSubphase),
                    Triggers.FinishTrigger
                );

                pilotAbilityDecision.DescriptionShort = "Commandant Goran Pilot Ability";
                pilotAbilityDecision.DescriptionLong = "Assign an Evade token and remove one non-stress red token.";
                pilotAbilityDecision.ImageSource = HostShip;

                pilotAbilityDecision.RequiredPlayer = HostShip.Owner.PlayerNo;

                foreach (var Token in redtokens)
                {
                    string name = Token.Name;
                    if (Token.GetType() == typeof(RedTargetLockToken))
                    {
                        RedTargetLockToken targetLockToken = (RedTargetLockToken)Token;
                        name = Token.Name + " " + targetLockToken.Letter;
                    }
                    pilotAbilityDecision.AddDecision(name, delegate { HostShip.Tokens.RemoveToken(Token, DecisionSubPhase.ConfirmDecision); });
                }

                pilotAbilityDecision.ShowSkipButton = true;
                pilotAbilityDecision.Start();
            }
            else
            {
                Triggers.FinishTrigger();
            }
        }

        public bool MeetsCriteria(GenericShip ship)
        {
            DistanceInfo distInfo = new DistanceInfo(HostShip, ship);
            
            bool isValid = (distInfo.Range <= 2 && ship.State.Initiative < HostShip.State.Initiative);

            if(!isValid) {
                Messages.ShowInfoToHuman("Choose a friendly target with a lower initiave between range 0 and 3");
            }

            return isValid;
        }

        public int GetAiPriority(GenericShip ship)
        {
            return ship.PilotInfo.Cost;
        }

        private class CommandantgoranDecisionSubphase : DecisionSubPhase { }
    }
}