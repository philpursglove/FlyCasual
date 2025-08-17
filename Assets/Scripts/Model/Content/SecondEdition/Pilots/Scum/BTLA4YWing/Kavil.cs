using Content;
using System.Collections.Generic;
using Upgrade;

namespace Ship
{
    namespace SecondEdition.BTLA4YWing
    {
        public class Kavil : BTLA4YWing
        {
            public Kavil() : base()
            {
                PilotInfo = new PilotCardInfo25
                (
                    "Kavil",
                    "Callous Corsair",
                    Faction.Scum,
                    5,
                    4,
                    7,
                    isLimited: true,
                    abilityType: typeof(Abilities.SecondEdition.KavilAbility),
                    extraUpgradeIcons: new List<UpgradeType>()
                    {
                        UpgradeType.Talent,
                        UpgradeType.Astromech,
                        UpgradeType.Illicit,
                        UpgradeType.Modification,
                        UpgradeType.Device,
                        UpgradeType.Turret,
                        UpgradeType.Missile,
                        UpgradeType.Torpedo
                    },
                    tags: new List<Tags>
                    {
                        Tags.YWing
                    },
                    seImageNumber: 165,
                    legality: new List<Legality> { Legality.StandardLegal, Legality.ExtendedLegal }
                );
            }
        }

        public class KavilXWA : Kavil
        {
            public KavilXWA() : base()
            {
                (PilotInfo as PilotCardInfo25).Cost = 4;
                (PilotInfo as PilotCardInfo25).LoadoutValue = 11;
                (PilotInfo as PilotCardInfo25).LegalityInfo = new List<Legality> { Legality.XWA };
            }
        }
    }
}

namespace Abilities.SecondEdition
{
    public class KavilAbility : GenericAbility
    {
        public override void ActivateAbility()
        {
            HostShip.AfterGotNumberOfAttackDice += KavilPilotAbility;
        }

        public override void DeactivateAbility()
        {
            HostShip.AfterGotNumberOfAttackDice -= KavilPilotAbility;
        }

        private void KavilPilotAbility(ref int diceCount)
        {
            if (Combat.ArcForShot.ArcType != Arcs.ArcType.Front)
            {
                Messages.ShowInfo(HostShip.PilotInfo.PilotName + " is attacking with a non-front-arc attack and gains +1 attack die");
                diceCount++;
            }
        }
    }
}