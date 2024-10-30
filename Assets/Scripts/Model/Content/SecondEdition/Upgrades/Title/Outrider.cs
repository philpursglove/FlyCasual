using ActionsList;
using BoardTools;
using Ship;
using SubPhases;
using System;
using System.Collections.Generic;
using System.Linq;
using Tokens;
using Upgrade;

namespace UpgradesList.SecondEdition
{
    public class Outrider : GenericUpgrade
    {
        public Outrider() : base()
        {
            UpgradeInfo = new UpgradeCardInfo(
                "Outrider",
                UpgradeType.Title,
                cost: 0,
                isLimited: true,
                restrictions: new UpgradeCardRestrictions
                (
                    new FactionRestriction(Faction.Rebel, Faction.Scum),
                    new ShipRestriction(typeof(Ship.SecondEdition.YT2400LightFreighter.YT2400LightFreighter))
                ),
                abilityType: typeof(Abilities.SecondEdition.OutriderAbility),
                seImageNumber: 105
            );
        }        
    }
}

namespace Abilities.SecondEdition
{
    public class OutriderAbility : GenericAbility
    {
        public override void ActivateAbility()
        {
            HostShip.AfterGotNumberOfAttackDice += CheckOutriderDiceChange;
            HostShip.OnGenerateDiceModificationsOpposite += OutriderJukeEffect;
        }

        public override void DeactivateAbility()
        {
            HostShip.AfterGotNumberOfAttackDice -= CheckOutriderDiceChange;
            HostShip.OnGenerateDiceModificationsOpposite -= OutriderJukeEffect;
        }

        private void OutriderJukeEffect(GenericShip host)
        {
            GenericAction newAction = new OutriderJukeEffect()
            {
                ImageUrl = HostUpgrade.ImageUrl,
                HostShip = host
            };
            host.AddAvailableDiceModificationOwn(newAction);
        }

        private void CheckOutriderDiceChange(ref int result)
        {
            ShotInfo shotInformation = new ShotInfo(Combat.Attacker, Combat.Defender, Combat.ChosenWeapon);
            if (shotInformation.Range == 3)
            {
                Messages.ShowInfo(HostShip.PilotInfo.PilotName + " is attacking at range 3 and gains +1 attack die");
                result++;
            }
        }

    }
}

namespace ActionsList
{
    public class OutriderJukeEffect : GenericAction
    {

        public OutriderJukeEffect()
        {
            Name = DiceModificationName = "Outrider";
            DiceModificationTiming = DiceModificationTimingType.Opposite;
        }

        public override int GetDiceModificationPriority()
        {
            int result = 0;

            result = 100;

            return result;
        }

        public override bool IsDiceModificationAvailable()
        {
            bool result = false;

            if (Combat.AttackStep == CombatStep.Defence &&
                Combat.DiceRollDefence.RegularSuccesses > 0 &&
                Combat.ShotInfo.IsObstructedByObstacle)
            {
                result = true;
            }

            return result;
        }

        public override void ActionEffect(System.Action callBack)
        {
            Combat.DiceRollDefence.ChangeOne(DieSide.Success, DieSide.Focus, false);
            callBack();
        }
    }
}