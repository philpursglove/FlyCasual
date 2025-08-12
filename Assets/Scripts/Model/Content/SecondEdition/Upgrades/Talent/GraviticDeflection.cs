using BoardTools;
using Content;
using Ship;
using System.Collections.Generic;
using Upgrade;

namespace UpgradesList.SecondEdition
{
    public class GraviticDeflection : GenericUpgrade
    {
        public GraviticDeflection() : base()
        {
            UpgradeInfo = new UpgradeCardInfo(
                "Gravitic Deflection",
                UpgradeType.Talent,
                cost: 4,
                abilityType: typeof(Abilities.SecondEdition.GraviticDeflectionAbility),
                restriction: new ShipRestriction(typeof(Ship.SecondEdition.NantexClassStarfighter.NantexClassStarfighter)),
                legalityInfo: new() { Legality.StandardLegal, Legality.ExtendedLegal }
            );

            
        }
    }

    public class GraviticDeflectionXWA : GraviticDeflection
    {
        public GraviticDeflectionXWA() : base()
        {
            UpgradeInfo.Cost = 3;
            UpgradeInfo.LegalityInfo = new() { Legality.XWA };
        }
    }
}

namespace Abilities.SecondEdition
{
    public class GraviticDeflectionAbility : GenericAbility
    {
        public override void ActivateAbility()
        {
            AddDiceModification(
                "Gravitic Deflection",
                IsAvailable,
                GetAiPriority,
                DiceModificationType.Reroll,
                GetDiceCount
            );
        }

        private int GetDiceCount()
        {
            List<GenericShip> shipsInAttackArc = new List<GenericShip>();

            foreach (GenericShip ship in Roster.AllShips.Values)
            {
                if (ship.IsTractored)
                {
                    ShotInfoArc inArcCheck = new ShotInfoArc(Combat.Attacker, ship, Combat.ArcForShot);
                    if (inArcCheck.InArc) shipsInAttackArc.Add(ship);
                }
            }

            return shipsInAttackArc.Count;
        }

        private int GetAiPriority()
        {
            return 90;
        }

        private bool IsAvailable()
        {
            return Combat.AttackStep == CombatStep.Defence
                && GetDiceCount() > 0;
        }

        public override void DeactivateAbility()
        {
            RemoveDiceModification();
        }
    }
}