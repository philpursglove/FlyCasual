using Content;
using System.Collections.Generic;
using Upgrade;

namespace Ship
{
    namespace SecondEdition.VCX100LightFreighter
    {
        public class AlexsandrKallus : VCX100LightFreighter
        {
            public AlexsandrKallus() : base()
            {
                PilotInfo = new PilotCardInfo25
                (
                    "Alexsandr Kallus",
                    "Fulcrum",
                    Faction.Rebel,
                    4,
                    7,
                    16,
                    isLimited: true,
                    abilityType: typeof(Abilities.SecondEdition.AlexsandrKallusAbility),
                    extraUpgradeIcons: new List<UpgradeType>
                    {
                        UpgradeType.Talent,
                        UpgradeType.Sensor,
                        UpgradeType.Turret,
                        UpgradeType.Torpedo,
                        UpgradeType.Crew,
                        UpgradeType.Crew,
                        UpgradeType.Gunner,
                        UpgradeType.Modification,
                        UpgradeType.Title
                    },
                    tags: new List<Tags>
                    {
                        Tags.Freighter,
                        Tags.Spectre
                    },
                    legality: new List<Legality> { Legality.StandardLegal, Legality.ExtendedLegal }
                );
            }
        }

        public class AlexsandrKallusXWA : AlexsandrKallus
        {
            public AlexsandrKallusXWA() : base()
            {
                (PilotInfo as PilotCardInfo25).Cost = 6;
                (PilotInfo as PilotCardInfo25).LoadoutValue = 10;
                (PilotInfo as PilotCardInfo25).ExtraUpgrades = new List<UpgradeType>
                { 
                    UpgradeType.Talent,
                    UpgradeType.Crew,
                    UpgradeType.Crew,
                    UpgradeType.Crew,
                    UpgradeType.Sensor,
                    UpgradeType.Modification,
                    UpgradeType.Turret,
                    UpgradeType.Torpedo,
                    UpgradeType.Title
                };
                (PilotInfo as PilotCardInfo25).LegalityInfo = new List<Legality> { Legality.XWA };
            }
        }
    }
}

namespace Abilities.SecondEdition
{
    //While you defend, if the attacker modified any attack dice, you may roll an additional defense die.

    public class AlexsandrKallusAbility : GenericAbility
    {
        public override void ActivateAbility()
        {
            HostShip.AfterGotNumberOfDefenceDice += CheckAbility;
        }

        public override void DeactivateAbility()
        {
            HostShip.AfterGotNumberOfDefenceDice -= CheckAbility;
        }

        private void CheckAbility(ref int value)
        {
            if (Combat.AttackStep == CombatStep.Defence && Combat.DiceRollAttack.ModifiedByPlayers.Contains(Combat.Attacker.Owner.PlayerNo))
            {
                Messages.ShowInfo(HostShip.PilotInfo.PilotName + " rolls an additional defense die");
                value++;
            }
        }
    }
}