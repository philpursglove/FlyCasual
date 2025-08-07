using Abilities.Parameters;
using Arcs;
using Content;
using Ship;
using System.Collections.Generic;
using Tokens;
using Upgrade;

namespace Ship
{
    namespace SecondEdition.HMPDroidGunship
    {
        public class DGS047 : HMPDroidGunship
        {
            public DGS047() : base()
            {
                PilotInfo = new PilotCardInfo25
                (
                    "DGS-047",
                    "Adaptive Intelligence",
                    Faction.Separatists,
                    1,
                    3,
                    8,
                    isLimited: true,
                    abilityType: typeof(Abilities.SecondEdition.DGS047Ability),
                    extraUpgradeIcons: new List<UpgradeType>
                    {
                        UpgradeType.Missile,
                        UpgradeType.Missile,
                        UpgradeType.TacticalRelay,
                        UpgradeType.Crew,
                        UpgradeType.Device,
                        UpgradeType.Modification,
                        UpgradeType.Configuration
                    },
                    tags: new List<Tags>
                    {
                        Tags.Droid
                    },
                    legality: new List<Legality> { Legality.StandardLegal, Legality.ExtendedLegal }
                );
            }
        }

        public class DGS047XWA : DGS047
        {
            public DGS047XWA() : base()
            {
                (PilotInfo as PilotCardInfo25).Cost = 4;
                (PilotInfo as PilotCardInfo25).LoadoutValue = 12;
                (PilotInfo as PilotCardInfo25).ExtraUpgrades = new List<UpgradeType>
                {
                    UpgradeType.Modification,
                    UpgradeType.Device,
                    UpgradeType.Crew,
                    UpgradeType.Missile,
                    UpgradeType.Missile,
                    UpgradeType.Configuration,
                    UpgradeType.TacticalRelay,
                };
                (PilotInfo as PilotCardInfo25).LegalityInfo = new List<Legality> { Legality.XWA };
            }
        }
    }
}

namespace Abilities.SecondEdition
{
    public class DGS047Ability : TriggeredAbility
    {
        public override TriggerForAbility Trigger => new AfterYouPerformAttack();

        public override AbilityPart Action => new SectorCheckAction
        (
            sectorType: ArcType.Front,
            targetShip: GetDefender,
            action: new AskAquireLockAction
            (
                description: new AbilityDescription
                (
                    name: "DGS-047",
                    description: "Do you want to acquire a lock on defender?",
                    imageSource: HostShip
                ),
                targetShip: GetDefender,
                showMessage: GetLockMessageToShow,
                action: new SectorCheckAction
                (
                    sectorType: ArcType.Bullseye,
                    targetShip: GetDefender,
                    action: new AssignTokenAction
                    (
                        tokenType: typeof(StrainToken),
                        targetShipRole: ShipRole.Defender,
                        showMessage: GetTokenMessageToShow
                    )
                )
            )
        );

        private GenericShip GetDefender()
        {
            return Combat.Defender;
        }

        private string GetLockMessageToShow()
        {
            return "DGS-047: Lock on " + Combat.Defender.PilotInfo.PilotName + " is aquired";
        }

        private string GetTokenMessageToShow()
        {
            return "DGS-047: Strain Token is assigned to " + Combat.Defender.PilotInfo.PilotName;
        }
    }
}