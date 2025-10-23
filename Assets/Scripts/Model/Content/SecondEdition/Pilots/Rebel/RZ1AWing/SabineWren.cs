using Content;
using System.Collections.Generic;
using Upgrade;

namespace Ship.SecondEdition.RZ1AWing
{
    public class SabineWren : RZ1AWing
    {
        public SabineWren() : base()
        {
            PilotInfo = new PilotCardInfo25
            (
                "Sabine Wren",
                "Daughter of Mandalore",
                Faction.Rebel,
                3,
                3,
                7,
                isLimited: true,
                abilityType: typeof(Abilities.SecondEdition.SabineWrenAWingAbility),
                abilityText: "While you defend or perform an attack, if the attack range is 1 and you are in the enemy ship's front arc, you may change 1 of your results to a hit or evade result.",
                extraUpgradeIcons: new List<UpgradeType>
                {
                        UpgradeType.Talent,
                        UpgradeType.Modification,
                        UpgradeType.Modification,
                        UpgradeType.Configuration
                },
                tags: new List<Tags>
                {
                        Tags.AWing,
                        Tags.Mandalorian
                },
                skinName: "Green",
                legality: new List<Legality> { Legality.StandardLegal, Legality.ExtendedLegal }
            );

            PilotNameCanonical = "sabinewren-rz1awing";
        }
    }

    public class SabineWrenXWA : SabineWren
    {
        public SabineWrenXWA() : base()
        {
            (PilotInfo as PilotCardInfo25).Cost = 8;
            (PilotInfo as PilotCardInfo25).LoadoutValue = 4;
            (PilotInfo as PilotCardInfo25).ExtraUpgrades = new List<UpgradeType>
                {
                    UpgradeType.Talent,
                    UpgradeType.Modification,
                    UpgradeType.Missile,
                    UpgradeType.Configuration
                };
            (PilotInfo as PilotCardInfo25).LegalityInfo = new List<Legality> { Legality.XWA };
        }
    }
}

namespace Abilities.SecondEdition
{
    public class SabineWrenAWingAbility : GenericAbility
    {
        public override void ActivateAbility()
        {
            AddDiceModification(
                "Sabine Wren",
                IsAvailable,
                AiPriority,
                DiceModificationType.Change,
                1,
                sideCanBeChangedTo: DieSide.Success
            );
        }

        public override void DeactivateAbility()
        {
            RemoveDiceModification();
        }

        public bool IsAvailable()
        {
            return
            (
                Combat.ShotInfo.Range == 1 &&
                (
                    (Combat.Defender == HostShip && Combat.Attacker.SectorsInfo.IsShipInSector(HostShip, Arcs.ArcType.Front))
                    || (Combat.Attacker == HostShip && Combat.Defender.SectorsInfo.IsShipInSector(HostShip, Arcs.ArcType.Front))
                )
            );
        }

        public int AiPriority()
        {
            return 100;
        }
    }
}