using Abilities.Parameters;
using Content;
using System.Collections.Generic;
using Tokens;
using Upgrade;

namespace Ship
{
    namespace SecondEdition.RZ2AWing
    {
        public class SuralindaJavos  : RZ2AWing
        {
            public SuralindaJavos() : base()
            {
                PilotInfo = new PilotCardInfo25
                (
                    "Suralinda Javos",
                    "Inquisitive Journalist",
                    Faction.Resistance,
                    3,
                    4,
                    10,
                    isLimited: true,
                    abilityType: typeof(Abilities.SecondEdition.SuralindaJavosAbility),
                    extraUpgradeIcons: new List<UpgradeType>
                    {
                        UpgradeType.Talent,
                        UpgradeType.Talent,
                        UpgradeType.Modification,
                        UpgradeType.Tech,
                        UpgradeType.Cannon
                    },
                    tags: new List<Tags>
                    {
                        Tags.AWing
                    },
                    skinName: "Blue",
                    legality: new List<Legality> { Legality.StandardLegal, Legality.ExtendedLegal }
                );
            }
        }

        public class SuralindaJavosXWA : SuralindaJavos
        {
            public SuralindaJavosXWA() : base()
            {
                (PilotInfo as PilotCardInfo25).Cost = 3;
                (PilotInfo as PilotCardInfo25).LoadoutValue = 4;
                (PilotInfo as PilotCardInfo25).LegalityInfo = new List<Legality> { Legality.XWA };
            }
        }
    }
}

namespace Abilities.SecondEdition
{
    public class SuralindaJavosAbility : TriggeredAbility
    {
        public override TriggerForAbility Trigger => new AfterManeuver
        (
            onlyIfPartialExecuted: true
        );

        public override AbilityPart Action => new AskToUseAbilityAction
        (
            description: new AbilityDescription
            (
                name: "Suralinda Javos",
                description: "Do you want to gain Strain token to rotate 90 or 180 degrees?",
                imageSource: HostShip
            ),
            onYes: new AssignTokenAction
            (
                tokenType: typeof(StrainToken),
                targetShipRole: ShipRole.HostShip,
                showMessage: GetGainedStrainTokenMessage,
                afterAction: new AskToRotateShipAction
                (
                    description: new AbilityDescription
                    (
                        name: "Suralinda Javos",
                        description: "Choose how do you rotate",
                        imageSource: HostShip
                    ),
                    rotate90allowed: true,
                    rotate180allowed: true
                )
            )
        );

        private string GetGainedStrainTokenMessage()
        {
            return "Suralinda Javos: Gained strain token to rotate";
        }
    }
}