using ActionsList;
using Content;
using Ship;
using System.Collections.Generic;
using Tokens;
using Upgrade;

namespace Ship
{
    namespace SecondEdition.ModifiedTIELnFighter
    {
        public class Lapin : ModifiedTIELnFighter
        {
            public Lapin() : base()
            {
                PilotInfo = new PilotCardInfo25
                (
                    "Lapin",
                    "Stickler for Details",
                    Faction.Scum,
                    2,
                    3,
                    7,
                    isLimited: true,
                    abilityType: typeof(Abilities.SecondEdition.LapinAbility),
                    extraUpgradeIcons: new List<UpgradeType>
                    {
                        UpgradeType.Talent,
                        UpgradeType.Modification,
                        UpgradeType.Modification,
                        UpgradeType.Cannon
                    },
                    tags: new List<Tags>
                    {
                        Tags.Tie
                    },
                    legality: new List<Legality> { Legality.StandardLegal, Legality.ExtendedLegal }
                );
            }
        }

        public class LapinXWA : Lapin
        {
            public LapinXWA() : base()
            {
                (PilotInfo as PilotCardInfo25).Cost = 8;
                (PilotInfo as PilotCardInfo25).LoadoutValue = 9;
                (PilotInfo as PilotCardInfo25).LegalityInfo = new List<Legality> { Legality.XWA };
                (PilotInfo as PilotCardInfo25).ExtraUpgrades = new List<UpgradeType>
                {
                    UpgradeType.Talent,
                    UpgradeType.Modification,
                    UpgradeType.Modification,
                };
            }
        }
    }
}

namespace Abilities.SecondEdition
{
    public class LapinAbility : GenericAbility
    {
        public override void ActivateAbility()
        {
            GenericShip.OnTryAddAvailableDiceModificationGlobal += UseLapinRestriction;
        }

        public override void DeactivateAbility()
        {
            GenericShip.OnTryAddAvailableDiceModificationGlobal -= UseLapinRestriction;
        }

        private void UseLapinRestriction(GenericShip ship, GenericAction diceModification, ref bool canBeUsed)
        {
            if (!diceModification.IsNotRealDiceModification
                && ship.Tokens.HasToken(typeof(StressToken))
                && (IsShipIsFightingAgainstLapin(ship))
            )
            {
                Messages.ShowErrorToHuman($"{HostShip.PilotInfo.PilotName}: {ship.PilotInfo.PilotName} is unable to modify dice");
                canBeUsed = false;
            }
        }

        private bool IsShipIsFightingAgainstLapin(GenericShip ship)
        {
            if (Tools.IsSameShip(Combat.Attacker, HostShip) && Tools.IsSameShip(Combat.Defender, ship)) return true;
            else if (Tools.IsSameShip(Combat.Defender, HostShip) && Tools.IsSameShip(Combat.Attacker, ship)) return true;
            else return false;
        }
    }
}