﻿using System.Collections;
using System.Collections.Generic;
using Movement;
using ActionsList;
using Actions;
using Arcs;
using Upgrade;

namespace Ship
{
    namespace FirstEdition.TIEFoFighter
    {
        public class TIEFoFighter : GenericShip, TIE
        {

            public TIEFoFighter() : base()
            {
                ShipInfo = new ShipCardInfo
                (
                    "TIE/FO Fighter",
                    BaseSize.Small,
                    Faction.Imperial,
                    new ShipArcsInfo(ArcType.Front, 2), 3, 3, 1,
                    new ShipActionsInfo(
                        new ActionInfo(typeof(FocusAction)),
                        new ActionInfo(typeof(TargetLockAction)),
                        new ActionInfo(typeof(EvadeAction)),
                        new ActionInfo(typeof(BarrelRollAction))
                    ),
                    new ShipUpgradesInfo(
                        UpgradeType.Modification,
                        UpgradeType.Tech
                    ),
                    subFaction: Faction.FirstOrder
                );

                IconicPilots = new Dictionary<Faction, System.Type> {
                    { Faction.Imperial, typeof(OmegaLeader) }
                };

                ModelInfo = new ShipModelInfo(
                    "TIE/FO Fighter",
                    "First Order",
                    previewScale: 2f
                );

                DialInfo = new ShipDialInfo(
                    new ManeuverInfo(ManeuverSpeed.Speed1, ManeuverDirection.Left, ManeuverBearing.Turn, MovementComplexity.Normal),
                    new ManeuverInfo(ManeuverSpeed.Speed1, ManeuverDirection.Right, ManeuverBearing.Turn, MovementComplexity.Normal),

                    new ManeuverInfo(ManeuverSpeed.Speed2, ManeuverDirection.Left, ManeuverBearing.Turn, MovementComplexity.Easy),
                    new ManeuverInfo(ManeuverSpeed.Speed2, ManeuverDirection.Left, ManeuverBearing.Bank, MovementComplexity.Easy),
                    new ManeuverInfo(ManeuverSpeed.Speed2, ManeuverDirection.Forward, ManeuverBearing.Straight, MovementComplexity.Easy),
                    new ManeuverInfo(ManeuverSpeed.Speed2, ManeuverDirection.Right, ManeuverBearing.Bank, MovementComplexity.Easy),
                    new ManeuverInfo(ManeuverSpeed.Speed2, ManeuverDirection.Right, ManeuverBearing.Turn, MovementComplexity.Easy),
                    new ManeuverInfo(ManeuverSpeed.Speed2, ManeuverDirection.Left, ManeuverBearing.SegnorsLoop, MovementComplexity.Complex),
                    new ManeuverInfo(ManeuverSpeed.Speed2, ManeuverDirection.Right, ManeuverBearing.SegnorsLoop, MovementComplexity.Complex),

                    new ManeuverInfo(ManeuverSpeed.Speed3, ManeuverDirection.Left, ManeuverBearing.Turn, MovementComplexity.Normal),
                    new ManeuverInfo(ManeuverSpeed.Speed3, ManeuverDirection.Left, ManeuverBearing.Bank, MovementComplexity.Normal),
                    new ManeuverInfo(ManeuverSpeed.Speed3, ManeuverDirection.Forward, ManeuverBearing.Straight, MovementComplexity.Easy),
                    new ManeuverInfo(ManeuverSpeed.Speed3, ManeuverDirection.Right, ManeuverBearing.Bank, MovementComplexity.Normal),
                    new ManeuverInfo(ManeuverSpeed.Speed3, ManeuverDirection.Right, ManeuverBearing.Turn, MovementComplexity.Normal),

                    new ManeuverInfo(ManeuverSpeed.Speed4, ManeuverDirection.Forward, ManeuverBearing.Straight, MovementComplexity.Normal),
                    new ManeuverInfo(ManeuverSpeed.Speed4, ManeuverDirection.Forward, ManeuverBearing.KoiogranTurn, MovementComplexity.Complex),

                    new ManeuverInfo(ManeuverSpeed.Speed5, ManeuverDirection.Forward, ManeuverBearing.Straight, MovementComplexity.Normal)
                );

                SoundInfo = new ShipSoundInfo(
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

                ShipIconLetter = 'O';

                HotacManeuverTable = new AI.TIEFOTable();
            }
        }
    }
}
