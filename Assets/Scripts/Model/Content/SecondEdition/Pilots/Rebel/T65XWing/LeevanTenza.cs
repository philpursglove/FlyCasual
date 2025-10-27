using Actions;
using ActionsList;
using Content;
using System.Collections.Generic;
using Upgrade;

namespace Ship.SecondEdition.T65XWing
{
    public class LeevanTenza : T65XWing
    {
        public LeevanTenza() : base()
        {
            PilotInfo = new PilotCardInfo25
            (
                "Leevan Tenza",
                "Rebel Alliance Defector",
                Faction.Rebel,
                3,
                4,
                8,
                isLimited: true,
                abilityType: typeof(Abilities.SecondEdition.LeevanTenzaAbility),
                extraUpgradeIcons: new List<UpgradeType>
                {
                    UpgradeType.Talent,
                    UpgradeType.Astromech,
                    UpgradeType.Illicit,
                    UpgradeType.Missile,
                    UpgradeType.Configuration
                },
                tags: new List<Tags>
                {
                    Tags.Partisan,
                    Tags.XWing
                },
                seImageNumber: 8,
                skinName: "Partisan",
                legality: new List<Legality> { Legality.StandardLegal, Legality.ExtendedLegal }
            );
        }
    }

    public class LeevanTenzaXWA : LeevanTenza
    {
        public LeevanTenzaXWA() : base()
        {
            (PilotInfo as PilotCardInfo25).Cost = 10;
            (PilotInfo as PilotCardInfo25).LoadoutValue = 10;
            (PilotInfo as PilotCardInfo25).ExtraUpgrades = new List<UpgradeType>()
            {
                UpgradeType.Talent,
                UpgradeType.Astromech,
                UpgradeType.Illicit,
                UpgradeType.Modification,
                UpgradeType.Missile,
                UpgradeType.Configuration
            };
            (PilotInfo as PilotCardInfo25).LegalityInfo = new List<Legality> { Legality.XWA };
        }
    }
}

namespace Abilities.SecondEdition
{
    public class LeevanTenzaAbility : GenericAbility
    {
        public override void ActivateAbility()
        {
            HostShip.OnActionIsPerformed += CheckLeevanTenzaAbility;
        }

        public override void DeactivateAbility()
        {
            HostShip.OnActionIsPerformed -= CheckLeevanTenzaAbility;
        }

        private void CheckLeevanTenzaAbility(GenericAction action)
        {
            if (action is BoostAction || action is BarrelRollAction)
            {
                RegisterAbilityTrigger(TriggerTypes.OnActionIsPerformed, AskToUseLeevanTenzaAbility);
            }
        }

        private void AskToUseLeevanTenzaAbility(object sender, System.EventArgs e)
        {
            HostShip.AskPerformFreeAction(
                new EvadeAction() { Color = ActionColor.Red },
                Triggers.FinishTrigger,
                HostShip.PilotInfo.PilotName,
                "After you perform a Barrel Roll or Boost action, you may perform a red Evade action.",
                HostShip
            );
        }
    }
}