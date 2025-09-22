using Content;
using System.Collections.Generic;
using Upgrade;
using UpgradesList.SecondEdition;

namespace Ship.SecondEdition.TIEPhPhantom
{
    public class EchoSL : TIEPhPhantom
    {
        public EchoSL() : base()
        {
            PilotInfo = new PilotCardInfo25
            (
                "\"Echo\"",
                "Copycat",
                Faction.Imperial,
                4,
                5,
                0,
                isLimited: true,
                isStandardLayout: true,
                abilityType: typeof(Abilities.SecondEdition.EchoSLAbility),
                tags: new List<Tags>
                {
                    Tags.Tie
                },
                extraUpgradeIcons: new List<UpgradeType>()
                {
                    UpgradeType.Talent,
                    UpgradeType.Talent,
                    UpgradeType.Modification
                }
            );
            ImageUrl = "https://infinitearenas.com/xw2/images/quickbuilds/echo-tiephphantom.png";

            MustHaveUpgrades.Add(typeof(SilentHunter));
            MustHaveUpgrades.Add(typeof(StealthGambit));
            MustHaveUpgrades.Add(typeof(ManualAilerons));
        }
    }
}

namespace Abilities.SecondEdition
{
    public class EchoSLAbility : GenericAbility
    {
        public override void ActivateAbility()
        {
            // Ability implementation goes here
        }
        public override void DeactivateAbility()
        {
            // Ability deactivation logic goes here
        }
    }
}
