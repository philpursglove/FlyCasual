using Abilities.SecondEdition;
using ActionsList;
using Content;
using SubPhases;
using System.Collections.Generic;
using Upgrade;

namespace Ship
{
    namespace SecondEdition.T65XWing
    {
        public class KullbeeSperado : T65XWing
        {
            public KullbeeSperado() : base()
            {
                PilotInfo = new PilotCardInfo25
                (
                    "Kullbee Sperado",
                    "Enigmatic Gunslinger",
                    Faction.Rebel,
                    4,
                    4,
                    7,
                    isLimited: true,
                    abilityType: typeof(KullbeeSperadoAbility),
                    extraUpgradeIcons: new List<UpgradeType>
                    {
                        UpgradeType.Talent,
                        UpgradeType.Astromech,
                        UpgradeType.Missile,
                        UpgradeType.Illicit,
                        UpgradeType.Configuration
                    },
                    tags: new List<Tags>
                    {
                        Tags.Partisan,
                        Tags.XWing
                    },
                    seImageNumber: 6,
                    skinName: "Partisan",
                    legality: new List<Legality> { Legality.StandardLegal, Legality.ExtendedLegal }
                );
            }
        }

        public class KullbeeSperadoXWA : KullbeeSperado
        {
            public KullbeeSperadoXWA() : base()
            {
                (PilotInfo as PilotCardInfo25).LoadoutValue = 9;
                (PilotInfo as PilotCardInfo25).LegalityInfo = new List<Legality> { Legality.XWA };
            }
        }
    }
}

namespace Abilities.SecondEdition
{
    public class KullbeeSperadoAbility : GenericAbility
    {
        private GenericUpgrade SFoilsUpgrade;

        public override void ActivateAbility()
        {
            HostShip.OnActionIsPerformed += CheckKullbeeSperadoAbility;
        }

        public override void DeactivateAbility()
        {
            HostShip.OnActionIsPerformed -= CheckKullbeeSperadoAbility;
        }

        private void CheckKullbeeSperadoAbility(GenericAction action)
        {
            if (action is BoostAction || action is BarrelRollAction)
            {
                SFoilsUpgrade = GetSFoils();
                if (SFoilsUpgrade == null) return;

                RegisterAbilityTrigger(TriggerTypes.OnActionIsPerformed, AskToUseKullbeeSperadoAbility);
            }
        }

        private void AskToUseKullbeeSperadoAbility(object sender, System.EventArgs e)
        {
            SFoilsUpgrade = GetSFoils();
            if (SFoilsUpgrade != null)
            {
                AskToUseAbility(
                    HostShip.PilotInfo.PilotName,
                    NeverUseByDefault,
                    UseKullbeeSperadoAbility,
                    descriptionLong: "Do you want to flip your equipped \"Servomotor S-foils\" upgrade card?",
                    imageHolder: HostShip
                );
            }
            else
            {
                Triggers.FinishTrigger();
            }
        }

        private void UseKullbeeSperadoAbility(object sender, System.EventArgs e)
        {
            (SFoilsUpgrade as GenericDualUpgrade).Flip();
            DecisionSubPhase.ConfirmDecision();
        }

        private GenericUpgrade GetSFoils()
        {
            return HostShip.UpgradeBar.GetUpgradesOnlyFaceup().Find(n => n.UpgradeInfo.HasType(UpgradeType.Configuration));
        }
    }
}