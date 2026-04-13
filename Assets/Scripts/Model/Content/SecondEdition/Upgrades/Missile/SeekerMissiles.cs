using System.Collections.Generic;
using Tokens;
using Upgrade;

namespace Content.SecondEdition
{
    public class SeekerMissiles : GenericSpecialWeapon
    {
        public SeekerMissiles() : base()
        {
            UpgradeInfo = new UpgradeCardInfo(
                "Seeker Missiles",
                UpgradeType.Missile,
                cost: 0,
                weaponInfo: new SpecialWeaponInfo(
                    attackValue: 3,
                    minRange: 2,
                    maxRange: 3,
                    charges: 4,
                    requiresToken: typeof(BlueTargetLockToken)
                ),
                abilityType: typeof(Abilities.SecondEdition.SeekerMissilesAbility),
                legalityInfo: new() { Legality.StandardLegal, Legality.ExtendedLegal }
            );
            IsHidden = true;

            //TODO Fix imageurl
            ImageUrl = "";
        }
    }

    public class SeekerMissilesXWA : SeekerMissiles
    {
        public SeekerMissilesXWA() : base()
        {
            UpgradeInfo.Cost = 1; //TODO Fix points
            UpgradeInfo.LegalityInfo = new() { Legality.XWA };
            IsHidden = false;
        }
    }
}

namespace Abilities.SecondEdition
{
    public class SeekerMissilesAbility : GenericAbility
    {
        public override void ActivateAbility()
        {
            AddDiceModification(name: "Seeker Missiles",
                isAvailable: IsAvailable,
                aiPriority: GetAIPriority,
                modificationType: DiceModificationType.Change,
                sidesCanBeSelected: new List<DieSide>() { DieSide.Focus },
                sideCanBeChangedTo: DieSide.Success,
                payAbilityPostCost: PayAbilityCost,
                count: 1);
        }

        public override void DeactivateAbility()
        {
            RemoveDiceModification();
        }

        private bool IsAvailable()
        {
            return Combat.DiceRollAttack.Focuses > 0;
        }

        private int GetAIPriority()
        {
            return 80;
        }

        private void PayAbilityCost()
        {
            for (int i = 0; i < DiceRoll.CurrentDiceRoll.DiceWereSelectedForRerollCount; i++)
            {
                HostUpgrade.State.SpendCharge();
            }
        }
    }
}