using Content;
using System;
using System.Collections.Generic;
using Upgrade;

namespace Ship.SecondEdition.VultureClassDroidFighter
{
    public class Dfs081 : VultureClassDroidFighter
    {
        public Dfs081()
        {
            PilotInfo = new PilotCardInfo25
            (
                "DFS-081",
                "Preservation Programming",
                Faction.Separatists,
                3,
                2,
                7,
                isLimited: true,
                abilityType: typeof(Abilities.SecondEdition.Dfs081Ability),
                extraUpgradeIcons: new List<UpgradeType>
                {
                    UpgradeType.Missile,
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

    public class Dfs081XWA : Dfs081
    {
        public Dfs081XWA() : base()
        {
            (PilotInfo as PilotCardInfo25).Cost = 3;
            (PilotInfo as PilotCardInfo25).LoadoutValue = 16;
            (PilotInfo as PilotCardInfo25).ExtraUpgrades = new List<UpgradeType>
            {
                UpgradeType.Modification,
                UpgradeType.Missile,
                UpgradeType.Configuration,
            };
            (PilotInfo as PilotCardInfo25).LegalityInfo = new List<Legality> { Legality.XWA };
        }
    }
}

namespace Abilities.SecondEdition
{
    //When a friendly ship at range 0-1 defends, it may spend 1 calculate token to change all crit results to hit results.
    public class Dfs081Ability : GenericAbility
    {
        public override void ActivateAbility()
        {
            AddDiceModification(
                HostName,
                IsDiceModificationAvailable,
                GetDiceModificationAiPriority,
                DiceModificationType.Change,
                0,
                new List<DieSide> { DieSide.Crit }, 
                DieSide.Success,
                DiceModificationTimingType.Opposite,
                isGlobal: true,
                payAbilityCost: PayAbilityCost
            );
        }

        public override void DeactivateAbility()
        {
            RemoveDiceModification();
        }

        public bool IsDiceModificationAvailable()
        {
            return (Combat.AttackStep == CombatStep.Attack
                && Combat.Defender.Owner == HostShip.Owner
                && Combat.Defender.Tokens.HasToken<Tokens.CalculateToken>()
                && new BoardTools.DistanceInfo(HostShip, Combat.Defender).Range <= 1);
        }

        private void PayAbilityCost(Action<bool> callback)
        {
            if (Combat.Defender.Tokens.HasToken<Tokens.CalculateToken>())
            {
                Combat.Defender.Tokens.SpendToken(typeof(Tokens.CalculateToken), () => callback(true));
            }
            else
            {
                callback(false);
            }
        }

        public int GetDiceModificationAiPriority()
        {
            return 95;
        }
    }
}
