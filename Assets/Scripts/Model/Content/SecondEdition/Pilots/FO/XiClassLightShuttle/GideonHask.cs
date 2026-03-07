using BoardTools;
using Content;
using Ship;
using System;
using System.Collections.Generic;
using Upgrade;

namespace Ship.SecondEdition.XiClassLightShuttle
{
    public class GideonHask : XiClassLightShuttle
    {
        public GideonHask() : base()
        {
            PilotInfo = new PilotCardInfo25
            (
                "Gideon Hask",
                "Merciless Hard-Liner",
                Faction.FirstOrder,
                4,
                4,
                15,
                isLimited: true,
                abilityType: typeof(Abilities.SecondEdition.GideonHaskXiClassLightShuttleAbility),
                extraUpgradeIcons: new List<UpgradeType>()
                {
                    UpgradeType.Talent,
                    UpgradeType.Talent,
                    UpgradeType.Tech,
                    UpgradeType.Tech,
                    UpgradeType.Crew,
                    UpgradeType.Crew,
                    UpgradeType.Modification
                },
                legality: new List<Legality> { Legality.StandardLegal, Legality.ExtendedLegal }
            );

            PilotNameCanonical = "gideonhask-xiclasslightshuttle";
        }
    }

    public class GideonHaskXWA : GideonHask
    {
        public GideonHaskXWA() : base()
        {
            (PilotInfo as PilotCardInfo25).Cost = 9;
            (PilotInfo as PilotCardInfo25).LoadoutValue = 8;
            (PilotInfo as PilotCardInfo25).ExtraUpgrades = new List<UpgradeType>
            {
                UpgradeType.Crew,
                UpgradeType.Crew,
                UpgradeType.Modification,
                UpgradeType.Tech,
                UpgradeType.Tech
            };
            (PilotInfo as PilotCardInfo25).LegalityInfo = new List<Legality> { Legality.XWA };
        }
    }
}

namespace Abilities.SecondEdition
{
    public class GideonHaskXiClassLightShuttleAbility : GenericAbility
    {
        public override void ActivateAbility()
        {
            AddDiceModification(
                HostShip.PilotInfo.PilotName,
                IsAvailable,
                GetAiPriority,
                DiceModificationType.Add,
                1,
                isGlobal: true,
                payAbilityCost: GainStrainToken
            );
        }

        public override void DeactivateAbility()
        {
            RemoveDiceModification();
        }

        private bool IsAvailable()
        {
            if (Combat.AttackStep != CombatStep.Attack) return false;
            if (Combat.Attacker.Owner.PlayerNo != HostShip.Owner.PlayerNo) return false;
            if (Combat.Attacker.ShipBase.Size != BaseSize.Small && Combat.Attacker.ShipId != HostShip.ShipId) return false;
            if (!Combat.Defender.Damage.IsDamaged) return false;
            if (Combat.ChosenWeapon.WeaponType != WeaponTypes.PrimaryWeapon) return false;
            if (Combat.Attacker.DiceRolledLastAttack > 2) return false;
            if (new DistanceInfo(Combat.Attacker, HostShip).Range > 2) return false;

            return true;
        }

        private void GainStrainToken(Action<bool> callback)
        {
            Combat.Attacker.Tokens.AssignToken(
                typeof(Tokens.StrainToken),
                delegate { callback(true); }
            );
        }

        private int GetAiPriority()
        {
            return 110;
        }
    }
}

