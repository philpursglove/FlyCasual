using Actions;
using ActionsList;
using BoardTools;
using Content;
using Ship;
using System;
using System.Collections.Generic;
using System.Linq;
using Tokens;
using Upgrade;

namespace Ship
{
    namespace SecondEdition.TIEFoFighter
    {
        public class LieutenantGalek : TIEFoFighter
        {
            public LieutenantGalek() : base()
            {
                PilotInfo = new PilotCardInfo25
                (
                    "Lieutenant Galek",
                    "Harsh Instructor",
                    Faction.FirstOrder,
                    5,
                    3,
                    7,
                    isLimited: true,
                    abilityType: typeof(Abilities.SecondEdition.LieutenantGalekAbility),
                    extraUpgradeIcons: new List<UpgradeType>()
                    {
                        UpgradeType.Talent,
                        UpgradeType.Tech,
                        UpgradeType.Tech,
                        UpgradeType.Cannon,
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

        public class LieutenantGalekXWA : LieutenantGalek
        {
            public LieutenantGalekXWA() : base()
            {
                var pilot = (PilotCardInfo25) PilotInfo;
                pilot.LegalityInfo = new List<Legality> {Legality.XWA};
                pilot.Cost = 4;
                pilot.LoadoutValue = 15;
                pilot.ExtraUpgrades = new List<UpgradeType>
                {
                    UpgradeType.Talent,
                    UpgradeType.Tech,
                    UpgradeType.Tech,
                    UpgradeType.Sensor,
                    UpgradeType.Missile,
                    UpgradeType.Modification
                };
            }
        }
    }
}

namespace Abilities.SecondEdition
{
    public class LieutenantGalekAbility : GenericAbility
    {
        private GenericShip PreviousCurrentShip { get; set; }
        public override void ActivateAbility()
        {
            GenericShip.OnShipIsDestroyedGlobal += CheckAbility;
        }

        public override void DeactivateAbility()
        {
            GenericShip.OnShipIsDestroyedGlobal -= CheckAbility;
        }

        private void CheckAbility(GenericShip ship, bool flag)
        {
            if (!Tools.IsFriendly(HostShip, ship) || Tools.IsSameShip(HostShip, ship)) return;

            DistanceInfo distanceInfo = new DistanceInfo(HostShip, ship);
            if (distanceInfo.Range > 2) return;

            RegisterAbilityTrigger(
                TriggerTypes.OnShipIsDestroyed,
                AskWhatToDo,
                customTriggerName: $"{HostShip.PilotInfo.PilotName} (ID: {HostShip.ShipId})"
            );
        }

        private void AskWhatToDo(object sender, EventArgs e)
        {
            PreviousCurrentShip = Selection.ThisShip;

            Selection.ChangeActiveShip(HostShip);
            HostShip.OnCheckCoordinateModeModification += SetCustomCoordinateMode;
            Selection.ThisShip.AskPerformFreeAction(
                new CoordinateAction() { CanBePerformedWhileStressed = true },
                FinishAbility,
                descriptionShort: HostShip.PilotInfo.PilotName,
                descriptionLong: "You may perform a Coordinate action, even while stressed",
                imageHolder: HostShip
            );
        }

        private void FinishAbility()
        {
            Selection.ChangeActiveShip(PreviousCurrentShip);
            Triggers.FinishTrigger();
        }

        private void SetCustomCoordinateMode(ref CoordinateActionData coordinateActionData)
        {
            coordinateActionData.SameActionLimit = true;

            coordinateActionData.GetAiPriority = GetAiPriority;

            HostShip.OnCheckCoordinateModeModification -= SetCustomCoordinateMode;
        }

        private int GetAiPriority(GenericShip ship)
        {
            int result = 0;

            result += NeedTokenPriority(ship);
            result += ship.PilotInfo.Cost + ship.UpgradeBar.GetUpgradesOnlyFaceup().Sum(n => n.UpgradeInfo.Cost);

            return result;
        }

        private int NeedTokenPriority(GenericShip ship)
        {
            if (!ship.Tokens.HasToken(typeof(FocusToken))) return 100;
            if (ship.ActionBar.HasAction(typeof(EvadeAction)) && !ship.Tokens.HasToken(typeof(EvadeToken))) return 50;
            if (ship.ActionBar.HasAction(typeof(TargetLockAction)) && !ship.Tokens.HasToken(typeof(BlueTargetLockToken), '*')) return 50;
            return 0;
        }
    }
}