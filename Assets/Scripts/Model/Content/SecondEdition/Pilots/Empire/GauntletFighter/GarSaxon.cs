using Content;
using Ship;
using System.Collections.Generic;
using Upgrade;

namespace Ship
{
    namespace SecondEdition.GauntletFighter
    {
        public class GarSaxon : GauntletFighter
        {
            public GarSaxon() : base()
            {
                PilotInfo = new PilotCardInfo25
                (
                    "Gar Saxon",
                    "Treacherous Viceroy",
                    Faction.Imperial,
                    3,
                    7,
                    18,
                    isLimited: true,
                    charges: 2,
                    regensCharges: 1,
                    abilityType: typeof(Abilities.SecondEdition.GarSaxonAbility),
                    extraUpgradeIcons: new List<UpgradeType>()
                    {
                        UpgradeType.Talent,
                        UpgradeType.Crew,
                        UpgradeType.Gunner,
                        UpgradeType.Illicit,
                        UpgradeType.Modification,
                        UpgradeType.Device,
                        UpgradeType.Configuration
                    },
                    tags: new List<Tags>()
                    {
                        Tags.Mandalorian 
                    },
                    skinName: "CIS Dark",
                    legality: new List<Legality> { Legality.StandardLegal, Legality.ExtendedLegal }
                );
            }
        }

        public class GarSaxonXWA : GarSaxon
        {
            public GarSaxonXWA() : base()
            {
                (PilotInfo as PilotCardInfo25).Cost = 7;
                (PilotInfo as PilotCardInfo25).LoadoutValue = 24;
                (PilotInfo as PilotCardInfo25).LegalityInfo = new List<Legality> { Legality.XWA };
            }
        }
    }
}

namespace Abilities.SecondEdition
{
    public class GarSaxonAbility : GenericAbility
    {
        public override void ActivateAbility()
        {
            GenericShip.OnAttackStartAsAttackerGlobal += RegisterGarSaxonAbility;
        }

        public override void DeactivateAbility()
        {
            GenericShip.OnAttackStartAsAttackerGlobal -= RegisterGarSaxonAbility;
        }

        protected void RegisterGarSaxonAbility()
        {
            if (Tools.IsFriendly(Combat.Attacker, HostShip)
                && !Tools.IsSameShip(Combat.Attacker, HostShip)
                && Combat.ChosenWeapon.WeaponType == WeaponTypes.PrimaryWeapon
                && Combat.Defender.SectorsInfo.IsShipInSector(Combat.Attacker, Arcs.ArcType.Rear))
            {
                BoardTools.DistanceInfo distanceInfo = new BoardTools.DistanceInfo(Combat.Attacker, HostShip);
                if (distanceInfo.Range < 3)
                {
                    RegisterAbilityTrigger(TriggerTypes.OnAttackStart, AskGarSaxonAbility);
                }
            }
        }
        protected void AskGarSaxonAbility(object sender, System.EventArgs e)
        {
            if (HostShip.State.Charges > 0)
            {
                AskToUseAbility(
                    HostShip.PilotInfo.PilotName,
                    AlwaysUseByDefault,
                    UseGarSaxonAbility,
                    descriptionLong: "Do you want to spend 1 charge to allow attacker to roll 1 additional attack die?",
                    imageHolder: HostShip
                );
            }
            else
            {
                Triggers.FinishTrigger();
            }
        }

        private void UseGarSaxonAbility(object sender, System.EventArgs e)
        {
            HostShip.SpendCharge();
            AllowRollAdditionalDice();
        }

        private void AllowRollAdditionalDice()
        {
            Combat.Attacker.AfterGotNumberOfAttackDice += IncreaseByOne;
            SubPhases.DecisionSubPhase.ConfirmDecision();
        }

        private void IncreaseByOne(ref int value)
        {
            value++;
            Combat.Attacker.AfterGotNumberOfAttackDice -= IncreaseByOne;
        }
    }
}