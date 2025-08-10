using ActionsList;
using Content;
using Ship;
using System.Collections.Generic;
using Tokens;
using Upgrade;

namespace Ship
{
    namespace SecondEdition.SheathipedeClassShuttle
    {
        public class AP5 : SheathipedeClassShuttle
        {
            public AP5() : base()
            {
                PilotInfo = new PilotCardInfo25
                (
                    "AP-5",
                    "Escaped Analyst Droid",
                    Faction.Rebel,
                    1,
                    3,
                    5,
                    isLimited: true,
                    abilityType: typeof(Abilities.SecondEdition.AP5PilotAbility),
                    extraUpgradeIcons: new List<UpgradeType>
                    {
                        UpgradeType.Crew,
                        UpgradeType.Astromech,
                        UpgradeType.Modification,
                        UpgradeType.Title
                    },
                    tags: new List<Tags>
                    {
                        Tags.Spectre,
                        Tags.Droid
                    },
                    seImageNumber: 41,
                    legality: new List<Legality> { Legality.StandardLegal, Legality.ExtendedLegal }
                );

                ShipInfo.ActionIcons.SwitchToDroidActions();
            }
        }

        public class AP5XWA : AP5
        {
            public AP5XWA() : base()
            {
                (PilotInfo as PilotCardInfo25).Cost = 3;
                (PilotInfo as PilotCardInfo25).LoadoutValue = 7;
                (PilotInfo as PilotCardInfo25).ExtraUpgrades = new List<UpgradeType>
                {
                    UpgradeType.Astromech,
                    UpgradeType.Crew,
                    UpgradeType.Modification,
                    UpgradeType.Title
                };
                (PilotInfo as PilotCardInfo25).LegalityInfo = new List<Legality> { Legality.XWA };
            }
        }
    }
}

namespace Abilities.SecondEdition
{
    public class AP5PilotAbility : GenericAbility
    {
        private GenericShip SelectedShip;

        public override void ActivateAbility()
        {
            HostShip.OnCoordinateTargetIsSelected += CheckUseAbility;
        }

        public override void DeactivateAbility()
        {
            HostShip.OnCoordinateTargetIsSelected -= CheckUseAbility;
        }

        private void CheckUseAbility(GenericShip ship)
        {
            SelectedShip = ship;

            SelectedShip.OnCheckCanPerformActionsWhileStressed += ConfirmThatIsPossible;
            SelectedShip.OnCanPerformActionWhileStressed += AllowIfOneOrLessStressTokens;

            SelectedShip.OnActionIsPerformed += RemoveEffect;
        }

        private void RemoveEffect(GenericAction action)
        {
            SelectedShip.OnActionIsPerformed -= RemoveEffect;

            SelectedShip.OnCheckCanPerformActionsWhileStressed -= ConfirmThatIsPossible;
            SelectedShip.OnCanPerformActionWhileStressed -= AllowIfOneOrLessStressTokens;

            SelectedShip = null;
        }

        private void ConfirmThatIsPossible(ref bool isAllowed)
        {
            AllowIfOneOrLessStressTokens(null, ref isAllowed);
        }

        private void AllowIfOneOrLessStressTokens(GenericAction action, ref bool isAllowed)
        {
            isAllowed = SelectedShip.Tokens.CountTokensByType(typeof(StressToken)) <= 1;
        }
    }
}
