using Abilities.SecondEdition;
using Content;
using Ship;
using Ship.SecondEdition.T65XWing;
using System;
using System.Collections.Generic;
using Tokens;
using Upgrade;

namespace Ship
{
    namespace SecondEdition.UT60DUWing
    {
        public class K2SO : UT60DUWing
        {
            public K2SO() : base()
            {
                PilotInfo = new PilotCardInfo25
                (
                    "K-2SO",
                    "Cassian Said I Had To",
                    Faction.Rebel,
                    3,
                    5,
                    10,
                    isLimited: true,
                    abilityType: typeof(K2SOPilotAbility),
                    extraUpgradeIcons: new List<UpgradeType>
                    {
                        UpgradeType.Crew,
                        UpgradeType.Crew,
                        UpgradeType.Sensor,
                        UpgradeType.Modification,
                        UpgradeType.Configuration
                    },
                    tags: new List<Tags>
                    {
                        Tags.Droid
                    },
                    legality: new List<Legality> { Legality.StandardLegal, Legality.ExtendedLegal }
                );

                ShipInfo.ActionIcons.SwitchToDroidActions();

                ImageUrl = "https://images-cdn.fantasyflightgames.com/filer_public/56/cb/56cb9ec8-0eff-4c6f-acda-f54413baadc7/swz66_k-2so.png";
            }
        }

        public class K2SOXWA : K2SO
        {
            public K2SOXWA() : base()
            {
                (PilotInfo as PilotCardInfo25).Cost = 4;
                (PilotInfo as PilotCardInfo25).LoadoutValue = 6;
                (PilotInfo as PilotCardInfo25).LegalityInfo = new List<Legality> { Legality.XWA };
            }
        }
    }
}

namespace Abilities.SecondEdition
{
    public class K2SOPilotAbility : GenericAbility
    {
        public override void ActivateAbility()
        {
            HostShip.OnTokenIsAssigned += RegisterK2SOAbility;
        }

        public override void DeactivateAbility()
        {
            HostShip.OnTokenIsAssigned -= RegisterK2SOAbility;
        }

        private void RegisterK2SOAbility(GenericShip ship, GenericToken token)
        {
            if (token is StressToken)
            {
                RegisterAbilityTrigger(TriggerTypes.OnTokenIsAssigned, AssignToken);
            }
        }

        private void AssignToken(object sender, EventArgs e)
        {
            Messages.ShowInfo(HostShip.PilotInfo.PilotName + ": Calculate Token is assigned");
            HostShip.Tokens.AssignToken(typeof(CalculateToken), Triggers.FinishTrigger);
        }
    }
}
