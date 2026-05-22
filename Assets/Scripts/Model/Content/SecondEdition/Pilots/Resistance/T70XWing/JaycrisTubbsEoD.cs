using Abilities.SecondEdition;
using ActionsList;
using Content;
using Movement;
using Ship;
using SubPhases;
using System;
using System.Collections.Generic;
using Tokens;
using Upgrade;
using UpgradesList.SecondEdition;
using Actions;

namespace Ship.SecondEdition.T70XWing
{
    public class JaycrisTubbsEoD : T70XWingEoD
    {
        public JaycrisTubbsEoD() : base()
        {
            PilotInfo = new PilotCardInfo25
            (
                "Jaycris Tubbs",
                "Evacuation of D'Qar",
                Faction.Resistance,
                1,
                10,
                0,
                isLimited: true,
                abilityType: typeof(Abilities.SecondEdition.JaycrisTubbsAbility),
                extraUpgradeIcons: new List<UpgradeType>
                {
                    UpgradeType.Talent,
                    UpgradeType.Astromech,
                },
                tags: new List<Tags>
                {
                    Tags.XWing
                },
                legality: new List<Legality> { Legality.XWA },
                isStandardLayout: true
            );
            MustHaveUpgrades.Add(typeof(ForTheCause));
            MustHaveUpgrades.Add(typeof(ROAstromech));

            PilotNameCanonical = "jaycristubbs-evacuationofdqar";
        }
    }
}

namespace UpgradesList.SecondEdition
{
    public class ROAstromech : GenericUpgrade
    {
        public ROAstromech() : base()
        {
            UpgradeInfo = new UpgradeCardInfo(
                "RO Astromech",
                UpgradeType.Astromech,
                abilityType: typeof(Abilities.SecondEdition.ROAstromechAbility),
                legalityInfo: new() { Legality.StandardLegal, Legality.ExtendedLegal, Legality.XWA }
            );
            IsHidden = true;
        }
    }
}

namespace Abilities.SecondEdition
{
    // After you perform a Barrel Roll action, gain a deplete token.
    // While you perform an attack, before rolling attack dice, if the defender is in your bullseye, you may remove 1 deplete token.
    public class AdaptiveSFoils : GenericAbility
    {
        public override void ActivateAbility()
        {
            HostShip.OnActionIsPerformed += CheckActionAbility;
            HostShip.OnAttackStartAsAttacker += AdaptiveSFoilsAttackAbility;
        }

        public override void DeactivateAbility()
        {
            HostShip.OnActionIsPerformed -= CheckActionAbility;
            HostShip.OnAttackStartAsAttacker -= AdaptiveSFoilsAttackAbility;
        }

        private void CheckActionAbility(GenericAction action)
        {
            if (action is BarrelRollAction)
            {
                RegisterAbilityTrigger(TriggerTypes.OnMovementFinish, GainDeplete);
            }
        }

        private void GainDeplete(object sender, EventArgs e)
        {
            HostShip.Tokens.AssignToken(new DepleteToken(HostShip),Triggers.FinishTrigger);
        }

        private void AdaptiveSFoilsAttackAbility()
        {
            if (HostShip.SectorsInfo.IsShipInSector(Combat.Defender, Arcs.ArcType.Bullseye))
            {
                HostShip.Tokens.RemoveToken(typeof(DepleteToken),()=>{});
            }
        }
    }

    // After you reveal a basic maneuver, you may reduce its difficulty.
    // If you do, after the Check Difficulty step, gain 1 strain token.
    public class ROAstromechAbility : GenericAbility
    {
        public override void ActivateAbility()
        {
            HostShip.OnManeuverIsRevealed += CheckManeuverForAbility;
        }

        public override void DeactivateAbility()
        {
            HostShip.OnManeuverIsRevealed -= CheckManeuverForAbility;
        }

        private void CheckManeuverForAbility(GenericShip ship)
        {
            if (HostShip.AssignedManeuver.IsBasicManeuver
                && (HostShip.AssignedManeuver.ColorComplexity == Movement.MovementComplexity.Normal || HostShip.AssignedManeuver.ColorComplexity == Movement.MovementComplexity.Complex)
            )
            {
                RegisterAbilityTrigger(TriggerTypes.OnManeuverIsRevealed, RegisterROAstromechAbility);
            }
        }

        private void RegisterROAstromechAbility(object sender, EventArgs e)
        {
            AskToUseAbility
            (
                HostUpgrade.UpgradeInfo.Name,
                AlwaysUseByDefault,
                UseROAstromechAbility,
                descriptionLong: "Do you want to reduce the difficulty of your maneuver? If you do, gain 1 Strain token after the check difficulty step.",
                imageHolder: HostUpgrade
            );
        }

        private void UseROAstromechAbility(object sender, EventArgs e)
        {
            HostShip.AssignedManeuver.ColorComplexity = GenericMovement.ReduceComplexity(HostShip.AssignedManeuver.ColorComplexity);
            Triggers.FinishTrigger();
            HostShip.OnMovementFinish += GainStrain;
        }

        private void GainStrain(GenericShip ship)
        {
            HostShip.OnMovementFinish -= GainStrain;
            HostShip.Tokens.AssignToken(new StrainToken(HostShip),()=>{});
        }
    }
}
