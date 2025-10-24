using Abilities.SecondEdition;
using BoardTools;
using Content;
using System.Collections.Generic;
using Upgrade;

namespace Ship.SecondEdition.UT60DUWing
{
    public class MagvaYarro : UT60DUWing
    {
        public MagvaYarro() : base()
        {
            PilotInfo = new PilotCardInfo25
            (
                "Magva Yarro",
                "Cavern Angels Spotter",
                Faction.Rebel,
                3,
                5,
                14,
                isLimited: true,
                abilityType: typeof(MagvaYarroPilotAbility),
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
                seImageNumber: 57,
                skinName: "Partisan",
                legality: new List<Legality> { Legality.StandardLegal, Legality.ExtendedLegal }
            );
        }
    }

    public class MagvaYarroXWA : MagvaYarro
    {
        public MagvaYarroXWA() : base()
        {
            (PilotInfo as PilotCardInfo25).Cost = 13;
            (PilotInfo as PilotCardInfo25).LoadoutValue = 17;
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
    public class MagvaYarroPilotAbility : GenericAbility
    {
        public override void ActivateAbility()
        {
            DiceRerollManager.OnMaxDiceRerollAllowed += CheckAbility;
        }

        public override void DeactivateAbility()
        {
            DiceRerollManager.OnMaxDiceRerollAllowed -= CheckAbility;
        }

        private void CheckAbility(ref int maxDiceRerollCount)
        {
            if (Combat.Defender.Owner.PlayerNo != HostShip.Owner.PlayerNo) return;

            DistanceInfo distInfo = new DistanceInfo(HostShip, Combat.Defender);
            if (distInfo.Range > 2) return;

            if (maxDiceRerollCount > 1) maxDiceRerollCount = 1;
        }
    }
}