using Abilities.SecondEdition;
using Content;
using System.Collections.Generic;
using System.Linq;
using Upgrade;

namespace Ship
{
    namespace SecondEdition.TIELnFighter
    {
        public class GideonHask : TIELnFighter
        {
            public GideonHask() : base()
            {
                PilotInfo = new PilotCardInfo25
                (
                    "Gideon Hask",
                    "Inferno Two",
                    Faction.Imperial,
                    4,
                    3,
                    12,
                    isLimited: true,
                    abilityType: typeof(GideonHaskTieLnAbility),
                    extraUpgradeIcons: new List<UpgradeType>
                    {
                        UpgradeType.Talent,
                        UpgradeType.Talent,
                        UpgradeType.Missile,
                        UpgradeType.Modification
                    },
                    tags: new List<Tags>
                    {
                        Tags.Tie
                    },
                    seImageNumber: 84,
                    skinName: "Inferno",
                    legality: new List<Legality> { Legality.StandardLegal, Legality.ExtendedLegal }
                );
            }
        }

        public class GideonHaskXWA : GideonHask
        {
            public GideonHaskXWA() : base()
            {
                (PilotInfo as PilotCardInfo25).Cost = 9;
                (PilotInfo as PilotCardInfo25).LoadoutValue = 16;
                (PilotInfo as PilotCardInfo25).LegalityInfo = new List<Legality> { Legality.XWA };
                (PilotInfo as PilotCardInfo25).ExtraUpgrades = new List<UpgradeType>
                {
                    UpgradeType.Talent,
                    UpgradeType.Missile,
                    UpgradeType.Modification,
                    UpgradeType.Modification,
                    UpgradeType.Sensor
                };
            }
        }
    }
}

namespace Abilities.SecondEdition
{
    public class GideonHaskTieLnAbility : GenericAbility
    {
        public override void ActivateAbility()
        {
            HostShip.OnShotStartAsAttacker += CheckConditions;
        }

        public override void DeactivateAbility()
        {
            HostShip.OnShotStartAsAttacker -= CheckConditions;
        }

        protected virtual void CheckConditions()
        {
            if (Combat.Defender.Damage.DamageCards.Any())
            {
                HostShip.AfterGotNumberOfAttackDice += RollExtraDice;
            }
        }

        protected virtual void SendExtraDiceMessage()
        {
            Messages.ShowInfo("The defender has a damage card, " + HostShip.PilotInfo.PilotName + " rolls an additional attack die");
        }

        protected void RollExtraDice(ref int count)
        {
            count++;
            SendExtraDiceMessage();
            HostShip.AfterGotNumberOfAttackDice -= RollExtraDice;
        }
    }
}

