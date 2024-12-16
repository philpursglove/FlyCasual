using Abilities.SecondEdition;
using BoardTools;
using Bombs;
using Content;
using Movement;
using Ship;
using System;
using System.Collections.Generic;
using Upgrade;

namespace Ship.SecondEdition.ASF01BWing
{
    public class AdonFoxBattleOverEndor : ASF01BWing
    {
        public AdonFoxBattleOverEndor() : base()
        {
            PilotInfo = new PilotCardInfo25
            (
                pilotName: "Adon Fox",
                pilotTitle: "Battle Over Endor",
                faction: Faction.Rebel,
                initiative: 1,
                cost: 5,
                loadoutValue: 0,
                isLimited: true,
                abilityType: typeof(AdonFoxBattleOverEndorAbility),
                extraUpgradeIcons: new List<UpgradeType>
                {
                    UpgradeType.Talent,
                    UpgradeType.Talent,
                    UpgradeType.Missile,
                    UpgradeType.Device
                },
                tags: new List<Tags>
                {
                    Tags.BWing
                },
                charges: 2,
                regensCharges: 1,
                isStandardLayout: true
            );

            ImageUrl = "https://infinitearenas.com/xw2/images/quickbuilds/adonfox-battleoverendor.png";

            MustHaveUpgrades.Add(typeof(UpgradesList.SecondEdition.ItsATrap));
            MustHaveUpgrades.Add(typeof(UpgradesList.SecondEdition.PartingGift));
            MustHaveUpgrades.Add(typeof(UpgradesList.SecondEdition.ProtonRockets));
            MustHaveUpgrades.Add(typeof(UpgradesList.SecondEdition.ProtonBombs));

            ShipAbilities.Add(new GyroCockpit());

            PilotNameCanonical = "adonfox-battleoverendor";

            DefaultUpgrades.Remove(typeof(UpgradesList.SecondEdition.StabilizedSFoilsOpen));
        }
    }
}

namespace Abilities.SecondEdition
{
    public class AdonFoxBattleOverEndorAbility : GenericAbility
    {
        public override void ActivateAbility()
        {
            HostShip.OnDefenceStartAsDefender += CheckAbility;
        }

        public override void DeactivateAbility()
        {
            HostShip.OnDefenceStartAsDefender -= CheckAbility;
        }
        private void CheckAbility()
        {
            if(HostShip.IsStressed)
            {
                RegisterAbilityTrigger(TriggerTypes.OnDefenseStart, UseAbility);
            }
        }

        private void UseAbility(object sender, EventArgs e)
        {
            if(Combat.Defender == HostShip)
            {
                Messages.ShowInfo($"{HostShip.PilotInfo.PilotName} adds an extra defense die");
                HostShip.AfterGotNumberOfDefenceDice += AddDefenseDie;
                Triggers.FinishTrigger();
            }
            else
            {
                Triggers.FinishTrigger();
            }
        }

        private void AddDefenseDie(ref int dieCount)
        {
            HostShip.AfterGotNumberOfDefenceDice -= AddDefenseDie;
            dieCount++;
        }
    }

    public class PartingGiftAbility : GenericAbility
    {
        public override void ActivateAbility()
        {
            HostShip.OnShipIsDestroyed += RegisterAbility;
        }

        public override void DeactivateAbility()
        {
            HostShip.OnShipIsDestroyed -= RegisterAbility;
        }

        private void RegisterAbility(GenericShip ship, bool isFled)
        {
            if (!isFled)
            {
                RegisterAbilityTrigger(TriggerTypes.OnShipIsDestroyed, UseAbility);
            }        
        }

        private void UseAbility(object sender, EventArgs e)
        {
            HostShip.OnGetAvailableBombDropTemplatesTwoConditions += GetBombTemplates;
            BombsManager.RegisterBombDropTriggerIfAvailable(HostShip, TriggerTypes.OnAbilityDirect);
            Triggers.ResolveTriggers(TriggerTypes.OnAbilityDirect, Triggers.FinishTrigger);
        }

        private void GetBombTemplates(List<ManeuverTemplate> availableTemplates, GenericUpgrade upgrade)
        {
            HostShip.OnGetAvailableBombDropTemplatesTwoConditions -= GetBombTemplates;
            availableTemplates.Add(new ManeuverTemplate(ManeuverBearing.Bank, ManeuverDirection.Left, ManeuverSpeed.Speed1, isBombTemplate: true));
            availableTemplates.Add(new ManeuverTemplate(ManeuverBearing.Bank, ManeuverDirection.Right, ManeuverSpeed.Speed1, isBombTemplate: true));
        }
    }
}

namespace UpgradesList.SecondEdition
{
    public class PartingGift : GenericUpgrade
    {
        public PartingGift() : base()
        {
            UpgradeInfo = new UpgradeCardInfo(
                "Parting Gift",
                UpgradeType.Talent,
                cost: 0,
                abilityType: typeof(PartingGiftAbility)
            );

            IsHidden = true;

            ImageUrl = HostShip != null? HostShip.ImageUrl : "https://infinitearenas.com/xw2/images/quickbuilds/adonfox-battleoverendor.png";
        }
    }
}