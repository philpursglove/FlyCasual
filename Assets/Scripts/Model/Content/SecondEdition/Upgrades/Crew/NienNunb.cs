using Content;
using Movement;
using Ship;
using System.Collections.Generic;
using UnityEngine;
using Upgrade;

namespace UpgradesList.SecondEdition
{
    public class NienNunb : GenericUpgrade
    {
        public NienNunb() : base()
        {
            UpgradeInfo = new UpgradeCardInfo(
                "Nien Nunb",
                UpgradeType.Crew,
                cost: 5,
                isLimited: true,
                restriction: new FactionRestriction(Faction.Rebel),
                abilityType: typeof(Abilities.SecondEdition.NienNunbCrewAbility),
                seImageNumber: 90,
                legalityInfo: new List<Legality> { Legality.StandardLegal, Legality.ExtendedLegal }
            );

            Avatar = new AvatarInfo(
                Faction.Rebel,
                new Vector2(429, 21),
                new Vector2(150, 150)
            );
        }
    }

    public class NienNunbXWA : NienNunb
    {
        public NienNunbXWA() : base()
        {
            UpgradeInfo.Cost = 4;
            UpgradeInfo.LegalityInfo = new List<Legality> { Legality.XWA };
        }
    }
}

namespace Abilities.SecondEdition
{
    public class NienNunbCrewAbility : GenericAbility
    {
        public override void ActivateAbility()
        {
            HostShip.AfterGetManeuverColorDecreaseComplexity += NienNunbAbility;
        }

        public override void DeactivateAbility()
        {
            HostShip.AfterGetManeuverColorDecreaseComplexity -= NienNunbAbility;
        }

        private void NienNunbAbility(GenericShip ship, ref ManeuverHolder movement)
        {
            if (movement.ColorComplexity != MovementComplexity.None)
            {
                if (movement.Bearing == ManeuverBearing.Bank)
                {
                    movement.ColorComplexity = GenericMovement.ReduceComplexity(movement.ColorComplexity);
                }
            }
        }
    }
}