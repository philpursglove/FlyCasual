using Bombs;
using Content;
using Ship;
using System;
using System.Collections.Generic;
using System.Linq;
using Upgrade;

namespace Ship
{
    namespace SecondEdition.BTLA4YWing
    {
        public class Padric : BTLA4YWing
        {
            public Padric() : base()
            {
                PilotInfo = new PilotCardInfo25
                (
                    "Padric",
                    "Napkin Bomber",
                    Faction.Scum,
                    3,
                    4,
                    10,
                    isLimited: true,
                    abilityType: typeof(Abilities.SecondEdition.PadricAbility),
                    extraUpgradeIcons: new List<UpgradeType>()
                    {
                        UpgradeType.Talent,
                        UpgradeType.Astromech,
                        UpgradeType.Modification,
                        UpgradeType.Tech,
                        UpgradeType.Device,
                        UpgradeType.Turret,
                        UpgradeType.Missile,
                        UpgradeType.Torpedo
                    },
                    tags: new List<Tags>
                    {
                        Tags.YWing
                    },
                    skinName: "Gray",
                    legality: new List<Legality> { Legality.StandardLegal, Legality.ExtendedLegal }
                );
            }
        }

        public class PadricXWA : Padric
        {
            public PadricXWA() : base()
            {
                (PilotInfo as PilotCardInfo25).Cost = 3;
                (PilotInfo as PilotCardInfo25).LoadoutValue = 6;
                (PilotInfo as PilotCardInfo25).LegalityInfo = new List<Legality> { Legality.XWA };
            }
        }
    }
}

namespace Abilities.SecondEdition
{
    // TODO
    // You need to lock the bomb first

    public class PadricAbility : GenericAbility
    {
        public override void ActivateAbility()
        {
            GenericBomb.OnBombIsDetonated += CheckAbility;
        }

        public override void DeactivateAbility()
        {
            GenericBomb.OnBombIsDetonated -= CheckAbility;
        }

        private void CheckAbility()
        {
            RegisterAbilityTrigger(TriggerTypes.OnBombIsDetonated, TryToDealStrainTokens);
        }

        private void TryToDealStrainTokens(object sender, EventArgs e)
        {
            List<GenericShip> shipsInRange = BombsManager.GetShipsInRange(BombsManager.CurrentBombObject);
            shipsInRange = shipsInRange.Where(n => !Tools.IsSameTeam(HostShip, n)).ToList();

            foreach (GenericShip enemyShip in shipsInRange)
            {
                RegisterAbilityTrigger(
                    TriggerTypes.OnAbilityDirect,
                    delegate { DealStrainTo(enemyShip); },
                    customTriggerName: $"Assign Strain token (ID:{enemyShip.ShipId})"
                );
            }

            Triggers.ResolveTriggers(TriggerTypes.OnAbilityDirect, Triggers.FinishTrigger);
        }

        private void DealStrainTo(GenericShip enemyShip)
        {
            enemyShip.Tokens.AssignToken(
                typeof(Tokens.StrainToken),
                Triggers.FinishTrigger
            );
        }
    }
}