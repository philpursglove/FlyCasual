using Arcs;
using Content;
using System.Collections.Generic;
using Upgrade;

namespace UpgradesList.SecondEdition
{
    public class Autoblasters : GenericSpecialWeapon
    {
        public Autoblasters() : base()
        {
            UpgradeInfo = new UpgradeCardInfo(
                "Autoblasters",
                UpgradeType.Cannon,
                cost: 7,
                weaponInfo: new SpecialWeaponInfo(
                    attackValue: 2,
                    minRange: 1,
                    maxRange: 2,
                    arc: ArcType.Front
                ),
                abilityType: typeof(Abilities.SecondEdition.AutoblastersAbility),
                legalityInfo: new List<Legality>
                {
                    Legality.StandardBanned,
                    Legality.ExtendedLegal
                }
            );
        }
    }

    public class AutoblastersXWA : Autoblasters
    {
        public AutoblastersXWA() : base()
        {
            UpgradeInfo.Cost = 7;
            UpgradeInfo.LegalityInfo = new List<Legality> { Legality.XWA };
            UpgradeInfo.Limited = 2;
        }
    }
}

namespace Abilities.SecondEdition
{
    public class AutoblastersAbility : GenericAbility
    {
        public override void ActivateAbility()
        {
            HostShip.AfterGotNumberOfAttackDice += CheckForExtraDie;
            HostShip.OnDefenceStartAsAttacker += MakeCritsUncancellable;
        }


        public override void DeactivateAbility()
        {
            HostShip.AfterGotNumberOfAttackDice -= CheckForExtraDie;
            HostShip.OnDefenceStartAsAttacker -= MakeCritsUncancellable;
        }

        private void CheckForExtraDie(ref int diceAmount)
        {
            if (Combat.ChosenWeapon.GetType() == HostUpgrade.GetType())
            {
                if (Combat.Attacker.SectorsInfo.IsShipInSector(Combat.Defender, ArcType.Bullseye))
                {
                    Messages.ShowInfo("Target is in bullseye arc, Autoblaster rolls +1 attack die");
                    diceAmount++;
                }
            }
        }

        private void MakeCritsUncancellable()
        {
            if (Combat.ChosenWeapon.GetType() == HostUpgrade.GetType() && !Combat.Defender.SectorsInfo.IsShipInSector(Combat.Attacker, ArcType.Front))
            {
                foreach (Die die in Combat.DiceRollAttack.DiceList)
                {
                    if (die.Side == DieSide.Crit) die.IsUncancelable = true;
                }
            }
        }
    }
}