using BoardTools;
using Content;
using Movement;
using System.Collections.Generic;
using System.Linq;
using Upgrade;

namespace Ship
{
    namespace SecondEdition.QuadrijetTransferSpacetug
    {
        public class ConstableZuvio : QuadrijetTransferSpacetug
        {
            public ConstableZuvio() : base()
            {
                PilotInfo = new PilotCardInfo25
                (
                    "Constable Zuvio",
                    "Missing Sheriff of Niima Outpost",
                    Faction.Scum,
                    4,
                    4,
                    13,
                    isLimited: true,
                    abilityType: typeof(Abilities.SecondEdition.ConstableZuvioAbility),
                    extraUpgradeIcons: new List<UpgradeType>()
                    {
                        UpgradeType.Talent,
                        UpgradeType.Illicit,
                        UpgradeType.Modification,
                        UpgradeType.Tech,
                        UpgradeType.Device,
                        UpgradeType.Device
                    },
                    seImageNumber: 161,
                    legality: new List<Legality>() { Legality.ExtendedLegal }
                );
            }
        }

        public class ConstableZuvioXWA : ConstableZuvio
        {
            public ConstableZuvioXWA() : base()
            {
                (PilotInfo as PilotCardInfo25).Cost = 10;
                (PilotInfo as PilotCardInfo25).LoadoutValue = 19;
                (PilotInfo as PilotCardInfo25).LegalityInfo = new List<Legality> { Legality.XWA };
                (PilotInfo as PilotCardInfo25).ExtraUpgrades = new List<UpgradeType>
                {
                    UpgradeType.Talent,
                    UpgradeType.Crew,
                    UpgradeType.Illicit,
                    UpgradeType.Modification,
                    UpgradeType.Device
                };
            }
        }
    }
}

namespace Abilities.SecondEdition
{
    public class ConstableZuvioAbility : GenericAbility
    {
        public override void ActivateAbility()
        {
            HostShip.OnGetAvailableBombLaunchTemplates += ConstableZuvioTemplate;
        }

        public override void DeactivateAbility()
        {
            HostShip.OnGetAvailableBombLaunchTemplates -= ConstableZuvioTemplate;
        }

        protected virtual void ConstableZuvioTemplate(List<ManeuverTemplate> availableTemplates, GenericUpgrade upgrade)
        {
            ManeuverTemplate newTemplate = new ManeuverTemplate(ManeuverBearing.Straight, ManeuverDirection.Forward, ManeuverSpeed.Speed1);

            if (!availableTemplates.Any(t => t.Name == newTemplate.Name))
            {
                availableTemplates.Add(newTemplate);
            }
        }
    }
}
