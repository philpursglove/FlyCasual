using Abilities.SecondEdition;
using BoardTools;
using Content;
using Ship;
using System;
using System.Collections.Generic;
using Tokens;
using Upgrade;

namespace Ship.SecondEdition.TIEInterceptor
{
    public class CienaRee : TIEInterceptor
    {
        public CienaRee() : base()
        {
            PilotInfo = new PilotCardInfo25
            (
                "Ciena Ree",
                "Look Through My Eyes",
                Faction.Imperial,
                6,
                5,
                14,
                isLimited: true,
                abilityType: typeof(CienaReeAbility),
                extraUpgradeIcons: new List<UpgradeType>()
                {
                    UpgradeType.Talent,
                    UpgradeType.Talent,
                    UpgradeType.Modification,
                    UpgradeType.Configuration
                },
                tags: new List<Tags>
                {
                    Tags.Tie
                },
                legality: new List<Legality> { Legality.StandardLegal, Legality.ExtendedLegal }
            );
        }
    }

    public class CienaReeXWA : CienaRee
    {
        public CienaReeXWA() : base()
        {
            (PilotInfo as PilotCardInfo25).Cost = 4;
            (PilotInfo as PilotCardInfo25).LoadoutValue = 4;
            (PilotInfo as PilotCardInfo25).LegalityInfo = new List<Legality> { Legality.XWA };
        }
    }
}

namespace Abilities.SecondEdition
{
    public class CienaReeAbility : GenericAbility
    {
        public override void ActivateAbility()
        {
            HostShip.OnAttackFinishAsAttacker += CheckKillAbility;
            GenericShip.OnShipIsDestroyedGlobal += CheckDestroyedAbility;
        }

        public override void DeactivateAbility()
        {
            HostShip.OnAttackFinishAsAttacker -= CheckKillAbility;
            GenericShip.OnShipIsDestroyedGlobal -= CheckDestroyedAbility;
        }

        private void CheckKillAbility(GenericShip ship)
        {
            if (Combat.Defender.IsDestroyed) RegisterAbilityTrigger(TriggerTypes.OnAttackFinish, GetStress);
        }

        private void GetStress(object sender, EventArgs e)
        {
            Messages.ShowInfo($"{HostShip.PilotInfo.PilotName} gains 1 stress token");
            HostShip.Tokens.AssignToken(typeof(StressToken), Triggers.FinishTrigger);
        }

        private void CheckDestroyedAbility(GenericShip ship, bool flag)
        {
            if (Tools.IsFriendly(HostShip, ship))
            {
                DistanceInfo distInfo = new DistanceInfo(HostShip, ship);
                if (distInfo.Range <= 3)
                {
                    RegisterAbilityTrigger(TriggerTypes.OnShipIsDestroyed, RemoveStressToken);
                }
            }
        }

        private void RemoveStressToken(object sender, EventArgs e)
        {
            if (HostShip.Tokens.HasToken<StressToken>())
            {
                Messages.ShowInfo($"{HostShip.PilotInfo.PilotName} removes 1 stress token");
                HostShip.Tokens.RemoveToken(typeof(StressToken), Triggers.FinishTrigger);
            }
            else
            {
                Triggers.FinishTrigger();
            }
        }
    }
}