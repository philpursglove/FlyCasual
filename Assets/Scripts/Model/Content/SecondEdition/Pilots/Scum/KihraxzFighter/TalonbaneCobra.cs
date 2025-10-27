using Content;
using System.Collections.Generic;
using Upgrade;

namespace Ship
{
    namespace SecondEdition.KihraxzFighter
    {
        public class TalonbaneCobra : KihraxzFighter
        {
            public TalonbaneCobra() : base()
            {
                PilotInfo = new PilotCardInfo25
                (
                    "Talonbane Cobra",
                    "Scourge of Tansarii Point",
                    Faction.Scum,
                    5,
                    5,
                    12,
                    isLimited: true,
                    abilityType: typeof(Abilities.SecondEdition.TalonbaneCobraAbility),
                    extraUpgradeIcons: new List<UpgradeType>()
                    {
                        UpgradeType.Talent,
                        UpgradeType.Illicit,
                        UpgradeType.Illicit,
                        UpgradeType.Modification,
                        UpgradeType.Modification,
                        UpgradeType.Missile
                    },
                    seImageNumber: 191,
                    legality: new List<Legality>() { Legality.ExtendedLegal }
                );
            }
        }

        public class TalonbaneCobraXWA : TalonbaneCobra
        {
            public TalonbaneCobraXWA() : base()
            {
                (PilotInfo as PilotCardInfo25).Cost = 12;
                (PilotInfo as PilotCardInfo25).LoadoutValue = 13;
                (PilotInfo as PilotCardInfo25).LegalityInfo = new List<Legality> { Legality.XWA };
            }
        }
    }
}

namespace Abilities.SecondEdition
{
    public class TalonbaneCobraAbility : GenericAbility
    {
        public override void ActivateAbility()
        {
            HostShip.AfterGotNumberOfAttackDice += TalonbaneCobraDiceCheck;
            HostShip.AfterGotNumberOfDefenceDice += TalonbaneCobraDiceCheck;
        }

        public override void DeactivateAbility()
        {
            HostShip.AfterGotNumberOfAttackDice -= TalonbaneCobraDiceCheck;
            HostShip.AfterGotNumberOfDefenceDice -= TalonbaneCobraDiceCheck;
        }

        private void TalonbaneCobraDiceCheck(ref int diceCount)
        {
            if (Combat.AttackStep == CombatStep.Attack && Combat.ShotInfo.Range == 1)
            {
                Messages.ShowInfo(HostShip.PilotInfo.PilotName + " is attacking at range 1, gaining +1 attack die");
                diceCount++;
            }
            if (Combat.AttackStep == CombatStep.Defence && Combat.ShotInfo.Range == 3)
            {
                Messages.ShowInfo(HostShip.PilotInfo.PilotName + " is defending at range 3, gaining +1 defense die");
                diceCount++;
            }
        }
    }
}