using Content;
using Ship;
using System.Collections.Generic;
using Upgrade;
using UpgradesList.SecondEdition;

namespace Ship
{
    namespace SecondEdition.TIELnFighter
    {
        public class MaulerMithelBoY : TIELnFighter
        {
            public MaulerMithelBoY() : base()
            {
                PilotInfo = new PilotCardInfo25
                (
                    "\"Mauler\" Mithel",
                    "Battle of Yavin",
                    Faction.Imperial,
                    5,
                    3,
                    0,
                    isLimited: true,
                    abilityType: typeof(Abilities.SecondEdition.MaulerMithelBoYAbility),
                    extraUpgradeIcons: new List<UpgradeType>
                    {
                        UpgradeType.Talent,
                        UpgradeType.Modification
                    },
                    tags: new List<Tags>
                    {
                        Tags.Tie
                    },
                    isStandardLayout: true,
                    legality: new List<Legality> { Legality.StandardLegal, Legality.ExtendedLegal }
                );

                MustHaveUpgrades.Add(typeof(Predator));
                MustHaveUpgrades.Add(typeof(AfterBurners));

                ShipInfo.Hull++;

                PilotNameCanonical = "maulermithel-battleofyavin";
            }
        }

        public class MaulerMithelBoYXWA : MaulerMithelBoY
        {
            public MaulerMithelBoYXWA() : base()
            {
                (PilotInfo as PilotCardInfo25).Cost = 10;
                (PilotInfo as PilotCardInfo25).LegalityInfo = new List<Legality> { Legality.XWA };
            }
        }
    }
}

namespace Abilities.SecondEdition
{
    public class MaulerMithelBoYAbility : GenericAbility
    {
        public override void ActivateAbility()
        {
            HostShip.AfterGotNumberOfAttackDice += MaulerMithelBoYPilotAbility;
        }

        public override void DeactivateAbility()
        {
            HostShip.AfterGotNumberOfAttackDice -= MaulerMithelBoYPilotAbility;
        }

        private void MaulerMithelBoYPilotAbility(ref int diceCount)
        {
            if (Combat.ChosenWeapon.WeaponType == Ship.WeaponTypes.PrimaryWeapon)
            {
                List<GenericShip> shipsInFlanks = new List<GenericShip>();
                foreach (GenericShip friendlyShip in HostShip.Owner.Ships.Values)
                {
                    if (friendlyShip.PilotInfo.PilotName == "Darth Vader" || friendlyShip.PilotInfo.PilotName == "\"Backstabber\"")
                    {
                        if (HostShip.SectorsInfo.RangeToShipBySector(friendlyShip, Arcs.ArcType.Left) <= 1
                            || HostShip.SectorsInfo.RangeToShipBySector(friendlyShip, Arcs.ArcType.Right) <= 1)
                        {
                            Messages.ShowInfo($"{HostShip.PilotInfo.PilotName}: {friendlyShip.PilotInfo.PilotName} is in range, +1 attack die");
                            diceCount++;
                            return;
                        }
                    }
                }

            }
        }
    }
}