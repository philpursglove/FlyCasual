using Abilities.SecondEdition;
using Content;
using System.Collections.Generic;
using Upgrade;

namespace Ship
{
    namespace SecondEdition.TIEInterceptor
    {
        public class GideonHask : TIEInterceptor
        {
            public GideonHask() : base()
            {
                PilotInfo = new PilotCardInfo25
                (
                    "Gideon Hask",
                    "Inferno Two",
                    Faction.Imperial,
                    4,
                    4,
                    7,
                    isLimited: true,
                    abilityType: typeof(GideonHaskTieInterceptorAbility),
                    extraUpgradeIcons: new List<UpgradeType>()
                    {
                        UpgradeType.Talent,
                        UpgradeType.Talent,
                        UpgradeType.Missile,
                        UpgradeType.Modification,
                        UpgradeType.Configuration
                    },
                    tags: new List<Tags>
                    {
                        Tags.Tie
                    },
                    legality: new List<Legality> { Legality.StandardLegal, Legality.ExtendedLegal }
                );

                PilotNameCanonical = "gideonhask-tieininterceptor";
            }
        }

        public class GideonHaskXWA : GideonHask
        {
            public GideonHaskXWA() : base()
            {
                (PilotInfo as PilotCardInfo25).Cost = 4;
                (PilotInfo as PilotCardInfo25).LoadoutValue = 4;
                (PilotInfo as PilotCardInfo25).LegalityInfo = new List<Legality> { Legality.XWA };
            }
        }
    }
}

namespace Abilities.SecondEdition
{
    public class GideonHaskTieInterceptorAbility : GenericAbility
    {
        public override void ActivateAbility()
        {
            HostShip.AfterGotNumberOfAttackDice += CheckGideoHaskAbility;
        }

        public override void DeactivateAbility()
        {
            HostShip.AfterGotNumberOfAttackDice -= CheckGideoHaskAbility;
        }

        private void CheckGideoHaskAbility(ref int value)
        {
            if (Combat.Defender.Damage.IsDamaged)
            {
                Messages.ShowInfo($"{Combat.Defender.PilotInfo.PilotName} is damaged, {HostShip.PilotInfo.PilotName} gains +1 attack die");
                value++;
            }
        }
    }
}