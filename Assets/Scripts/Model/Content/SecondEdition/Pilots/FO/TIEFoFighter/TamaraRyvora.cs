using Abilities.SecondEdition;
using Content;
using System.Collections.Generic;
using Tokens;

namespace Ship.SecondEdition.TIEFoFighter
{
    public class TamaraRyvora : TIEFoFighter
    {
        public TamaraRyvora() : base()
        {
            PilotInfo = new PilotCardInfo25(
                pilotName: "Tamara Ryvora",
                pilotTitle: "DT-533",
                faction: Faction.FirstOrder,
                initiative: 4,
                cost: 50, // TODO: Update
                loadoutValue: 50, // TODO: Update
                isLimited: true,
                abilityType: typeof(TamaraRyvoraAbility),
                legality: new List<Legality>() { Legality.XWA }
            );
        }
    }
}

namespace Abilities.SecondEdition
{
    // While a ship you are locking performs an attack, you may choose 1 attack die.
    // If you do, the attacker rerolls that die.

    public class TamaraRyvoraAbility : GenericAbility
    {
        public override void ActivateAbility()
        {
            AddDiceModification(
                HostShip.PilotInfo.PilotName,
                IsAvailable,
                GetAiPriority,
                DiceModificationType.Reroll,
                1,
                timing: DiceModificationTimingType.Opposite,
                isGlobal: true
            );
        }

        public override void DeactivateAbility()
        {
            RemoveDiceModification();
        }

        public bool IsAvailable()
        {
            foreach (RedTargetLockToken token in Combat.Attacker.Tokens.GetTokens<RedTargetLockToken>('*'))
            {
                if (token.OtherTargetLockTokenOwner == HostShip) return true;
            }

            return false;
        }

        public int GetAiPriority()
        {
            if (Combat.DiceRollAttack.Successes > 0)
                return 100;

            return 0;
        }
    }
}