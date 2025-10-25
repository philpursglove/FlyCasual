using ActionsList;
using Arcs;
using Content;
using Ship;
using System;
using System.Collections.Generic;
using System.Linq;
using Upgrade;

namespace Ship.SecondEdition.TIESeBomber
{
    public class Dread : TIESeBomber
    {
        public Dread() : base()
        {
            PilotInfo = new PilotCardInfo25
            (
                "\"Dread\"",
                "Devotee of Devastation",
                Faction.FirstOrder,
                3,
                3,
                8,
                isLimited: true,
                abilityType: typeof(Abilities.SecondEdition.DreadPilotAbility),
                extraUpgradeIcons: new List<UpgradeType>()
                {
                    UpgradeType.Tech,
                    UpgradeType.Torpedo,
                    UpgradeType.Missile,
                    UpgradeType.Gunner,
                    UpgradeType.Device,
                    UpgradeType.Device,
                    UpgradeType.Modification
                },
                tags: new List<Tags>
                {
                    Tags.Tie
                },
                legality: new List<Legality> { Legality.StandardLegal, Legality.ExtendedLegal }
            );
        }
    }

    public class DreadXWA : Dread
    {
        public DreadXWA() : base()
        {
            (PilotInfo as PilotCardInfo25).Cost = 9;
            (PilotInfo as PilotCardInfo25).LoadoutValue = 9;
            (PilotInfo as PilotCardInfo25).ExtraUpgrades = new List<UpgradeType>
            {
                UpgradeType.Talent,
                UpgradeType.Gunner,
                UpgradeType.Modification,
                UpgradeType.Tech,
                UpgradeType.Device,
                UpgradeType.Device,
                UpgradeType.Missile,
                UpgradeType.Missile
            };
            (PilotInfo as PilotCardInfo25).LegalityInfo = new List<Legality> { Legality.XWA };
        }
    }
}


namespace Abilities.SecondEdition
{
    public class DreadPilotAbility : GenericAbility
    {
        public override void ActivateAbility()
        {
            HostShip.OnActionIsPerformed += CheckAbility;
        }

        public override void DeactivateAbility()
        {
            HostShip.OnActionIsPerformed -= CheckAbility;
        }

        private void CheckAbility(GenericAction action)
        {
            if (action is ReloadAction
                && GetShipsInBullseyeArc().Count > 0)
            {
                RegisterAbilityTrigger(TriggerTypes.OnActionIsPerformed, AssignDepleteToShipsInBullseye);
            }
        }

        private List<GenericShip> GetShipsInBullseyeArc()
        {
            List<GenericShip> shipsInBullseye = new List<GenericShip>();

            foreach (GenericShip ship in Roster.AllShips.Values)
            {
                if (HostShip.SectorsInfo.IsShipInSector(ship, ArcType.Bullseye)) shipsInBullseye.Add(ship);
            }

            return shipsInBullseye;
        }

        private void AssignDepleteToShipsInBullseye(object sender, EventArgs e)
        {
            List<GenericShip> sufferedShips = new List<GenericShip>(GetShipsInBullseyeArc());
            AssignDepleteToShipsInBullseyeRecursive(sufferedShips);
        }

        private void AssignDepleteToShipsInBullseyeRecursive(List<GenericShip> sufferedShips)
        {
            if (sufferedShips.Count == 0)
            {
                Triggers.FinishTrigger();
            }
            else
            {
                GenericShip sufferedShip = sufferedShips.First();
                sufferedShips.Remove(sufferedShip);

                Messages.ShowInfo($"{HostShip.PilotInfo.PilotName}: {sufferedShip.PilotInfo.PilotName} gains Deplete token");

                sufferedShip.Tokens.AssignToken
                (
                    typeof(Tokens.DepleteToken),
                    delegate { AssignDepleteToShipsInBullseyeRecursive(sufferedShips); }
                );
            }
        }
    }
}
