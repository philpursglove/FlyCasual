using Content;
using Ship;
using SubPhases;
using System;
using System.Collections.Generic;
using Tokens;
using Upgrade;

namespace Ship
{
    namespace SecondEdition.LaatIGunship
    {
        public class Hound : LaatIGunship
        {
            public Hound() : base()
            {
                PilotInfo = new PilotCardInfo25
                (
                    "\"Hound\"",
                    "Vigilant Tracker",
                    Faction.Republic,
                    2,
                    6,
                    20,
                    isLimited: true,
                    abilityType: typeof(Abilities.SecondEdition.HoundAbility),
                    extraUpgradeIcons: new List<UpgradeType>
                    {
                        UpgradeType.Crew,
                        UpgradeType.Crew,
                        UpgradeType.Sensor,
                        UpgradeType.Gunner,
                        UpgradeType.Gunner,
                        UpgradeType.Modification,
                        UpgradeType.Missile,
                        UpgradeType.Missile,
                    },
                    tags: new List<Tags>
                    {
                        Tags.Clone
                    },
                    legality: new List<Legality> { Legality.StandardLegal, Legality.ExtendedLegal }
                );
            }
        }

        public class HoundXWA : Hound
        {
            public HoundXWA() : base()
            {
                (PilotInfo as PilotCardInfo25).Cost = 10;
                (PilotInfo as PilotCardInfo25).LoadoutValue = 5;
                (PilotInfo as PilotCardInfo25).LegalityInfo = new List<Legality> { Legality.XWA };
                (PilotInfo as PilotCardInfo25).ExtraUpgrades = new List<UpgradeType>
                {
                    UpgradeType.Crew,
                    UpgradeType.Crew,
                    UpgradeType.Gunner,
                    UpgradeType.Gunner,
                    UpgradeType.Modification,
                    UpgradeType.Missile,
                    UpgradeType.Missile,
                    UpgradeType.Torpedo
                };
            }
        }
    }
}

namespace Abilities.SecondEdition
{
    public class HoundAbility : GenericAbility
    {
        private Type TokenType;

        public override void ActivateAbility()
        {
            GenericShip.BeforeTokenIsAssignedGlobal += CheckAbility;
        }

        public override void DeactivateAbility()
        {
            GenericShip.BeforeTokenIsAssignedGlobal -= CheckAbility;
        }

        private void CheckAbility(GenericShip ship, GenericToken token)
        {
            if ((token is DepleteToken || token is StrainToken)
                && ship.Owner == HostShip.Owner
                && ship != HostShip
                && ship.ShipBase.Size == BaseSize.Small
                && HostShip.Tokens.CountTokensByType(token.GetType()) == 0
                && HostShip.ArcsInfo.HasShipInTurretArc(ship))
            {
                TargetShip = ship;
                TokenType = token.GetType();
                RegisterAbilityTrigger(TriggerTypes.OnBeforeTokenIsAssigned, ShowDecision);
            }
        }

        private void ShowDecision(object sender, EventArgs e)
        {
            var tokenName = TokenType == typeof(StrainToken)
                ? "Strain"
                : TokenType == typeof(DepleteToken)
                    ? "Deplete"
                    : throw new InvalidOperationException("Invalid token type: " + TokenType);

            AskToUseAbility(
                HostShip.PilotInfo.PilotName,
                NeverUseByDefault,
                UseAbility,
                descriptionLong: "Do you want to receive " + tokenName + " token instead of the friendly ship?",
                imageHolder: HostShip
            );
        }

        private void UseAbility(object sender, EventArgs e)
        {
            HostShip.Tokens.AssignToken(TargetShip.Tokens.TokenToAssign, delegate
            {
                TargetShip.Tokens.TokenToAssign = null;
                TargetShip = null;
                TokenType = null;
                DecisionSubPhase.ConfirmDecision();
            });
        }

    }
}
