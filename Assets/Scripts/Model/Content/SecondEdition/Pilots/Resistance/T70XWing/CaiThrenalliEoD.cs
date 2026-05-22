using ActionsList;
using Content;
using System.Collections.Generic;
using Upgrade;
using Actions;
using UpgradesList.SecondEdition;
using BoardTools;
using System;
using Movement;
using System.Linq;
using Tokens;


namespace Ship.SecondEdition.T70XWing
{
    public class CaiThrenalliEoD : T70XWingEoD
    {
        public CaiThrenalliEoD() : base()
        {
            PilotInfo = new PilotCardInfo25
            (
                "C’ai Threnalli",
                "Evacuation of D'Qar",
                Faction.Resistance,
                4,
                11,
                0,
                isLimited: true,
                abilityType: typeof(Abilities.SecondEdition.CaiThrenalliAbility),
                extraUpgradeIcons: new List<UpgradeType>
                {
                    UpgradeType.Talent,
                    UpgradeType.Talent,
                    UpgradeType.Astromech,
                    UpgradeType.Modification,
                },
                tags: new List<Tags>
                {
                    Tags.XWing
                },
                legality: new List<Legality> { Legality.XWA },
                isStandardLayout: true
            );

            MustHaveUpgrades.Add(typeof(ForTheCause));
            MustHaveUpgrades.Add(typeof(Heroic));
            MustHaveUpgrades.Add(typeof(BBAstromech));
            MustHaveUpgrades.Add(typeof(RepulsorliftEngines));

            PilotNameCanonical = "caithrenalli-evacuationofdqar";
        }
    }
}

namespace UpgradesList.SecondEdition
{
    public class RepulsorliftEngines : GenericUpgrade
    {
        public RepulsorliftEngines() : base()
        {
            UpgradeInfo = new UpgradeCardInfo(
                "Repulsorlift Engines",
                UpgradeType.Modification,
                abilityType: typeof(Abilities.SecondEdition.RepulsorliftEnginesAbility),
                legalityInfo: new() { Legality.StandardLegal, Legality.ExtendedLegal, Legality.XWA }
            );
            IsHidden = true;
        }
    }
}

namespace Abilities.SecondEdition
{
    // While you perform a Barrel Roll action, you may gain 1 strain token to use the bank-left or bank-right template instead of the straight template.
    public class RepulsorliftEnginesAbility : GenericAbility
    {
        public override void ActivateAbility()
        {
            HostShip.OnGetAvailableBarrelRollTemplates += AddBarrelRollTemplates;
            HostShip.OnActionIsPerformed += CheckForGainStrain;
        }

        public override void DeactivateAbility()
        {
            HostShip.OnGetAvailableBarrelRollTemplates -= AddBarrelRollTemplates;
            HostShip.OnActionIsPerformed -= CheckForGainStrain;
        }

        private void AddBarrelRollTemplates(List<ManeuverTemplate> availableTemplates, GenericAction action)
        {
            /// I don't think these check are nessisary for Cai Threnalli EoD.
            if (availableTemplates.Any(n => n.Name == "Straight 1"))
            {
                availableTemplates.Add(new ManeuverTemplate(ManeuverBearing.Bank, ManeuverDirection.Left, ManeuverSpeed.Speed1));
                availableTemplates.Add(new ManeuverTemplate(ManeuverBearing.Bank, ManeuverDirection.Right, ManeuverSpeed.Speed1));
            }
            if (availableTemplates.Any(n => n.Name == "Straight 2"))
            {
                availableTemplates.Add(new ManeuverTemplate(ManeuverBearing.Bank, ManeuverDirection.Left, ManeuverSpeed.Speed2));
                availableTemplates.Add(new ManeuverTemplate(ManeuverBearing.Bank, ManeuverDirection.Right, ManeuverSpeed.Speed2));
            }
            if (availableTemplates.Any(n => n.Name == "Straight 3"))
            {
                availableTemplates.Add(new ManeuverTemplate(ManeuverBearing.Bank, ManeuverDirection.Left, ManeuverSpeed.Speed3));
                availableTemplates.Add(new ManeuverTemplate(ManeuverBearing.Bank, ManeuverDirection.Right, ManeuverSpeed.Speed3));
            }
        }

        private void CheckForGainStrain(GenericAction action)
        {
            if (action is BarrelRollAction)
            {
                if ((action as BarrelRollAction).SelectedTemplate.Name == "Bank 1 Left" ||
                    (action as BarrelRollAction).SelectedTemplate.Name == "Bank 2 Left" ||
                    (action as BarrelRollAction).SelectedTemplate.Name == "Bank 3 Left" ||
                    (action as BarrelRollAction).SelectedTemplate.Name == "Bank 1 Right" ||
                    (action as BarrelRollAction).SelectedTemplate.Name == "Bank 2 Right" ||
                    (action as BarrelRollAction).SelectedTemplate.Name == "Bank 3 Right"
                )
                {
                    RegisterAbilityTrigger(TriggerTypes.OnFreeAction, GainStrain);
                }
            }
        }

        private void GainStrain(object sender, EventArgs e)
        {
            HostShip.Tokens.AssignToken(new StrainToken(HostShip),Triggers.FinishTrigger);
        }
    }
}