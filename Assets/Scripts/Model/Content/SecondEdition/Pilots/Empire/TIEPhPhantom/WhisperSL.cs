using Abilities.SecondEdition;
using Content;
using System.Collections.Generic;
using Upgrade;
using UpgradesList.SecondEdition;

namespace Ship.SecondEdition.TIEPhPhantom
{
    public class WhisperSL : TIEPhPhantom
    {
        public WhisperSL() : base()
        {
            PilotInfo = new PilotCardInfo25
            (
                "Whisper",
                "Unseen Assailant",
                Faction.Imperial,
                5,
                6,
                0,
                isLimited: true,
                isStandardLayout: true,
                abilityType: typeof(WhisperSLAbility),
                tags: new List<Tags>
                {
                    Tags.Tie
                },
                extraUpgradeIcons: new List<UpgradeType>()
                {
                    UpgradeType.Talent,
                    UpgradeType.Sensor,
                    UpgradeType.Modification
                },
                legality: new List<Legality> { Legality.StandardLegal, Legality.ExtendedLegal },
                charges: 2
            );
            ImageUrl = "https://infinitearenas.com/xw2/images/quickbuilds/whisper-tiephphantom.png";

            MustHaveUpgrades.Add(typeof(WithoutATrace));
            MustHaveUpgrades.Add(typeof(RelaySystem));
            MustHaveUpgrades.Add(typeof(StygiumReserve));
        }
    }
}

namespace Abilities.SecondEdition
{
    public class WhisperSLAbility : GenericAbility
    {
        public override void ActivateAbility()
        {
            throw new System.NotImplementedException();
        }

        public override void DeactivateAbility()
        {
            throw new System.NotImplementedException();
        }
    }
}