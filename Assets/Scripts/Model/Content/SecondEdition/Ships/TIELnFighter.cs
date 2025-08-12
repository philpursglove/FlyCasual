using Actions;
using ActionsList;
using Arcs;
using BoardTools;
using Movement;
using Ship;
using Ship.CardInfo;
using SubPhases;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Ship.SecondEdition.TIELnFighter
{
    public class TIELnFighter : GenericShip
    {
        public TIELnFighter() : base()
        {
            ShipInfo = new ShipCardInfo25
            (
                "TIE/ln Fighter",
                BaseSize.Small,
                new FactionData
                (
                    new Dictionary<Faction, Type>
                    {
                        { Faction.Imperial, typeof(BlackSquadronAce) },
                        { Faction.Rebel, typeof(ZebOrrelios) }
                    }
                ),
                new ShipArcsInfo(ArcType.Front, 2), 3, 3, 0,
                new ShipActionsInfo
                (
                    new ActionInfo(typeof(FocusAction)),
                    new ActionInfo(typeof(EvadeAction)),
                    new ActionInfo(typeof(BarrelRollAction))
                ),
                new ShipUpgradesInfo()
            );

            ModelInfo = new ShipModelInfo
            (
                "TIE Fighter",
                "Gray",
                previewScale: 2f
            );

            DialInfo = new ShipDialInfo
            (
                new ManeuverInfo(ManeuverSpeed.Speed1, ManeuverDirection.Left, ManeuverBearing.Turn, MovementComplexity.Normal),
                new ManeuverInfo(ManeuverSpeed.Speed1, ManeuverDirection.Right, ManeuverBearing.Turn, MovementComplexity.Normal),

                new ManeuverInfo(ManeuverSpeed.Speed2, ManeuverDirection.Left, ManeuverBearing.Turn, MovementComplexity.Normal),
                new ManeuverInfo(ManeuverSpeed.Speed2, ManeuverDirection.Left, ManeuverBearing.Bank, MovementComplexity.Easy),
                new ManeuverInfo(ManeuverSpeed.Speed2, ManeuverDirection.Forward, ManeuverBearing.Straight, MovementComplexity.Easy),
                new ManeuverInfo(ManeuverSpeed.Speed2, ManeuverDirection.Right, ManeuverBearing.Bank, MovementComplexity.Easy),
                new ManeuverInfo(ManeuverSpeed.Speed2, ManeuverDirection.Right, ManeuverBearing.Turn, MovementComplexity.Normal),

                new ManeuverInfo(ManeuverSpeed.Speed3, ManeuverDirection.Left, ManeuverBearing.Turn, MovementComplexity.Normal),
                new ManeuverInfo(ManeuverSpeed.Speed3, ManeuverDirection.Left, ManeuverBearing.Bank, MovementComplexity.Normal),
                new ManeuverInfo(ManeuverSpeed.Speed3, ManeuverDirection.Forward, ManeuverBearing.Straight, MovementComplexity.Easy),
                new ManeuverInfo(ManeuverSpeed.Speed3, ManeuverDirection.Right, ManeuverBearing.Bank, MovementComplexity.Normal),
                new ManeuverInfo(ManeuverSpeed.Speed3, ManeuverDirection.Right, ManeuverBearing.Turn, MovementComplexity.Normal),
                new ManeuverInfo(ManeuverSpeed.Speed3, ManeuverDirection.Forward, ManeuverBearing.KoiogranTurn, MovementComplexity.Complex),

                new ManeuverInfo(ManeuverSpeed.Speed4, ManeuverDirection.Forward, ManeuverBearing.Straight, MovementComplexity.Normal),
                new ManeuverInfo(ManeuverSpeed.Speed4, ManeuverDirection.Forward, ManeuverBearing.KoiogranTurn, MovementComplexity.Complex),

                new ManeuverInfo(ManeuverSpeed.Speed5, ManeuverDirection.Forward, ManeuverBearing.Straight, MovementComplexity.Normal)
            );

            SoundInfo = new ShipSoundInfo
            (
                new List<string>()
                {
                    "TIE-Fly1",
                    "TIE-Fly2",
                    "TIE-Fly3",
                    "TIE-Fly4",
                    "TIE-Fly5",
                    "TIE-Fly6",
                    "TIE-Fly7"
                },
                "TIE-Fire", 2
            );

            ShipIconLetter = 'F';
        }
    }
}

namespace Abilities.SecondEdition
{
    public class FormedUpAbility : GenericAbility
    {
        public override void ActivateAbility()
        {
            Phases.Events.OnRoundEnd += CheckEndPhaseAbility;
        }

        public override void DeactivateAbility()
        {
            Phases.Events.OnRoundEnd -= CheckEndPhaseAbility;
        }

        private void CheckEndPhaseAbility()
        {
            if (HostShip.Tokens.GetNonLockRedTokens().Count > 0
                && hasFriendlyShipsInRange())
            {
                RegisterAbilityTrigger(TriggerTypes.OnRoundEnd, AskRemoveToken);
            }
        }

        private void AskRemoveToken(object sender, EventArgs e)
        {
            Selection.ChangeActiveShip(HostShip);
            FormedUpRemoveRedTokenAbilityDecisionSubPhase subphase = Phases.StartTemporarySubPhaseNew<FormedUpRemoveRedTokenAbilityDecisionSubPhase>(
                "Formed Up: You may remove 1 non-lock red token",
                Triggers.FinishTrigger
            );
            subphase.ImageSource = HostReal as IImageHolder;
            subphase.AbilityHostShip = HostShip;
            subphase.RemoveOnlyNonLocks = true;
            subphase.Start();
        }

        private class FormedUpRemoveRedTokenAbilityDecisionSubPhase : RemoveRedTokenDecisionSubPhase
        {
            public GenericShip AbilityHostShip;

            public override void PrepareCustomDecisions()
            {
                DescriptionShort = AbilityHostShip.PilotInfo.PilotName;
                DescriptionLong = "You may remove 1 non-lock red token";

                DecisionOwner = Selection.ThisShip.Owner;
                DefaultDecisionName = decisions.First().Name;
            }
        }

        private bool hasFriendlyShipsInRange()
        {
            List<GenericShip> friendlyTies = Board.GetShipsAtRange(HostShip, new Vector2(0, 1), Team.Type.Friendly).Where(n => n is Ship.SecondEdition.TIELnFighter.TIELnFighter).ToList();
            return friendlyTies.Count > 1;
        }
    }
}
