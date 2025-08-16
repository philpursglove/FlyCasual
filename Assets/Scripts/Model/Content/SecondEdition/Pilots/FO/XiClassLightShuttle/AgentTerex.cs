using Abilities.Parameters;
using System.Collections.Generic;
using Content;
using Upgrade;

namespace Ship
{
    namespace SecondEdition.XiClassLightShuttle
    {
        public class AgentTerex : XiClassLightShuttle
        {
            public AgentTerex() : base()
            {
                PilotInfo = new PilotCardInfo25
                (
                    "Agent Terex",
                    "Devious Provocateur",
                    Faction.FirstOrder,
                    3,
                    4,
                    13,
                    isLimited: true,
                    extraUpgradeIcons: new List<UpgradeType>()
                    {
                        UpgradeType.Talent,
                        UpgradeType.Tech,
                        UpgradeType.Tech,
                        UpgradeType.Crew,
                        UpgradeType.Illicit,
                        UpgradeType.Illicit,
                        UpgradeType.Illicit,
                        UpgradeType.Modification
                    },
                    abilityType: typeof(Abilities.SecondEdition.AgentTerexPilotAbility),
                    legality: new List<Legality> { Legality.StandardLegal, Legality.ExtendedLegal }
                );
            }
        }

        public class AgentTerexXWA : AgentTerex
        {
            public AgentTerexXWA() : base()
            {
                var pilot = (PilotCardInfo25)PilotInfo;
                pilot.LegalityInfo = new List<Legality> { Legality.XWA };
                pilot.Cost = 3;
                pilot.LoadoutValue = 8;
                pilot.ExtraUpgrades = new List<UpgradeType>
                {
                    UpgradeType.Talent,
                    UpgradeType.Tech,
                    UpgradeType.Tech,
                    UpgradeType.Crew,
                    UpgradeType.Illicit,
                    UpgradeType.Illicit,
                    UpgradeType.Illicit,
                    UpgradeType.Modification
                };
            }
        }
    }
}

namespace Abilities.SecondEdition
{
    public class AgentTerexPilotAbility : TriggeredAbility
    {
        public override TriggerForAbility Trigger => new AfterPlacingForces();

        public override AbilityPart Action => new EachUpgradeCanDoAction
        (
            eachUpgradeAction: new SelectShipAction
            (
                action: new TransferUpgradeAction(),
                conditions: new ConditionsBlock
                (
                    new ShipTypeCondition
                    (
                        typeof(Ship.SecondEdition.TIEFoFighter.TIEFoFighter),
                        typeof(Ship.SecondEdition.TIESfFighter.TIESfFighter)
                    )
                ),
                abilityDescription: new AbilityDescription
                (
                    "Agent Terex",
                    "Select a ship to equip",
                    imageSource: HostShip
                ),
                aiSelectShipPlan: new AiSelectShipPlan
                (
                    AiSelectShipTeamPriority.Friendly,
                    AiSelectShipSpecial.None
                )
            ),
            conditions: new ConditionsBlock
            (
                new UpgradeTypeCondition(UpgradeType.Illicit)
            )
        );
    }
}

