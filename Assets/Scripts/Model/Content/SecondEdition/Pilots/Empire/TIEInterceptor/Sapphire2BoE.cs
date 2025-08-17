using System.Collections.Generic;
using Abilities.SecondEdition;
using System.Linq;
using Content;
using Tokens;
using Upgrade;

namespace Ship
{
    namespace SecondEdition.TIEInterceptor
    {
        public class Sapphire2BoE : TIEInterceptor
        {
            public Sapphire2BoE()
            {
                PilotInfo = new PilotCardInfo25(
                    "Sapphire 2",
                    "Battle Over Endor",
                    Faction.Imperial,
                    1,
                    4,
                    1,
                    extraUpgradeIcons:new List<UpgradeType>()
                    {
                        UpgradeType.Talent,
                        UpgradeType.Talent,
                        UpgradeType.Modification,
                        UpgradeType.Tech
                    },
                    isLimited: true,
                    isStandardLayout: true,
                    abilityType: typeof(Sapphire2Ability),
                    tags: new List<Tags>
                    {
                        Tags.Tie
                    }

                );
                PilotNameCanonical = "sapphire2-battleoverendor";
                ShipInfo.UpgradeIcons.Upgrades.Remove(UpgradeType.Configuration);
                AutoThrustersAbility oldAbility = (AutoThrustersAbility)ShipAbilities.First(n => n.GetType() == typeof(AutoThrustersAbility));
                ShipAbilities.Remove(oldAbility);
                ShipAbilities.Add(new SensitiveControlsRealAbility());

                MustHaveUpgrades.Add(typeof(NoEscapeAbility));
                MustHaveUpgrades.Add(typeof(TargetingMatrixAbility));
                MustHaveUpgrades.Add(typeof(PrimedThrustersAbility));


            }
        }
    }
}

namespace Abilities.SecondEdition
{
    //While you defend, if you are focused, roll 1 additional defense die.
    public class Sapphire2Ability : GenericAbility
    {
        public override void ActivateAbility()
        {
            HostShip.AfterGotNumberOfDefenceDice += CheckDefenseBonus;
        }

        public override void DeactivateAbility()
        {
            HostShip.AfterGotNumberOfDefenceDice -= CheckDefenseBonus;
        }

        private void CheckDefenseBonus(ref int count)
        {
            if (HostShip.Tokens.HasToken(typeof(FocusToken)))
            {
                Messages.ShowInfo($"{HostShip.PilotInfo.PilotName} rolls 1 additional defense die");
                count++;
            }
        }
    }
}