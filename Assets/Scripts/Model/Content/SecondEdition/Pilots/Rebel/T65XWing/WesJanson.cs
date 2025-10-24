using Abilities.Parameters;
using Content;
using System;
using System.Collections.Generic;
using Tokens;
using Upgrade;

namespace Ship.SecondEdition.T65XWing
{
    public class WesJanson : T65XWing
    {
        public WesJanson() : base()
        {
            PilotInfo = new PilotCardInfo25
            (
                "Wes Janson",
                "Wisecracking Wingman",
                Faction.Rebel,
                5,
                5,
                15,
                isLimited: true,
                abilityType: typeof(Abilities.SecondEdition.WesJansonAbility),
                extraUpgradeIcons: new List<UpgradeType>
                {
                    UpgradeType.Talent,
                    UpgradeType.Astromech,
                    UpgradeType.Modification,
                    UpgradeType.Missile,
                    UpgradeType.Configuration
                },
                tags: new List<Tags>
                {
                    Tags.XWing
                },
                charges: 1,
                regensCharges: 1,
                legality: new List<Legality> { Legality.StandardLegal, Legality.ExtendedLegal }
            );
        }
    }

    public class WesJansonXWA : WesJanson
    {
        public WesJansonXWA() : base()
        {
            (PilotInfo as PilotCardInfo25).Cost = 12;
            (PilotInfo as PilotCardInfo25).LoadoutValue = 13;
            (PilotInfo as PilotCardInfo25).ExtraUpgrades = new List<UpgradeType>()
            {
                UpgradeType.Talent,
                UpgradeType.Astromech,
                UpgradeType.Modification,
                UpgradeType.Torpedo,
                UpgradeType.Configuration
            };
            (PilotInfo as PilotCardInfo25).LegalityInfo = new List<Legality> { Legality.XWA };
        }
    }
}

namespace Abilities.SecondEdition
{
    public class WesJansonAbility : CombinedAbility
    {
        public override List<Type> CombinedAbilities => new List<Type>()
        {
            typeof(WesJansonAttackAbility),
            typeof(WesJansonDefenseAbility)
        };
    }

    public class WesJansonAttackAbility : TriggeredAbility
    {
        public override TriggerForAbility Trigger => new AfterYouPerformAttack();

        public override AbilityPart Action => new AskToUseAbilityAction
        (
            description: new AbilityDescription
            (
                "Wes Janson",
                "Do you want to assign the defender 1 jam token?",
                HostShip
            ),
            onYes: new SpendPilotChargeAction
            (
                next: new AssignTokenAction
                (
                    tokenType: typeof(JamToken),
                    targetShipRole: ShipRole.Defender,
                    showMessage: ShowJamDefenderMessage
                )
            ),
            conditions: new ConditionsBlock
            (
                new HasPilotChargesAbility(1)
            ),
            aiUseByDefault: AlwaysUseByDefault
        );

        private string ShowJamDefenderMessage()
        {
            return $"{HostShip.PilotInfo.PilotName}: Jam token is assigned to {Combat.Defender.PilotInfo.PilotName}";
        }
    }

    public class WesJansonDefenseAbility : TriggeredAbility
    {
        public override TriggerForAbility Trigger => new AfterYouDefend();

        public override AbilityPart Action => new AskToUseAbilityAction
        (
            description: new AbilityDescription
            (
                "Wes Janson",
                "Do you want to assign the attacker 1 jam token?",
                HostShip
            ),
            onYes: new SpendPilotChargeAction
            (
                next: new AssignTokenAction
                (
                    tokenType: typeof(JamToken),
                    targetShipRole: ShipRole.Attacker,
                    showMessage: ShowJamAttackerMessage
                )
            ),
            conditions: new ConditionsBlock
            (
                new HasPilotChargesAbility(1)
            ),
            aiUseByDefault: AlwaysUseByDefault
        );

        private string ShowJamAttackerMessage()
        {
            return $"{HostShip.PilotInfo.PilotName}: Jam token is assigned to {Combat.Attacker.PilotInfo.PilotName}";
        }
    }
}