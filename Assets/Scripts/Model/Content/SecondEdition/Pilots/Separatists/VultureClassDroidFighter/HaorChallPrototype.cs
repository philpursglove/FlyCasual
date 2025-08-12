using ActionsList;
using Arcs;
using Content;
using Ship;
using System;
using System.Collections.Generic;
using Upgrade;

namespace Ship.SecondEdition.VultureClassDroidFighter
{
    public class HaorChallPrototype : VultureClassDroidFighter
    {
        public HaorChallPrototype()
        {
            PilotInfo = new PilotCardInfo25
            (
                "Haor Chall Prototype",
                "Xi Char Offering",
                Faction.Separatists,
                1,
                2,
                4,
                limited: 2,
                abilityType: typeof(Abilities.SecondEdition.HaorChallPrototypeAbility),
                extraUpgradeIcons: new List<UpgradeType>
                {
                    UpgradeType.Missile,
                    UpgradeType.Modification,
                    UpgradeType.Configuration
                },
                tags: new List<Tags>
                {
                    Tags.Droid
                },
                skinName: "Gray",
                legality: new List<Legality> { Legality.StandardLegal, Legality.ExtendedLegal }
            );
        }
    }

    public class HaorChallPrototypeXWA : HaorChallPrototype
    {
        public HaorChallPrototypeXWA() : base()
        {
            (PilotInfo as PilotCardInfo25).Cost = 2;
            (PilotInfo as PilotCardInfo25).LoadoutValue = 2;
            (PilotInfo as PilotCardInfo25).ExtraUpgrades = new List<UpgradeType>
            {
                UpgradeType.Modification,
                UpgradeType.Missile,
                UpgradeType.Configuration,
            };
            (PilotInfo as PilotCardInfo25).LegalityInfo = new List<Legality> { Legality.XWA };
        }
    }
}

namespace Abilities.SecondEdition
{
    //After an Enemy ship in your bullseye arc at range 0-2 declares another friendly ship as the defender, 
    //you may perform a calculate or lock action.
    public class HaorChallPrototypeAbility : GenericAbility
    {
        public override void ActivateAbility()
        {
            GenericShip.OnAttackStartAsAttackerGlobal += CheckAbility;
        }

        public override void DeactivateAbility()
        {
            GenericShip.OnAttackStartAsAttackerGlobal -= CheckAbility;
        }

        private void CheckAbility()
        {
            if (Combat.Defender.Owner == HostShip.Owner
                && Combat.Defender != HostShip
                && HostShip.SectorsInfo.IsShipInSector(Combat.Attacker, ArcType.Bullseye)
                && new BoardTools.DistanceInfo(HostShip, Combat.Attacker).Range <= 2)
            {
                RegisterAbilityTrigger(TriggerTypes.OnAttackStart, AskPerformAction);
            }
        }

        private void AskPerformAction(object sender, EventArgs e)
        {
            GenericShip previousActiveShip = Selection.ThisShip;
            Selection.ChangeActiveShip(HostShip);

            List<GenericAction> actions = new List<GenericAction>() { new CalculateAction(), new TargetLockAction() };
            HostShip.AskPerformFreeAction(
                actions,
                delegate {
                    Selection.ChangeActiveShip(previousActiveShip);
                    Triggers.FinishTrigger();
                },
                HostShip.PilotInfo.PilotName,
                "After an Enemy ship in your bullseye arc at range 0-2 declares another friendly ship as the defender, you may perform a Calculate or Lock action",
                HostShip
            );
        }
    }
}
