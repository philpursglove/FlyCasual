using Content;
using System.Collections.Generic;
using Tokens;
using Upgrade;

namespace UpgradesList.SecondEdition
{
    public class AdvProtonTorpedoes : GenericSpecialWeapon
    {
        public AdvProtonTorpedoes() : base()
        {
            UpgradeInfo = new UpgradeCardInfo(
                "Adv. Proton Torpedoes",
                UpgradeType.Torpedo,
                cost: 9,
                weaponInfo: new SpecialWeaponInfo(
                    attackValue: 5,
                    minRange: 1,
                    maxRange: 1,
                    requiresToken: typeof(BlueTargetLockToken),
                    charges: 1
                ),
                abilityType: typeof(Abilities.SecondEdition.AdvProtonTorpedoesAbility),
                seImageNumber: 33,
                legalityInfo: new List<Legality> { Legality.StandardLegal, Legality.ExtendedLegal }
            );

            NameCanonical = "advprotontorpedoes";
        }
    }

    public class AdvProtonTorpedoesXWA : AdvProtonTorpedoes
    {
        public AdvProtonTorpedoesXWA() : base()
        {
            UpgradeInfo.Cost = 7;
            UpgradeInfo.LegalityInfo = new List<Legality> { Legality.XWA };
        }
    }
}

namespace Abilities.SecondEdition
{
    //Attack(lock) : Spend 1 charge. Change 1 hit result to a crit result.
    public class AdvProtonTorpedoesAbility : GenericAbility
    {
        public override void ActivateAbility()
        {
            AddDiceModification(
                HostUpgrade.UpgradeInfo.Name,
                IsDiceModificationAvailable,
                GetDiceModificationAiPriority,
                DiceModificationType.Change,
                1,
                new List<DieSide>() { DieSide.Success },
                DieSide.Crit
            );
        }

        public override void DeactivateAbility()
        {
            RemoveDiceModification();
        }

        private bool IsDiceModificationAvailable()
        {
            return HostShip.IsAttacking && Combat.ChosenWeapon == HostUpgrade;
        }

        private int GetDiceModificationAiPriority()
        {
            return 30;
        }
    }
}