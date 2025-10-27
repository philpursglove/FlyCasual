using Content;
using System.Collections.Generic;
using Upgrade;

namespace Ship.SecondEdition.T70XWing
{
    public class JophSeastriker : T70XWing
    {
        public JophSeastriker() : base()
        {
            PilotInfo = new PilotCardInfo25
            (
                "Joph Seastriker",
                "Reckless Bodyguard",
                Faction.Resistance,
                3,
                5,
                13,
                isLimited: true,
                abilityType: typeof(Abilities.SecondEdition.JophSeastrikerAbility),
                extraUpgradeIcons: new List<UpgradeType>
                {
                    UpgradeType.Talent,
                    UpgradeType.Tech,
                    UpgradeType.Astromech,
                    UpgradeType.Modification,
                    UpgradeType.Configuration
                },
                tags: new List<Tags>
                {
                    Tags.XWing
                },
                legality: new List<Legality> { Legality.StandardLegal, Legality.ExtendedLegal }
            );
        }
    }

    public class JophSeastrikerXWA : JophSeastriker
    {
        public JophSeastrikerXWA() : base()
        {
            (PilotInfo as PilotCardInfo25).Cost = 11;
            (PilotInfo as PilotCardInfo25).LoadoutValue = 10;
            (PilotInfo as PilotCardInfo25).ExtraUpgrades = new List<UpgradeType>()
            {
                UpgradeType.Astromech,
                UpgradeType.Modification,
                UpgradeType.Tech,
                UpgradeType.Configuration
            };
            (PilotInfo as PilotCardInfo25).LegalityInfo = new List<Legality> { Legality.XWA };
        }
    }
}

namespace Abilities.SecondEdition
{
    public class JophSeastrikerAbility : GenericAbility
    {

        public override void ActivateAbility()
        {
            HostShip.OnShieldLost += RegisterJophSeastrikerAbility;
        }

        public override void DeactivateAbility()
        {
            HostShip.OnShieldLost -= RegisterJophSeastrikerAbility;
        }

        private void RegisterJophSeastrikerAbility()
        {
            RegisterAbilityTrigger(TriggerTypes.OnShieldIsLost, GetEvadeToken);
        }

        private void GetEvadeToken(object sender, System.EventArgs e)
        {
            Messages.ShowInfo(HostShip.PilotInfo.PilotName + " gains Evade token");
            HostShip.Tokens.AssignToken(typeof(Tokens.EvadeToken), Triggers.FinishTrigger);
        }
    }
}