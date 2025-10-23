using ActionsList;
using Content;
using Ship;
using System.Collections.Generic;
using Upgrade;

namespace Ship.SecondEdition.TIELnFighter
{
    public class SabineWren : TIELnFighter
    {
        public SabineWren() : base()
        {
            PilotInfo = new PilotCardInfo25
            (
                "Sabine Wren",
                "Spectre-5",
                Faction.Rebel,
                3,
                2,
                0,
                isLimited: true,
                abilityType: typeof(Abilities.SecondEdition.SabineWrenPilotAbility),
                tags: new List<Tags>
                {
                    Tags.Mandalorian,
                    Tags.Tie,
                    Tags.Spectre
                },
                seImageNumber: 47,
                legality: new List<Legality> { Legality.StandardLegal, Legality.ExtendedLegal }
            );

            PilotNameCanonical = "sabinewren-tielnfighter";

            ModelInfo.ModelName = "TIE Fighter Rebel";
            ModelInfo.SkinName = "Rebel";
        }
    }

    public class SabineWrenXWA : SabineWren
    {
        public SabineWrenXWA() : base()
        {
            (PilotInfo as PilotCardInfo25).Cost = 8;
            (PilotInfo as PilotCardInfo25).LoadoutValue = 13;
            (PilotInfo as PilotCardInfo25).ExtraUpgrades = new List<UpgradeType>
            {
                UpgradeType.Talent,
                UpgradeType.Modification,
                UpgradeType.Modification
            };
            (PilotInfo as PilotCardInfo25).LegalityInfo = new List<Legality> { Legality.XWA };
        }
    }
}

namespace Abilities.SecondEdition
{
    public class SabineWrenPilotAbility : GenericAbility
    {
        public override void ActivateAbility()
        {
            HostShip.OnManeuverIsRevealed += RegisterSabineWrenPilotAbility;
        }

        public override void DeactivateAbility()
        {
            HostShip.OnManeuverIsRevealed -= RegisterSabineWrenPilotAbility;
        }

        private void RegisterSabineWrenPilotAbility(GenericShip ship)
        {
            RegisterAbilityTrigger(TriggerTypes.OnManeuverIsRevealed, PerformFreeReposition);
        }

        private void PerformFreeReposition(object sender, System.EventArgs e)
        {
            List<GenericAction> actions = new List<GenericAction>() { new BoostAction(), new BarrelRollAction() };

            HostShip.AskPerformFreeAction(
                actions,
                Triggers.FinishTrigger,
                descriptionShort: HostShip.PilotInfo.PilotName,
                descriptionLong: "Before you activate, you may perform a Barrel Roll or Boost action",
                imageHolder: HostShip
            );
        }
    }
}
