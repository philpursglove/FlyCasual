using Abilities.SecondEdition;
using Content;
using System.Collections.Generic;
using Upgrade;

namespace Ship.SecondEdition.BTLA4YWing
{
    public class PopsKrailBoY : BTLA4YWing
    {
        public PopsKrailBoY() : base()
        {
            PilotInfo = new PilotCardInfo25
            (
                "\"Pops\" Krail",
                "Battle of Yavin",
                Faction.Rebel,
                4,
                4,
                0,
                isLimited: true,
                abilityType: typeof(PopsKrailBoYAbility),
                extraUpgradeIcons: new List<UpgradeType>
                {
                    UpgradeType.Turret,
                    UpgradeType.Torpedo,
                    UpgradeType.Astromech
                },
                tags: new List<Tags>
                {
                    Tags.YWing
                },
                isStandardLayout: true
            );

            ShipAbilities.Add(new HopeAbility());

            MustHaveUpgrades.Add(typeof(UpgradesList.SecondEdition.IonCannonTurret));
            MustHaveUpgrades.Add(typeof(UpgradesList.SecondEdition.AdvProtonTorpedoes));
            MustHaveUpgrades.Add(typeof(UpgradesList.SecondEdition.R4Astromech));

            PilotNameCanonical = "popskrail-battleofyavin";
        }
    }

    public class PopsKrailBoYXWA : PopsKrailBoY
    {
        public PopsKrailBoYXWA() : base()
        {
            var pilot = (PilotCardInfo25)PilotInfo;
            pilot.Cost = 4;
            pilot.LegalityInfo = new List<Legality> { Legality.XWA };
        }
    }
}

namespace Abilities.SecondEdition
{
    public class PopsKrailBoYAbility : GenericAbility
    {
        public override void ActivateAbility()
        {
            AddDiceModification
            (
                HostShip.PilotInfo.PilotName,
                IsAvailable,
                GetAiPriority,
                DiceModificationType.Reroll,
                2
            );
        }

        private int GetAiPriority()
        {
            return 90;
        }

        private bool IsAvailable()
        {
            return Combat.AttackStep == CombatStep.Attack
                && Combat.ArcForShot.ArcType == Arcs.ArcType.SingleTurret;
        }

        public override void DeactivateAbility()
        {
            RemoveDiceModification();
        }
    }
}