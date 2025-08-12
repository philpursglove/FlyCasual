using Abilities.Secondedition;
using Abilities.SecondEdition;
using Actions;
using ActionsList;
using Content;
using System.Collections.Generic;
using Upgrade;

namespace Ship
{
    namespace SecondEdition.TIELnFighter
    {
        public class Scythe6BoE : TIELnFighter
        {
            public Scythe6BoE() : base()
            {
                PilotInfo = new PilotCardInfo25(
                    "Scythe 6",
                    "Battle Over Endor",
                    Faction.Imperial,
                    2,
                    3,
                    loadoutValue: 0,
                    isStandardLayout: true,
                    isLimited: true,
                    extraUpgradeIcons: new List<UpgradeType>
                    {
                        UpgradeType.Talent,
                        UpgradeType.Talent,
                        UpgradeType.Modification,
                        UpgradeType.Modification,
                    },
                    tags: new List<Tags>
                    {
                        Tags.Tie
                    },
                    abilityType: typeof(Scythe6Ability)
                );

                ShipAbilities.Add(new FormedUpAbility());
                ShipInfo.ActionIcons.AddLinkedAction(new LinkedActionInfo(typeof(BarrelRollAction), typeof(EvadeAction)));
                ShipInfo.Hull++;

                MustHaveUpgrades.Add(typeof(UpgradesList.SecondEdition.NoEscape));
                MustHaveUpgrades.Add(typeof(UpgradesList.SecondEdition.Predator));
                MustHaveUpgrades.Add(typeof(UpgradesList.SecondEdition.IonManeuveringJet));
                MustHaveUpgrades.Add(typeof(UpgradesList.SecondEdition.TargetingMatrix));

                PilotNameCanonical = "scythe6-battleoverendor";
                ImageUrl = "https://infinitearenas.com/xw2/images/quickbuilds/scythe6-battleoverendor.png";
            }
        }
    }
}

namespace Abilities.SecondEdition
{
    public class Scythe6Ability : GenericAbility
    {
        public override void ActivateAbility()
        {
            HostShip.AfterGotNumberOfAttackDice += CheckAbility;
        }

        public override void DeactivateAbility()
        {
            HostShip.AfterGotNumberOfAttackDice += CheckAbility;
        }

        private void CheckAbility(ref int value)
        {
            if (Combat.ShotInfo.Range >= 1 && Combat.ShotInfo.Range <= 2 )
            {
                Messages.ShowInfo(HostShip.PilotInfo.PilotName + ": The attack is at range 1-2, attacker gains +1 attack die");
                value++;
            }
        }
    }
}