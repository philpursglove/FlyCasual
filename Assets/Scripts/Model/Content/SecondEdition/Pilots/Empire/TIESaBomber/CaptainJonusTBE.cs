using Content;
using System.Collections.Generic;
using Upgrade;
using UpgradesList.SecondEdition;

namespace Ship
{
    namespace SecondEdition.TIESaBomber
    {
        public class CaptainJonusTBE : TIESaBomber
        {
            public CaptainJonusTBE()
            {
                PilotInfo = new PilotCardInfo25(
                    "Captain Jonus",
                    "Top Cover",
                    Faction.Imperial,
                    4,
                    5,
                    loadoutValue: 0,
                    isStandardLayout: true,
                    tags: new List<Tags>
                    {
                        Tags.Tie
                    },
                    isLimited: true,
                    abilityType: typeof(Abilities.SecondEdition.CaptainJonusTBEAbility),
                    extraUpgradeIcons: new List<UpgradeType>
                    {
                        UpgradeType.Talent,
                        UpgradeType.Missile,
                        UpgradeType.Missile,
                        UpgradeType.Device
                    }
                );
                PilotNameCanonical = "captainjonus-swz98";

                MustHaveUpgrades.Add(typeof(BarrageRockets));
                MustHaveUpgrades.Add(typeof(ProtonBombs));
                MustHaveUpgrades.Add(typeof(TopCover));

                ImageUrl = "https://infinitearenas.com/xw2/images/quickbuilds/captainjonus-swz98.png";
            }
        }
    }
}

namespace Abilities.SecondEdition
{
    //After you drop or launch a device, gain an evade token.
    public class CaptainJonusTBEAbility : GenericAbility
    {
        public override void ActivateAbility()
        {
            HostShip.OnBombWasDropped += CheckAbilityOnDrop;
            HostShip.OnBombWasLaunched += CheckAbilityOnLaunch;
        }

        public override void DeactivateAbility()
        {
            HostShip.OnBombWasDropped -= CheckAbilityOnDrop;
            HostShip.OnBombWasLaunched -= CheckAbilityOnLaunch;
        }

        private void CheckAbilityOnDrop()
        {
            RegisterAbilityTrigger(TriggerTypes.OnBombWasDropped, GetEvadeToken);
        }

        private void CheckAbilityOnLaunch()
        {
            RegisterAbilityTrigger(TriggerTypes.OnBombWasLaunched, GetEvadeToken);
        }

        private void GetEvadeToken(object sender, System.EventArgs e)
        {
            Messages.ShowInfo(HostShip.PilotInfo.PilotName + " gains Evade token");
            HostShip.Tokens.AssignToken(typeof(Tokens.EvadeToken), Triggers.FinishTrigger);
        }
    }
}