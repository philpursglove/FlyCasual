using Abilities.SecondEdition;
using BoardTools;
using Content;
using Ship;
using System.Collections.Generic;
using System.Linq;
using Upgrade;

namespace Ship.SecondEdition.ResistanceTransportPod
{
    public class RoseTico : ResistanceTransportPod
    {
        public RoseTico()
        {
            PilotInfo = new PilotCardInfo25
            (
                "Rose Tico",
                "Earnest Engineer",
                Faction.Resistance,
                3,
                3,
                9,
                isLimited: true,
                abilityType: typeof(RoseTicoResistanceTransportPodAbility),
                extraUpgradeIcons: new List<UpgradeType>
                {
                    UpgradeType.Talent,
                    UpgradeType.Tech,
                    UpgradeType.Crew,
                    UpgradeType.Modification,
                    UpgradeType.Modification
                },
                legality: new List<Legality> { Legality.StandardLegal, Legality.ExtendedLegal }
            );
        }
    }

    public class RoseTicoXWA : RoseTico
    {
        public RoseTicoXWA() : base()
        {
            (PilotInfo as PilotCardInfo25).Cost = 8;
            (PilotInfo as PilotCardInfo25).LoadoutValue = 14;
            (PilotInfo as PilotCardInfo25).ExtraUpgrades = new List<UpgradeType>
            {
                UpgradeType.Talent,
                UpgradeType.Crew,
                UpgradeType.Modification,
                UpgradeType.Tech
            };
            (PilotInfo as PilotCardInfo25).LegalityInfo = new List<Legality> { Legality.XWA };
        }
    }
}

namespace Abilities.SecondEdition
{
    public class RoseTicoResistanceTransportPodAbility : GenericAbility
    {
        public override void ActivateAbility()
        {
            AddDiceModification(
                HostShip.PilotInfo.PilotName,
                () => { return true; },
                () => { return 90; },
                DiceModificationType.Reroll,
                GetRerollNumber
            );
        }

        private int GetRerollNumber()
        {
            int friendlyShipsInArc = 0;

            foreach (GenericShip friendlyShip in HostShip.Owner.Ships.Values)
            {
                if (friendlyShip.ShipId == HostShip.ShipId) continue;

                ShotInfo shotInfo = new ShotInfo(Combat.Attacker, friendlyShip, Combat.Attacker.PrimaryWeapons.First());
                if (shotInfo.InArc) friendlyShipsInArc++;
            }

            return friendlyShipsInArc;
        }

        public override void DeactivateAbility()
        {
            RemoveDiceModification();
        }
    }
}