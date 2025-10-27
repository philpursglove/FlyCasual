using Content;
using System;
using System.Collections.Generic;
using Upgrade;
using UpgradesList.SecondEdition;

namespace Ship.SecondEdition.VultureClassDroidFighter
{
    public class Dfs081SoC : VultureClassDroidFighter
    {
        public Dfs081SoC()
        {
            PilotInfo = new PilotCardInfo25
            (
                "DFS-081",
                "Siege of Coruscant",
                Faction.Separatists,
                3,
                2,
                0,
                isLimited: true,
                abilityType: typeof(Abilities.SecondEdition.Dfs081SoCAbility),
                charges: 2,
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
                isStandardLayout: true,
                legality: new List<Legality> { Legality.StandardLegal, Legality.ExtendedLegal }
            );

            MustHaveUpgrades.Add(typeof(DiscordMissiles));
            MustHaveUpgrades.Add(typeof(ContingencyProtocol));
            MustHaveUpgrades.Add(typeof(StrutLockOverride));

            PilotNameCanonical = "dfs081-siegeofcoruscant";
        }
    }

    public class Dfs081SoCXWA : Dfs081SoC
    {
        public Dfs081SoCXWA() : base()
        {
            (PilotInfo as PilotCardInfo25).Cost = 6;
            (PilotInfo as PilotCardInfo25).LegalityInfo = new List<Legality> { Legality.XWA };
        }
    }
}

namespace Abilities.SecondEdition
{
    public class Dfs081SoCAbility : GenericAbility
    {
        public override void ActivateAbility()
        {
            AddDiceModification
            (
                HostName,
                IsDiceModificationAvailable,
                GetDiceModificationAiPriority,
                DiceModificationType.Cancel,
                1,
                new List<DieSide> { DieSide.Crit },
                timing: DiceModificationTimingType.Opposite,
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
                && HostShip.Tokens.HasToken<Tokens.CalculateToken>()
                && HostShip.State.Charges > 0);
        }

        private void PayAbilityCost(Action<bool> callback)
        {
            if (HostShip.Tokens.HasToken<Tokens.CalculateToken>()
                && HostShip.State.Charges > 0)
            {
                HostShip.LoseCharge();
                HostShip.Tokens.SpendToken(typeof(Tokens.CalculateToken), () => callback(true));
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
