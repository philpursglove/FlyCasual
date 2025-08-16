using Arcs;
using Content;
using System.Collections.Generic;
using Upgrade;

namespace UpgradesList.SecondEdition
{
    public class Marksmanship : GenericUpgrade
    {
        public Marksmanship() : base()
        {
            UpgradeInfo = new UpgradeCardInfo(
                "Marksmanship",
                UpgradeType.Talent,
                cost: 1,
                abilityType: typeof(Abilities.SecondEdition.MarksmanshipAbility),
                seImageNumber: 10,
                legalityInfo: new() { Legality.StandardLegal, Legality.ExtendedLegal }
            );
        }        
    }

    public class MarksmanshipXWA : Marksmanship
    {
        public MarksmanshipXWA() : base()
        {
            UpgradeInfo.Cost = 2;
            UpgradeInfo.LegalityInfo = new() { Legality.XWA };
        }
    }
}

namespace Abilities.SecondEdition
{
    //While performing an attack, if the defender is in your bullseye firing arc, you may change one hit result to a critical hit result.
    public class MarksmanshipAbility : GenericAbility
    {
        public override void ActivateAbility()
        {
            AddDiceModification(
                HostUpgrade.UpgradeInfo.Name,
                IsDiceModificationAvailable,
                GetDiceModificationAiPriority,
                DiceModificationType.Change,
                1,
                new List<DieSide>() { DieSide.Success },
                DieSide.Crit
            );
        }

        public override void DeactivateAbility()
        {
            RemoveDiceModification();
        }

        private bool IsDiceModificationAvailable()
        {
            return (Combat.AttackStep == CombatStep.Attack
                && Combat.Attacker == HostShip
                && Combat.Attacker.SectorsInfo.IsShipInSector(Combat.Defender, ArcType.Bullseye)
            );
        }

        private int GetDiceModificationAiPriority()
        {
            return 20;
        }
    }
}