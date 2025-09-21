using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ship.SecondEdition.TIEPhPhantom
{
    public class WhisperSL : TIEPhPhantom
    {
        public WhisperSL() : base()
        {
            PilotInfo = new PilotCardInfo25
            (
                "Whisper (SL)",
                "Unseen Assailant",
                Faction.Imperial,
                5,
                5,
                9,
                isLimited: true,
                abilityType: typeof(Abilities.SecondEdition.WhisperSLAbility),
                tags: new List<Tags>
                {
                    Tags.Tie
                },
                extraUpgradeIcons: new List<UpgradeType>()
                {
                    UpgradeType.Talent,
                    UpgradeType.Talent,
                    UpgradeType.Sensor,
                    UpgradeType.Gunner,
                    UpgradeType.Modification
                },
                seImageNumber: 133,
                legality: new List<Legality> { Legality.StandardLegal, Legality.ExtendedLegal }
            );
            ModelInfo.SkinName = "Whisper SL";
        }
    }
    {
    }
}
