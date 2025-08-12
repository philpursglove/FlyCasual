using BoardTools;
using Content;
using Movement;
using Ship;
using System.Collections.Generic;
using Tokens;
using Upgrade;

namespace Ship
{
    namespace SecondEdition.FangFighter
    {
        public class KadSolus : FangFighter
        {
            public KadSolus() : base()
            {
                PilotInfo = new PilotCardInfo25
                (
                    "Kad Solus",
                    "Skilled Commando",
                    Faction.Scum,
                    4,
                    4,
                    8,
                    isLimited: true,
                    abilityType: typeof(Abilities.SecondEdition.KadSolusAbility),
                    extraUpgradeIcons: new List<UpgradeType>
                    {
                        UpgradeType.Talent,
                        UpgradeType.Modification,
                        UpgradeType.Modification,
                        UpgradeType.Missile
                    },
                    tags: new List<Tags>
                    {
                        Tags.Mandalorian
                    },
                    seImageNumber: 158,
                    skinName: "Skull Squadron",
                    legality: new List<Legality> { Legality.StandardLegal, Legality.ExtendedLegal }
                );
            }
        }

        public class KadSolusXWA : KadSolus
        {
            public KadSolusXWA() : base()
            {
                (PilotInfo as PilotCardInfo25).Cost = 4;
                (PilotInfo as PilotCardInfo25).LoadoutValue = 8;
                (PilotInfo as PilotCardInfo25).LegalityInfo = new List<Legality> { Legality.XWA };
            }
        }
    }
}

namespace Abilities.SecondEdition
{
    //After you fully execute a red maneuver, gain 2 focus tokens.

    public class KadSolusAbility : GenericAbility
    {
        public override void ActivateAbility()
        {
            HostShip.OnMovementFinish += RegisterTrigger;
        }

        public override void DeactivateAbility()
        {
            HostShip.OnMovementFinish -= RegisterTrigger;
        }

        private void RegisterTrigger(GenericShip ship)
        {
            if (CheckAbility())
            {
                RegisterAbilityTrigger(TriggerTypes.OnMovementFinish, AssignTokens);
            }
        }

        private bool CheckAbility()
        {
            if (HostShip.IsBumped) return false;
            if (HostShip.GetLastManeuverColor() != MovementComplexity.Complex) return false;
            if (Board.IsOffTheBoard(HostShip)) return false;

            return true;
        }

        private void AssignTokens(object sender, System.EventArgs e)
        {
            HostShip.Tokens.AssignTokens(() => new FocusToken(HostShip), 2, Triggers.FinishTrigger);
        }
    }
}