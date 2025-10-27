using BoardTools;
using Content;
using Movement;
using System.Collections.Generic;
using System.Linq;
using Upgrade;

namespace Ship
{
    namespace SecondEdition.ScurrgH6Bomber
    {
        public class SolSixxa : ScurrgH6Bomber
        {
            public SolSixxa() : base()
            {
                PilotInfo = new PilotCardInfo25
                (
                    "Sol Sixxa",
                    "Cunning Commander",
                    Faction.Scum,
                    3,
                    5,
                    12,
                    isLimited: true,
                    abilityType: typeof(Abilities.SecondEdition.SolSixxaAbility),
                    extraUpgradeIcons: new List<UpgradeType>()
                    {
                        UpgradeType.Talent,
                        UpgradeType.Crew,
                        UpgradeType.Gunner,
                        UpgradeType.Modification,
                        UpgradeType.Device,
                        UpgradeType.Device,
                        UpgradeType.Turret
                    },
                    seImageNumber: 205,
                    legality: new List<Legality>() { Legality.ExtendedLegal }
                );
            }
        }

        public class SolSixxaXWA : SolSixxa
        {
            public SolSixxaXWA() : base()
            {
                (PilotInfo as PilotCardInfo25).Cost = 12;
                (PilotInfo as PilotCardInfo25).LoadoutValue = 16;
                (PilotInfo as PilotCardInfo25).LegalityInfo = new List<Legality> { Legality.XWA };
                (PilotInfo as PilotCardInfo25).ExtraUpgrades = new List<UpgradeType>()
                {
                    UpgradeType.Talent,
                    UpgradeType.Crew,
                    UpgradeType.Gunner,
                    UpgradeType.Illicit,
                    UpgradeType.Modification,
                    UpgradeType.Device,
                    UpgradeType.Device,
                    UpgradeType.Turret
                };
            }
        }
    }
}

namespace Abilities.SecondEdition
{
    public class SolSixxaAbility : Abilities.FirstEdition.SolSixxaAbiliity
    {


        protected override void SolSixxaTemplate(List<ManeuverTemplate> availableTemplates, GenericUpgrade upgrade)
        {
            base.SolSixxaTemplate(availableTemplates, upgrade);

            List<ManeuverTemplate> newTemplates = new List<ManeuverTemplate>()
            {
                new ManeuverTemplate(ManeuverBearing.Bank, ManeuverDirection.Right, ManeuverSpeed.Speed1, isBombTemplate: true),
                new ManeuverTemplate(ManeuverBearing.Bank, ManeuverDirection.Left, ManeuverSpeed.Speed1, isBombTemplate: true),
            };

            foreach (ManeuverTemplate newTemplate in newTemplates)
            {
                if (!availableTemplates.Any(t => t.Name == newTemplate.Name))
                {
                    availableTemplates.Add(newTemplate);
                }
            }
        }
    }
}

namespace Abilities.FirstEdition
{
    public class SolSixxaAbiliity : GenericAbility
    {
        public override void ActivateAbility()
        {
            HostShip.OnGetAvailableBombDropTemplatesTwoConditions += SolSixxaTemplate;
        }

        public override void DeactivateAbility()
        {
            HostShip.OnGetAvailableBombDropTemplatesTwoConditions -= SolSixxaTemplate;
        }

        protected virtual void SolSixxaTemplate(List<ManeuverTemplate> availableTemplates, GenericUpgrade upgrade)
        {
            if (availableTemplates.Any(n => n.Bearing == ManeuverBearing.Straight && n.Speed == ManeuverSpeed.Speed1))
            {
                List<ManeuverTemplate> newTemplates = new List<ManeuverTemplate>()
                {
                    new ManeuverTemplate(ManeuverBearing.Turn, ManeuverDirection.Right, ManeuverSpeed.Speed1, isBombTemplate: true),
                    new ManeuverTemplate(ManeuverBearing.Turn, ManeuverDirection.Left, ManeuverSpeed.Speed1, isBombTemplate: true),
                };

                foreach (ManeuverTemplate newTemplate in newTemplates)
                {
                    if (!availableTemplates.Any(t => t.Name == newTemplate.Name))
                    {
                        availableTemplates.Add(newTemplate);
                    }
                }
            }
        }
    }
}