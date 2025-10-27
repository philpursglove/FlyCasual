using Content;
using SubPhases;
using System;
using System.Collections.Generic;
using Tokens;
using Upgrade;

namespace Ship
{
    namespace SecondEdition.RogueClassStarfighter
    {
        public class CadBaneScum : RogueClassStarfighter
        {
            public CadBaneScum() : base()
            {
                PilotInfo = new PilotCardInfo25
                (
                    "Cad Bane",
                    "Infamous Bounty Hunter",
                    Faction.Scum,
                    4,
                    4,
                    13,
                    isLimited: true,
                    charges: 2,
                    regensCharges: 1,
                    abilityType: typeof(Abilities.SecondEdition.CadBaneScumAbility),
                    extraUpgradeIcons: new List<UpgradeType>
                    {
                        UpgradeType.Talent,
                        UpgradeType.Illicit,
                        UpgradeType.Illicit,
                        UpgradeType.Modification,
                        UpgradeType.Cannon,
                        UpgradeType.Cannon,
                        UpgradeType.Missile,
                        UpgradeType.Title
                    },
                    tags: new List<Tags>()
                    {
                        Tags.BountyHunter
                    },
                    legality: new List<Legality> { Legality.StandardLegal, Legality.ExtendedLegal }
                );

                PilotNameCanonical = "cadbane";
            }
        }

        public class CadBaneScumXWA : CadBaneScum
        {
            public CadBaneScumXWA() : base()
            {
                (PilotInfo as PilotCardInfo25).Cost = 12;
                (PilotInfo as PilotCardInfo25).LoadoutValue = 20;
                (PilotInfo as PilotCardInfo25).LegalityInfo = new List<Legality> { Legality.XWA };
                (PilotInfo as PilotCardInfo25).ExtraUpgrades = new List<UpgradeType>
                {
                    UpgradeType.Talent,
                    UpgradeType.Illicit,
                    UpgradeType.Modification,
                    UpgradeType.Modification,
                    UpgradeType.Cannon,
                    UpgradeType.Cannon,
                    UpgradeType.Missile,
                    UpgradeType.Title
                };
            }
        }
    }
}

namespace Abilities.SecondEdition
{
    public class CadBaneScumAbility : GenericAbility
    {
        public override void ActivateAbility()
        {
            HostShip.OnAttackHitAsAttacker += RegisterHitAbility;
        }

        public override void DeactivateAbility()
        {
            HostShip.OnAttackHitAsAttacker -= RegisterHitAbility;
        }

        private void RegisterHitAbility()
        {
            RegisterAbilityTrigger(TriggerTypes.OnAttackHit, UseChargesToPerformAction);
        }

        private void UseChargesToPerformAction(object sender, EventArgs e)
        {
            if (HostShip.State.Charges > 1 && HostShip.Tokens.GetNonLockRedOrangeTokens().Count > 0)
            {
                AskToUseAbility(
                    HostShip.PilotInfo.PilotName,
                    NeverUseByDefault,
                    AgreeToTransferToken,
                    descriptionLong: "Do you want to spend 2 charges to transfer a red or orange token?",
                    imageHolder: HostShip
                );
            }
            else
            {
                Triggers.FinishTrigger();
            }
        }

        private void AgreeToTransferToken(object sender, EventArgs e)
        {
            SelectTokenToReassignSubphase subphase = Phases.StartTemporarySubPhaseNew<SelectTokenToReassignSubphase>(
                "Reassign Token",
                DecisionSubPhase.ConfirmDecision
            );

            subphase.Name = HostShip.PilotInfo.PilotName;
            subphase.DescriptionShort = "Select a token to transfer to the target";
            subphase.ImageSource = HostShip;

            subphase.DecisionOwner = HostShip.Owner;
            subphase.ShowSkipButton = true;

            HostShip.SpendCharges(2);

            foreach (GenericToken token in HostShip.Tokens.GetNonLockRedOrangeTokens())
            {
                subphase.AddDecision(
                    token.Name + ((token.GetType() == typeof(RedTargetLockToken)) ? " \"" + (token as RedTargetLockToken).Letter + "\"" : ""),
                    delegate { ActionsHolder.ReassignToken(token, HostShip, Combat.Defender, DecisionSubPhase.ConfirmDecision); }
                );
            }

            if (subphase.GetDecisions().Count > 0)
            {
                subphase.Start();
            }
            else
            {
                Phases.GoBack();
                Messages.ShowInfoToHuman("Cad Bane: No tokens to transfer to the target");
                Triggers.FinishTrigger();
            }
        }

        private class SelectTokenToReassignSubphase : DecisionSubPhase { }
    }
}