using Abilities.Parameters;
using Content;
using Ship;
using System;
using System.Collections.Generic;
using Upgrade;

namespace Ship.SecondEdition.T70XWing
{
    public class TemminWexleyHoH : T70XWing
    {
        public TemminWexleyHoH() : base()
        {
            PilotInfo = new PilotCardInfo25
            (
                "Temmin Wexley",
                "Black Two",
                Faction.Resistance,
                4,
                5,
                13,
                isLimited: true,
                abilityType: typeof(Abilities.SecondEdition.TemminWexleyHoHAbility),
                extraUpgradeIcons: new List<UpgradeType>
                {
                    UpgradeType.Talent,
                    UpgradeType.Tech,
                    UpgradeType.Tech,
                    UpgradeType.Astromech,
                    UpgradeType.Modification,
                    UpgradeType.Configuration
                },
                tags: new List<Tags>
                {
                    Tags.XWing
                },
                skinName: "Green (HoH)",
                legality: new List<Legality> { Legality.StandardLegal, Legality.ExtendedLegal }
            );

            PilotNameCanonical = "temminwexley-swz68";
        }
    }

    public class TemminWexleyHoHXWA : TemminWexleyHoH
    {
        public TemminWexleyHoHXWA() : base()
        {
            (PilotInfo as PilotCardInfo25).Cost = 11;
            (PilotInfo as PilotCardInfo25).LoadoutValue = 5;
            (PilotInfo as PilotCardInfo25).ExtraUpgrades = new List<UpgradeType>()
            {
                UpgradeType.Talent,
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
    public class TemminWexleyHoHAbility : TriggeredAbility
    {
        public override TriggerForAbility Trigger => new AtTheStartOfPhase(typeof(SubPhases.CombatStartSubPhase));

        public override AbilityPart Action => new EachShipCanDoAction
        (
            eachShipAction: FlipConfiguration,
            conditions: new ConditionsBlock
            (
                new TeamCondition(ShipTypes.Friendly),
                new RangeToHostCondition(minRange: 0, maxRange: 3),
                new ShipTypeCondition(typeof(Ship.SecondEdition.T70XWing.T70XWing)),
                new HasUpgradeTypeInstalledCondition(UpgradeType.Configuration)
            ),
            description: new AbilityDescription
            (
                "Temmin Wexley",
                "Friendly T-70 may gain 1 strain token to flip Configuration upgrade and get 1 calculate token",
                imageSource: HostShip
            )
        );

        private void FlipConfiguration(GenericShip ship, Action callback)
        {
            ship.Tokens.AssignToken
            (
                typeof(Tokens.StrainToken),
                delegate
                {
                    GenericDualUpgrade dualUpgrade = ship.UpgradeBar.GetInstalledUpgrade(UpgradeType.Configuration) as GenericDualUpgrade;
                    if (dualUpgrade != null)
                    {
                        dualUpgrade.Flip();
                        ship.Tokens.AssignToken
                        (
                            typeof(Tokens.CalculateToken),
                            callback
                        );
                    }
                    else
                    {
                        Messages.ShowError("Error: No configuration to flip");
                        callback();
                    }
                }
            );
        }
    }
}
