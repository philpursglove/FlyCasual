using Abilities.SecondEdition;
using Content;
using Ship;
using SubPhases;
using System;
using System.Collections.Generic;
using System.Linq;
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

            ShipInfo.Shields = 0;
            ShipInfo.Hull = 1;

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
                Messages.ShowInfo($"{HostShip.PilotName} adds an extra defense die");
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

        private void RegisterAbility(GenericShip ship, bool flag)
        {

            RegisterAbilityTrigger(TriggerTypes.OnShipIsDestroyed, UseAbility);
        }

        private void UseAbility(object sender, EventArgs e)
        {
            // When you are destroyed, before you are removed, you may spend 1 charge on an equipped device upgrade to drop or launch a bomb using a speed 1 straight or bank template
            List<GenericUpgrade> equippedBombs = HostShip.UpgradeBar.GetInstalledUpgrades(UpgradeType.Device).Where(b => b.State.Charges > 0 && typeof(GenericBomb).IsAssignableFrom(b.GetType())).ToList();

            if(equippedBombs.Count > 0)
            {
                AskToUseAbility
                (
                    descriptionShort: HostUpgrade.UpgradeInfo.Name,
                    useByDefault: AlwaysUseByDefault,
                    useAbility: DropOrLaunchBomb,
                    callback: Triggers.FinishTrigger,
                    descriptionLong: "You may drop or launch a bomb using a speed 1 straight or bank template.",
                    showSkipButton: false
                );
            }
            else
            {
                Triggers.FinishTrigger();
            }
        }

        private void DropOrLaunchBomb(object sender, EventArgs e)
        {
            
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