using Content;
using Tokens;
using Upgrade;

namespace UpgradesList.SecondEdition
{
    public class IonTorpedoes : GenericSpecialWeapon
    {
        public IonTorpedoes() : base()
        {
            UpgradeInfo = new UpgradeCardInfo(
                "Ion Torpedoes",
                UpgradeType.Torpedo,
                cost: 4,
                weaponInfo: new SpecialWeaponInfo(
                    attackValue: 4,
                    minRange: 2,
                    maxRange: 3,
                    requiresToken: typeof(BlueTargetLockToken),
                    charges: 2
                ),
                abilityType: typeof(Abilities.SecondEdition.IonDamageAbility),
                seImageNumber: 34,
                legalityInfo: new() { Legality.StandardLegal, Legality.ExtendedLegal }
            );
        }        
    }

    public class IonTorpedoesXWA : IonTorpedoes
    {
        public IonTorpedoesXWA() : base()
        {
            UpgradeInfo.Cost = 5;
            UpgradeInfo.LegalityInfo = new() { Legality.XWA };
        }
    }
}