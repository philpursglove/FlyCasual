using Content;
using System.Collections.Generic;
using Upgrade;

namespace Ship.SecondEdition.BTLA4YWing
{
    public class ArlizHadrassian : BTLA4YWing
    {
        public ArlizHadrassian() : base()
        {
            PilotInfo = new PilotCardInfo25
            (
                "Arliz Hadrassian",
                "Crimson Blade",
                Faction.Scum,
                4,
                3,
                10,
                isLimited: true,
                abilityType: typeof(Abilities.SecondEdition.ArlizHadrassianAbility),
                extraUpgradeIcons: new List<UpgradeType>()
                {
                    UpgradeType.Tech,
                    UpgradeType.Turret,
                    UpgradeType.Torpedo,
                    UpgradeType.Missile,
                    UpgradeType.Astromech,
                    UpgradeType.Device
                },
                tags: new List<Tags>
                {
                    Tags.YWing
                },
                skinName: "Gray",
                legality: new List<Legality> { Legality.StandardLegal, Legality.ExtendedLegal }
            );
        }
    }

    public class ArlizHadrassianXWA : ArlizHadrassian
    {
        public ArlizHadrassianXWA() : base()
        {
            (PilotInfo as PilotCardInfo25).Cost = 4;
            (PilotInfo as PilotCardInfo25).LoadoutValue = 12;
            (PilotInfo as PilotCardInfo25).ExtraUpgrades = new List<UpgradeType>
                {
                    UpgradeType.Talent,
                    UpgradeType.Astromech,
                    UpgradeType.Tech,
                    UpgradeType.Device,
                    UpgradeType.Turret,
                    UpgradeType.Missile,
                    UpgradeType.Torpedo
                };
            (PilotInfo as PilotCardInfo25).LegalityInfo = new List<Legality> { Legality.XWA };
        }
    }
}

namespace Abilities.SecondEdition
{
    public class ArlizHadrassianAbility : GenericAbility
    {
        public override void ActivateAbility()
        {
            AddDiceModification(
                HostShip.PilotInfo.PilotName,
                IsPosititveAvailable,
                GetPositiveAiPriority,
                DiceModificationType.Change,
                1,
                sidesCanBeSelected: new List<DieSide>() { DieSide.Focus },
                sideCanBeChangedTo: DieSide.Crit
            );

            AddDiceModification(
                HostShip.PilotInfo.PilotName,
                IsNegativeAvailable,
                GetNegativeAiPriority,
                DiceModificationType.Change,
                1,
                sidesCanBeSelected: new List<DieSide>() { DieSide.Focus },
                sideCanBeChangedTo: DieSide.Blank,
                timing: DiceModificationTimingType.AfterRolled,
                isForcedModification: true
            );
        }

        public override void DeactivateAbility()
        {
            RemoveDiceModification();
        }

        private bool IsPosititveAvailable()
        {
            return Combat.AttackStep == CombatStep.Attack
                && Combat.ChosenWeapon.WeaponInfo.ArcRestrictions.Contains(Arcs.ArcType.Front)
                && HostShip.Damage.IsDamaged
                && Combat.DiceRollAttack.HasResult(DieSide.Focus);
        }

        private int GetPositiveAiPriority()
        {
            return 70;
        }

        private bool IsNegativeAvailable()
        {
            return Combat.AttackStep == CombatStep.Defence
                && HostShip.Damage.IsDamaged
                && Combat.DiceRollDefence.HasResult(DieSide.Focus);
        }

        private int GetNegativeAiPriority()
        {
            return 1;
        }
    }
}