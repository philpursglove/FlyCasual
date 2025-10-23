using Abilities.SecondEdition;
using ActionsList;
using Content;
using Ship;
using System.Collections.Generic;
using Upgrade;

namespace Ship.SecondEdition.UT60DUWing
{
    public class SawGerrera : UT60DUWing
    {
        public SawGerrera() : base()
        {
            PilotInfo = new PilotCardInfo25
            (
                "Saw Gerrera",
                "Obsessive Outlaw",
                Faction.Rebel,
                4,
                5,
                18,
                isLimited: true,
                abilityType: typeof(SawGerreraPilotAbility),
                extraUpgradeIcons: new List<UpgradeType>
                {
                    UpgradeType.Talent,
                    UpgradeType.Crew,
                    UpgradeType.Crew,
                    UpgradeType.Sensor,
                    UpgradeType.Illicit,
                    UpgradeType.Modification,
                    UpgradeType.Configuration
                },
                tags: new List<Tags>
                {
                    Tags.Partisan
                },
                seImageNumber: 55,
                skinName: "Partisan",
                legality: new List<Legality> { Legality.StandardLegal, Legality.ExtendedLegal }
            );
        }
    }

    public class SawGerreraXWA : SawGerrera
    {
        public SawGerreraXWA() : base()
        {
            (PilotInfo as PilotCardInfo25).Cost = 13;
            (PilotInfo as PilotCardInfo25).LoadoutValue = 16;
            (PilotInfo as PilotCardInfo25).ExtraUpgrades = new List<UpgradeType>()
            {
                UpgradeType.Talent,
                UpgradeType.Crew,
                UpgradeType.Crew,
                UpgradeType.Sensor,
                UpgradeType.Illicit,
                UpgradeType.Modification,
                UpgradeType.Configuration
            };
            (PilotInfo as PilotCardInfo25).LegalityInfo = new List<Legality> { Legality.XWA };
        }
    }
}

namespace Abilities.SecondEdition
{
    public class SawGerreraPilotAbility : GenericAbility
    {
        public override void ActivateAbility()
        {
            GenericShip.OnGenerateDiceModificationsGlobal += AddSawGarreraAbility;
        }

        public override void DeactivateAbility()
        {
            GenericShip.OnGenerateDiceModificationsGlobal -= AddSawGarreraAbility;
        }

        private void AddSawGarreraAbility(GenericShip ship)
        {
            Combat.Attacker.AddAvailableDiceModification(new SawGarreraAction(), HostShip);
        }

        private class SawGarreraAction : FriendlyRerollAction
        {
            public SawGarreraAction() : base(1, 2, true, RerollTypeEnum.AttackDice)
            {
                Name = DiceModificationName = "Saw Gerrera";
                ImageUrl = new Ship.SecondEdition.UT60DUWing.SawGerrera().ImageUrl;
            }

            public override bool IsDiceModificationAvailable()
            {
                bool result = false;
                if (Combat.Attacker.Damage.IsDamaged) result = base.IsDiceModificationAvailable();
                return result;
            }
        }
    }
}