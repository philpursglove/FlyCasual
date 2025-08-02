using Content;
using Ship;
using System.Collections.Generic;
using Tokens;
using Upgrade;

namespace Ship
{
    namespace SecondEdition.YT2400LightFreighter
    {
        public class Leebo : YT2400LightFreighter
        {
            public Leebo() : base()
            {
                PilotInfo = new PilotCardInfo25
                (
                    "Leebo",
                    "Wisdom of Ages",
                    Faction.Rebel,
                    3,
                    6,
                    16,
                    isLimited: true,
                    abilityType: typeof(Abilities.SecondEdition.LeeboAbility),
                    tags: new List<Tags>
                    {
                        Tags.Droid,
                        Tags.Freighter
                    },
                    extraUpgradeIcons: new List<UpgradeType>()
                    {
                        UpgradeType.Talent,
                        UpgradeType.Missile,
                        UpgradeType.Illicit,
                        UpgradeType.Illicit,
                        UpgradeType.Modification,
                        UpgradeType.Title
                    },
                    legality: new List<Legality> { Legality.StandardLegal, Legality.ExtendedLegal }
                );

                ImageUrl = "https://infinitearenas.com/xw2/images/pilots/leebo-wisdomofages.png";

                ShipInfo.ActionIcons.SwitchToDroidActions();
            }
        }

        public class LeeboXWA : Leebo
        {
            public LeeboXWA() : base()
            {
                (PilotInfo as PilotCardInfo25).Cost = 6;
                (PilotInfo as PilotCardInfo25).LoadoutValue = 7;
                (PilotInfo as PilotCardInfo25).ExtraUpgrades = new List<UpgradeType>
                { 
                    UpgradeType.Talent,
                    UpgradeType.Crew,
                    UpgradeType.Illicit,
                    UpgradeType.Illicit,
                    UpgradeType.Missile,
                    UpgradeType.Title
                };
                (PilotInfo as PilotCardInfo25).LegalityInfo = new List<Legality> { Legality.XWA };
            }
        }
    }
}

namespace Abilities.SecondEdition
{
    public class LeeboAbility : GenericAbility
    {
        private bool _spentCalculate;

        public override void ActivateAbility()
        {
            HostShip.OnAttackFinish += CheckAssignCalculate;
            HostShip.OnTokenIsSpent += CheckCalculateSpent;
        }

        public override void DeactivateAbility()
        {
            HostShip.OnAttackFinish -= CheckAssignCalculate;
            HostShip.OnTokenIsSpent -= CheckCalculateSpent;
        }

        private void CheckCalculateSpent(GenericShip ship, GenericToken token)
        {
            if (Combat.AttackStep == CombatStep.None)
                return;

            if (HostShip != Combat.Attacker && HostShip != Combat.Defender)
                return;

            if (!(token is CalculateToken))
                return;

            _spentCalculate = true;
        }

        private void CheckAssignCalculate(GenericShip ship)
        {
            if (_spentCalculate)
            {
                _spentCalculate = false;
                Triggers.RegisterTrigger(new Trigger()
                {
                    Name = "Assign calculate to Leebo.",
                    TriggerType = TriggerTypes.OnAttackFinish,
                    TriggerOwner = HostShip.Owner.PlayerNo,
                    EventHandler = AssignCalculateToken
                });
            }
        }
        private void AssignCalculateToken(object sender, System.EventArgs e)
        {
            Messages.ShowInfo("Calculate token is assigned to " + HostShip.PilotInfo.PilotName);
            HostShip.Tokens.AssignToken(new Tokens.CalculateToken(HostShip), Triggers.FinishTrigger);
        }
    }
}