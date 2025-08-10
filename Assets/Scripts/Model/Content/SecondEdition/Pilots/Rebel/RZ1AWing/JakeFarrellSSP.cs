using ActionsList;
using Content;
using Ship;
using SubPhases;
using System;
using System.Collections;
using System.Collections.Generic;
using Upgrade;
using UpgradesList.SecondEdition;

namespace Ship
{
    namespace SecondEdition.RZ1AWing
    {
        public class JakeFarrellSSP : RZ1AWing
        {
            public JakeFarrellSSP() : base()
            {
                PilotInfo = new PilotCardInfo25
                (
                    "Jake Farrell",
                    "Sage Instructor",
                    Faction.Rebel,
                    4,
                    5,
                    0,
                    isLimited: true,
                    abilityType: typeof(Abilities.SecondEdition.JakeFarrellAbility),
                    extraUpgradeIcons: new List<UpgradeType>
                    {
                        UpgradeType.Talent,
                        UpgradeType.Talent,
                        UpgradeType.Missile
                    },
                    tags: new List<Tags>
                    {
                        Tags.AWing
                    },
                    skinName: "Blue",
                    isStandardLayout: true,
                    legality: new List<Legality> { Legality.StandardLegal, Legality.ExtendedLegal }
                );

                MustHaveUpgrades.Add(typeof(Elusive));
                MustHaveUpgrades.Add(typeof(Outmaneuver));
                MustHaveUpgrades.Add(typeof(IonMissiles));

                PilotNameCanonical = "jakefarrell-swz106";
            }
        }

        public class JakeFarrellSSPXWA : JakeFarrellSSP
        {
            public JakeFarrellSSPXWA() : base()
            {
                (PilotInfo as PilotCardInfo25).Cost = 4;
                (PilotInfo as PilotCardInfo25).LegalityInfo = new List<Legality> { Legality.XWA };
            }
        }
    }
}