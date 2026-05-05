using Abilities.SecondEdition;
using Content;
using System.Collections.Generic;
using System.Linq;
using Tokens;
using Upgrade;
using UpgradesList.SecondEdition;

namespace Ship.SecondEdition.NabooRoyalN1Starfighter
{
    public class TheMandalorianAaD : NabooRoyalN1Starfighter
    {
        public TheMandalorianAaD() : base()
        {
            PilotInfo = new PilotCardInfo25
            (
                pilotName: "The Mandalorian",
                pilotTitle: "Armed and Dangerous",
                faction: Faction.Scum,
                initiative: 5,
                cost: 10,
                loadoutValue: 0,
                isLimited: true,
                abilityType: typeof(Abilities.SecondEdition.TheMandalorianAbility),
                force: 0,
                extraUpgradeIcons: new List<UpgradeType>
                {
                    UpgradeType.Talent,
                    UpgradeType.Crew,
                    UpgradeType.Illicit,
                    UpgradeType.Configuration
                },
                tags: new List<Tags>
                {

                },
                charges: 0,
                regensCharges: 0,
                isStandardLayout: true,
                legality: new List<Legality> { Legality.XWA },
                abilityText: "While you defend or perform an attack, if you are in the  at range 1-2 of 2 or more enemy ships, you may change 1 of your blank results to a  result."

                );

            MustHaveUpgrades.Add(typeof(Outmaneuver));
            MustHaveUpgrades.Add(typeof(TheChild));
            MustHaveUpgrades.Add(typeof(CalibratedLaserTargeting));

            PilotNameCanonical = "";

            FullThrottleAbility fullThrottleAbility = ShipAbilities.First(a => a is FullThrottleAbility) as FullThrottleAbility;
            ShipAbilities.Remove(fullThrottleAbility);
            ShipAbilities.Add(new RestoredSpeedsterAbility());
        }
    }
}

namespace Abilities.SecondEdition
{
    public class RestoredSpeedsterAbility : GenericAbility
    {
        public override void ActivateAbility()
        {
            HostShip.OnWeaponsDisabledCheck += AllowBullseyeAttacksWhileDisarmed;
        }

        public override void DeactivateAbility()
        {
            HostShip.OnWeaponsDisabledCheck -= AllowBullseyeAttacksWhileDisarmed;
        }

        private void AllowBullseyeAttacksWhileDisarmed(ref bool allowed)
        {
            allowed = HostShip.Tokens.CountTokensByType(typeof(WeaponsDisabledToken)) < 2 &&
                   HostShip.SectorsInfo.IsShipInSector(Combat.Defender, Arcs.ArcType.Bullseye);

        }


    }
}
