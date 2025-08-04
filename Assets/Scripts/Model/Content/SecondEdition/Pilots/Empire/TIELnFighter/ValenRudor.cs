using Abilities.SecondEdition;
using BoardTools;
using Content;
using Ship;
using System.Collections.Generic;
using Upgrade;

namespace Ship
{
    namespace SecondEdition.TIELnFighter
    {
        public class ValenRudor : TIELnFighter
        {
            public ValenRudor() : base()
            {
                PilotInfo = new PilotCardInfo25
                (
                    "Valen Rudor",
                    "Braggadocious Baron",
                    Faction.Imperial,
                    3,
                    3,
                    5,
                    isLimited: true,
                    abilityType: typeof(ValenRudorAbility),
                    extraUpgradeIcons: new List<UpgradeType>
                    {
                        UpgradeType.Talent,
                        UpgradeType.Talent
                    },
                    tags: new List<Tags>
                    {
                        Tags.Tie
                    },
                    seImageNumber: 87,
                    legality: new List<Legality> { Legality.StandardLegal, Legality.ExtendedLegal }
                );
            }
        }

        public class ValenRudorXWA : ValenRudor
        {
            public ValenRudorXWA() : base()
            {
                (PilotInfo as PilotCardInfo25).Cost = 3;
                (PilotInfo as PilotCardInfo25).LoadoutValue = 5;
                (PilotInfo as PilotCardInfo25).LegalityInfo = new List<Legality> { Legality.XWA };
            }
        }
    }
}

namespace Abilities.SecondEdition
{
    public class ValenRudorAbility : GenericAbility
    {
        public override void ActivateAbility()
        {
            GenericShip.OnAttackFinishGlobal += RegisterValenRudorAbility;
        }

        public override void DeactivateAbility()
        {
            GenericShip.OnAttackFinishGlobal -= RegisterValenRudorAbility;
        }

        private void RegisterValenRudorAbility(GenericShip ship)
        {
            var distanceInfo = new DistanceInfo(HostShip, Combat.Defender);
            if (distanceInfo.Range <= 1 && Combat.Defender.Owner == HostShip.Owner)
            {
                RegisterAbilityTrigger(TriggerTypes.OnAttackFinish, PerformFreeAction);
            }
        }

        private void PerformFreeAction(object sender, System.EventArgs e)
        {
            var previousSelectedShip = Selection.ThisShip;
            Selection.ThisShip = HostShip;

            HostShip.AskPerformFreeAction(
                HostShip.GetAvailableActions(),
                delegate
                {
                    Selection.ThisShip = previousSelectedShip;
                    Triggers.FinishTrigger();
                },
                HostShip.PilotInfo.PilotName,
                "After a friendly ship at range 0-1 defends, you may perform an action",
                HostShip
            );
        }
    }
}
