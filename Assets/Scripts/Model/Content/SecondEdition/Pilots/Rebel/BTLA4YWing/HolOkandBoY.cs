using Abilities.SecondEdition;
using BoardTools;
using Content;
using Ship;
using SubPhases;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Upgrade;

namespace Ship.SecondEdition.BTLA4YWing
{
    public class HolOkandBoY : BTLA4YWing
    {
        public HolOkandBoY() : base()
        {
            PilotInfo = new PilotCardInfo25
            (
                "Hol Okand",
                "Battle of Yavin",
                Faction.Rebel,
                4,
                3,
                0,
                isLimited: true,
                abilityType: typeof(HolOkandBoYAbility),
                extraUpgradeIcons: new List<UpgradeType>
                {
                    UpgradeType.Turret,
                    UpgradeType.Torpedo,
                    UpgradeType.Astromech
                },
                tags: new List<Tags>
                {
                    Tags.YWing
                },
                isStandardLayout: true,
                legality: new List<Legality> { Legality.StandardLegal, Legality.ExtendedLegal }
            );

            ShipAbilities.Add(new HopeAbility());

            MustHaveUpgrades.Add(typeof(UpgradesList.SecondEdition.DorsalTurret));
            MustHaveUpgrades.Add(typeof(UpgradesList.SecondEdition.AdvProtonTorpedoes));
            MustHaveUpgrades.Add(typeof(UpgradesList.SecondEdition.PreciseAstromech));

            PilotNameCanonical = "holokand-battleofyavin";
        }
    }

    public class HolOkandBoYXWA : HolOkandBoY
    {
        public HolOkandBoYXWA() : base()
        {
            (PilotInfo as PilotCardInfo25).Cost = 4;
            (PilotInfo as PilotCardInfo25).LegalityInfo = new List<Legality> { Legality.XWA };
        }
    }
}

namespace Abilities.SecondEdition
{
    public class HolOkandBoYAbility : GenericAbility
    {
        public override void ActivateAbility()
        {
            HostShip.OnCheckSystemsAbilityActivation += CheckAbility;
            HostShip.OnSystemsAbilityActivation += RegisterAbility;
        }

        public override void DeactivateAbility()
        {
            HostShip.OnCheckSystemsAbilityActivation -= CheckAbility;
            HostShip.OnSystemsAbilityActivation -= RegisterAbility;
        }

        private void CheckAbility(GenericShip ship, ref bool flag)
        {
            flag = HasEnemyShipsAtR12AndHasRechargableUpgrades();
        }

        private bool HasEnemyShipsAtR12AndHasRechargableUpgrades()
        {
            return (
                Board.GetShipsAtRange(HostShip, new Vector2(1, 2), Team.Type.Enemy).Count == 0
                && HasRechargableUpgrades()
            );
        }

        private bool HasRechargableUpgrades()
        {
            return HostShip.UpgradeBar.GetRechargableUpgrades().Count > 0;
        }

        private void RegisterAbility(GenericShip ship)
        {
            if (HasEnemyShipsAtR12AndHasRechargableUpgrades())
            {
                RegisterAbilityTrigger(TriggerTypes.OnSystemsAbilityActivation, AskToRechargeUpgrade);
            }
        }

        private void AskToRechargeUpgrade(object sender, EventArgs e)
        {
            if (HasEnemyShipsAtR12AndHasRechargableUpgrades())
            {
                HolOkandBoYReloadDecisionSubphase subphase = Phases.StartTemporarySubPhaseNew<HolOkandBoYReloadDecisionSubphase>(
                    "Recover 1 Charge on any upgrade",
                    Triggers.FinishTrigger
                );

                subphase.DescriptionShort = "Recover 1 Charge on any upgrade";
                subphase.RequiredPlayer = Selection.ThisShip.Owner.PlayerNo;
                subphase.DecisionViewType = DecisionViewTypes.ImagesUpgrade;

                foreach (GenericUpgrade upgrade in HostShip.UpgradeBar.GetRechargableUpgrades())
                {
                    subphase.AddDecision(
                        upgrade.UpgradeInfo.Name,
                        delegate { RechargeUpgradeAndFinish(upgrade); },
                        upgrade.ImageUrl,
                        upgrade.State.Charges
                    );
                }

                subphase.DefaultDecisionName = subphase.GetDecisions().First().Name;

                subphase.Start();
            }
            else
            {
                Triggers.FinishTrigger();
            }
        }

        private static void RechargeUpgradeAndFinish(GenericUpgrade upgrade)
        {
            RechargeUpgrade(upgrade);

            DecisionSubPhase.ConfirmDecision();
        }

        private static void RechargeUpgrade(GenericUpgrade upgrade)
        {
            int count = upgrade.HostShip.GetReloadChargesCount(upgrade);
            upgrade.State.RestoreCharges(count);

            string chargesText = (count == 1) ? "1 Charge" : $"{count} Charges";
            Messages.ShowInfo($"Reload: {chargesText} of {upgrade.UpgradeInfo.Name} is restored");
        }

        public class HolOkandBoYReloadDecisionSubphase : DecisionSubPhase { }
    }
}