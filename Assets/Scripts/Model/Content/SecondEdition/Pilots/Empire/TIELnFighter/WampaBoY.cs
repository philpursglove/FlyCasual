using Abilities.SecondEdition;
using Actions;
using ActionsList;
using Content;
using System.Collections.Generic;
using Upgrade;

namespace Ship
{
    namespace SecondEdition.TIELnFighter
    {
        public class WampaBoY : TIELnFighter
        {
            public WampaBoY() : base()
            {
                PilotInfo = new PilotCardInfo25
                (
                    "\"Wampa\"",
                    "Battle of Yavin",
                    Faction.Imperial,
                    1,
                    3,
                    0,
                    isLimited: true,
                    abilityType: typeof(WampaAbility),
                    charges: 1,
                    regensCharges: 1,
                    extraUpgradeIcons: new List<UpgradeType>
                    {
                        UpgradeType.Talent,
                        UpgradeType.Talent
                    },
                    tags: new List<Tags>
                    {
                        Tags.Tie
                    },
                    isStandardLayout: true,
                    legality: new List<Legality> { Legality.StandardLegal, Legality.ExtendedLegal }
                );

                ShipInfo.ActionIcons.AddActions(new ActionInfo(typeof(TargetLockAction)));

                MustHaveUpgrades.Add(typeof(UpgradesList.SecondEdition.Elusive));
                MustHaveUpgrades.Add(typeof(UpgradesList.SecondEdition.Vengeful));

                ShipInfo.Hull++;

                PilotNameCanonical = "wampa-battleofyavin";
            }
        }

        public class WampaBoYXWA : WampaBoY
        {
            public WampaBoYXWA() : base()
            {
                (PilotInfo as PilotCardInfo25).Cost = 7;
                (PilotInfo as PilotCardInfo25).LegalityInfo = new List<Legality> { Legality.XWA };
            }
        }
    }
}