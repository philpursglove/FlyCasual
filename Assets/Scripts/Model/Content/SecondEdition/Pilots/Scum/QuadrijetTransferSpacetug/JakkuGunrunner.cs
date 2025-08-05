using Content;
using System.Collections.Generic;
using Upgrade;

namespace Ship
{
    namespace SecondEdition.QuadrijetTransferSpacetug
    {
        public class JakkuGunrunner : QuadrijetTransferSpacetug
        {
            public JakkuGunrunner() : base()
            {
                PilotInfo = new PilotCardInfo25
                (
                    "Jakku Gunrunner",
                    "",
                    Faction.Scum,
                    1,
                    4,
                    4,
                    extraUpgradeIcons: new List<UpgradeType>()
                    {
                        UpgradeType.Illicit,
                        UpgradeType.Device                        
                    },
                    seImageNumber: 164,
                    legality: new List<Legality>() { Legality.ExtendedLegal }
                );
            }
        }

        public class JakkuGunrunnerXWA : JakkuGunrunner
        {
            public JakkuGunrunnerXWA() : base()
            {
                (PilotInfo as PilotCardInfo25).Cost = 3;
                (PilotInfo as PilotCardInfo25).LoadoutValue = 7;
                (PilotInfo as PilotCardInfo25).LegalityInfo = new List<Legality> { Legality.XWA };
            }
        }
    }
}
