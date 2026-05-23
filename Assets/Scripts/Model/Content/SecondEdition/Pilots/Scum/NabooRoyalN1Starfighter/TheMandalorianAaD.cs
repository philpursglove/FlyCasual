using Abilities.SecondEdition;
using Actions;
using ActionsList;
using BoardTools;
using Content;
using Ship;
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
                    UpgradeType.Configuration,
                    UpgradeType.Modification
                },
                tags: new List<Tags>
                {
                    Tags.Mandalorian,
                    Tags.BountyHunter
                },
                charges: 0,
                regensCharges: 0,
                isStandardLayout: true,
                legality: new List<Legality> { Legality.XWA },
                abilityText: "While you defend or perform an attack, if you are in the  at range 1-2 of 2 or more enemy ships, you may change 1 of your blank results to a  result.",
                skinName: "Silver"

                );

            MustHaveUpgrades.Add(typeof(Outmaneuver));
            MustHaveUpgrades.Add(typeof(TheChild));
            MustHaveUpgrades.Add(typeof(CalibratedLaserTargeting));
            MustHaveUpgrades.Add(typeof(KinesoSwitch));

            PilotNameCanonical = "themandalorian-armedanddangerous";

            FullThrottleAbility fullThrottleAbility = ShipAbilities.First(a => a is FullThrottleAbility) as FullThrottleAbility;
            ShipAbilities.Remove(fullThrottleAbility);
            ShipAbilities.Add(new RestoredSpeedsterAbility());

            ShipInfo.ActionIcons.AddActions(new ActionInfo(typeof(SlamAction)));
            ShipInfo.ActionIcons.AddLinkedAction(new LinkedActionInfo(typeof(SlamAction), typeof(TargetLockAction), ActionColor.Red));
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
            ShotInfo shotInformation = new(HostShip, Selection.AnotherShip, HostShip.PrimaryWeapons.First());

            if (HostShip.Tokens.GetTokens<WeaponsDisabledToken>().Count == 1
                && Combat.ChosenWeapon is PrimaryWeaponClass
                && shotInformation.InArcByType(Arcs.ArcType.Bullseye))
            {
                Messages.ShowInfo($"{HostUpgrade.UpgradeInfo.Name}: Primary weapon attacks in the bullseye arc are allowed while disarmed.");
                allowed = false;
            }
        }
    }
}
