using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ship.SecondEdition.TIEPhPhantom
{
    public class EchoSL : TIEPhPhantom
    {
        public EchoSL() : base()
        {
            PilotInfo = new PilotCardInfo25
            (
                "\"Echo\" (SL)",
                "Copycat",
                Faction.Imperial,
                4,
                5,
                9,
                isLimited: true,
                abilityType: typeof(Abilities.SecondEdition.EchoSLAbility),
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
                seImageNumber: 134,
                legality: new List<Legality> { Legality.StandardLegal, Legality.ExtendedLegal }
            );
            ModelInfo.SkinName = "Echo SL";
        }
    }
    {
    }
}
