using BoardTools;
using Bombs;
using Content;
using Movement;
using Obstacles;
using System;
using System.Collections.Generic;
using Upgrade;

namespace UpgradesList.SecondEdition
{
    public class ElectroChaffMissiles : GenericTimedBombSE
    {
        private ElectroChaffCloud chaffCloud;

        public ElectroChaffMissiles() : base()
        {
            UpgradeInfo = new UpgradeCardInfo
            (
                "Electro-Chaff Missiles",
                types: new List<UpgradeType>()
                {
                    UpgradeType.Missile,
                    UpgradeType.Device
                },
                cost: 4,
                limited: 2,
                charges: 1,
                cannotBeRecharged: true,
                subType: UpgradeSubType.Bomb,
                legalityInfo: new() { Legality.StandardLegal, Legality.ExtendedLegal }
            );

            detonationRange = 2;
            bombPrefabPath = "Prefabs/Bombs/ElectroChaffCloud";
        }

        public override List<ManeuverTemplate> GetDefaultDropTemplates()
        {
            return new List<ManeuverTemplate>();
        }

        public override List<ManeuverTemplate> GetDefaultLaunchTemplates()
        {
           return new List<ManeuverTemplate>()
            {
                new (ManeuverBearing.Straight, ManeuverDirection.Forward, ManeuverSpeed.Speed4),
                new (ManeuverBearing.Bank, ManeuverDirection.Left, ManeuverSpeed.Speed3),
                new (ManeuverBearing.Bank, ManeuverDirection.Right, ManeuverSpeed.Speed3)
            };
        }

        public override void ActivateBombs(List<GenericDeviceGameObject> bombObjects, Action callBack)
        {
            CurrentBombObjects.AddRange(bombObjects);
            HostShip.IsBombAlreadyDropped = true;
            BombsManager.RegisterBombs(bombObjects, this);
            PayDropCost(callBack);

            Phases.Events.OnEndPhaseStart_Triggers += base.PlanTimedDetonation;

            foreach (GenericDeviceGameObject bombObject in bombObjects)
            {
                chaffCloud = new ElectroChaffCloud("Electro-Chaff Cloud", "electro-chaffcloud", HostShip.Owner);
                chaffCloud.Spawn("Electro-Chaff Cloud " + HostShip.ShipId, Board.GetBoard());
                ObstaclesManager.AddObstacle(chaffCloud);

                chaffCloud.ObstacleGO.transform.position = bombObject.transform.position;
                chaffCloud.ObstacleGO.transform.eulerAngles = bombObject.transform.eulerAngles;
                chaffCloud.IsPlaced = true;
                bombObject.Fuses++;
            }
        }

        protected override void Detonate()
        {
            ObstaclesManager.DestroyObstacle(chaffCloud);
            Phases.Events.OnEndPhaseStart_Triggers -= base.PlanTimedDetonation;
            base.Detonate();
        }
    }

    public class ElectroChaffMissilesXWA : ElectroChaffMissiles
    {
        public ElectroChaffMissilesXWA() : base()
        {
            UpgradeInfo.Cost = 11;
            UpgradeInfo.LegalityInfo = new() { Legality.XWA };
        }
    }
}