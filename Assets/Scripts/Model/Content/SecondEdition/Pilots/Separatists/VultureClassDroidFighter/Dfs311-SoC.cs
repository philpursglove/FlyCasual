using Content;
using Ship;
using SubPhases;
using System;
using System.Collections.Generic;
using Upgrade;
using UpgradesList.SecondEdition;

namespace Ship.SecondEdition.VultureClassDroidFighter
{
    public class Dfs311SoC : VultureClassDroidFighter
    {
        public Dfs311SoC()
        {
            PilotInfo = new PilotCardInfo25
            (
                "DFS-311",
                "Siege of Coruscant",
                Faction.Separatists,
                1,
                3,
                0,
                true,
                abilityType: typeof(Abilities.SecondEdition.Dfs311SoCAbility),
                extraUpgradeIcons: new List<UpgradeType>
                {
                    UpgradeType.Missile,
                    UpgradeType.Modification,
                    UpgradeType.Configuration
                },
                tags: new List<Tags>
                {
                    Tags.Droid
                },
                isStandardLayout: true,
                legality: new List<Legality> { Legality.StandardLegal, Legality.ExtendedLegal }
            );

            MustHaveUpgrades.Add(typeof(DiscordMissiles));
            MustHaveUpgrades.Add(typeof(ContingencyProtocol));
            MustHaveUpgrades.Add(typeof(StrutLockOverride));

            PilotNameCanonical = "dfs311-siegeofcoruscant";
        }
    }

    public class Dfs311SoCXWA : Dfs311SoC
    {
        public Dfs311SoCXWA() : base()
        {
            (PilotInfo as PilotCardInfo25).Cost = 2;
            (PilotInfo as PilotCardInfo25).LegalityInfo = new List<Legality> { Legality.XWA };
        }
    }
}

namespace Abilities.SecondEdition
{
    //At the start of the Engagment Phase you may transfer 1 of your calculate tokens to another friendly ship at range 0-3.
    public class Dfs311SoCAbility : GenericAbility
    {
        public override void ActivateAbility()
        {
            Phases.Events.OnCombatPhaseStart_Triggers += RegisterAbility;
        }

        public override void DeactivateAbility()
        {
            Phases.Events.OnCombatPhaseStart_Triggers -= RegisterAbility;
        }

        private void RegisterAbility()
        {
            if (HostShip.Tokens.HasToken<Tokens.CalculateToken>())
            {
                RegisterAbilityTrigger(TriggerTypes.OnCombatPhaseStart, Ability);
            }
        }

        private void Ability(object sender, EventArgs e)
        {
            if (HostShip.Tokens.HasToken<Tokens.CalculateToken>() && TargetsForAbilityExist(FilterAbilityTarget))
            {
                Messages.ShowInfoToHuman(HostName + ": Select a ship to transfer 1 calculate token to");

                SelectTargetForAbility
                (
                    SelectAbilityTarget,
                    FilterAbilityTarget,
                    GetAiAbilityPriority,
                    HostShip.Owner.PlayerNo,
                    HostShip.PilotInfo.PilotName,
                    "Select a ship to transfer 1 calculate token to"
                );
            }
            else
            {
                Triggers.FinishTrigger();
            }
        }

        private void SelectAbilityTarget()
        {
            HostShip.Tokens.TransferToken(typeof(Tokens.CalculateToken), TargetShip, SelectShipSubPhase.FinishSelection);
        }

        private int GetAiAbilityPriority(GenericShip ship)
        {
            //TODO: when should the AI use this ability?   
            return 0;
        }

        protected virtual bool FilterAbilityTarget(GenericShip ship)
        {
            return FilterByTargetType(ship, new List<TargetTypes>() { TargetTypes.OtherFriendly }) && FilterTargetsByRange(ship, 0, 3);
        }
    }
}