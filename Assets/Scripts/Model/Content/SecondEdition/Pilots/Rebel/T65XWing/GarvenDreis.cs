using Abilities.SecondEdition;
using BoardTools;
using Content;
using Ship;
using SubPhases;
using System.Collections.Generic;
using Tokens;
using Upgrade;

namespace Ship
{
    namespace SecondEdition.T65XWing
    {
        public class GarvenDreis : T65XWing
        {
            public GarvenDreis() : base()
            {
                PilotInfo = new PilotCardInfo25
                (
                    "Garven Dreis",
                    "Red Leader",
                    Faction.Rebel,
                    4,
                    5,
                    16,
                    isLimited: true,
                    abilityType: typeof(GarvenDreisXWingAbility),
                    extraUpgradeIcons: new List<UpgradeType>
                    {
                        UpgradeType.Talent,
                        UpgradeType.Astromech,
                        UpgradeType.Modification,
                        UpgradeType.Torpedo,
                        UpgradeType.Configuration
                    },
                    tags: new List<Tags>
                    {
                        Tags.XWing
                    },
                    seImageNumber: 4,
                    legality: new List<Legality> { Legality.StandardLegal, Legality.ExtendedLegal }
                );

                PilotNameCanonical = "garvendreis-t65xwing";
            }
        }

        public class GarvenDreisXWA : GarvenDreis
        {
            public GarvenDreisXWA() : base()
            {
                (PilotInfo as PilotCardInfo25).Cost = 4;
                (PilotInfo as PilotCardInfo25).LoadoutValue = 6;
                (PilotInfo as PilotCardInfo25).LegalityInfo = new List<Legality> { Legality.XWA };
            }
        }
    }
}

namespace Abilities.SecondEdition
{
    public class GarvenDreisXWingAbility : GenericAbility
    {
        public override void ActivateAbility()
        {
            HostShip.OnTokenIsSpent += RegisterGarvenDreisPilotAbility;
        }

        public override void DeactivateAbility()
        {
            HostShip.OnTokenIsSpent -= RegisterGarvenDreisPilotAbility;
        }

        private void RegisterGarvenDreisPilotAbility(GenericShip ship, GenericToken token)
        {
            if (token is FocusToken)
            {
                RegisterAbilityTrigger(TriggerTypes.OnTokenIsSpent, StartSubphaseForGarvenDreisPilotAbility);
            }
        }

        private void StartSubphaseForGarvenDreisPilotAbility(object sender, System.EventArgs e)
        {
            if (HasFriendlyShipsInRange())
            {
                SelectTargetForAbility(
                    SelectGarvenDreisAbilityTarget,
                    FilterAbilityTarget,
                    GetAiAbilityPriority,
                    HostShip.Owner.PlayerNo,
                    HostShip.PilotInfo.PilotName,
                    "Choose another ship to assign Focus token to it",
                    HostShip
                );
            }
            else
            {
                Triggers.FinishTrigger();
            }
        }

        private bool HasFriendlyShipsInRange()
        {
            foreach (GenericShip friendlyShip in HostShip.Owner.Ships.Values)
            {
                if (friendlyShip.ShipId != HostShip.ShipId)
                {
                    DistanceInfo distInfo = new DistanceInfo(HostShip, friendlyShip);
                    if (distInfo.Range >= 1 && distInfo.Range <= 3) return true;
                }
            }

            return false;
        }

        protected virtual bool FilterAbilityTarget(GenericShip ship)
        {
            return FilterByTargetType(ship, new List<TargetTypes>() { TargetTypes.OtherFriendly }) && FilterTargetsByRange(ship, 1, 3);
        }

        private int GetAiAbilityPriority(GenericShip ship)
        {
            int result = 0;
            int shipFocusTokens = ship.Tokens.CountTokensByType(typeof(FocusToken));
            if (shipFocusTokens == 0) result += 100;
            result += (5 - shipFocusTokens);
            return result;
        }

        private void SelectGarvenDreisAbilityTarget()
        {
            MovementTemplates.ReturnRangeRuler();

            TargetShip.Tokens.AssignToken(typeof(FocusToken), SelectShipSubPhase.FinishSelection);
        }
    }
}
