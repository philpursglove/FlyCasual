using Ship;
using System;

namespace Remote
{
    public class RemoteInfo : PilotCardInfo
    {
        public string Name { get; }

        public int Agility { get; }
        public int Hull { get; }

        public string ImageUrl { get; }
        public ShipArcsInfo ArcInfo { get; set; }
        public ShipSoundInfo SoundInfo { get; set; }

        public RemoteInfo(string name, int initiative, int agility, int hull, string imageUrl, Type abilityType = null) : base (name, initiative, 0)
        {
            Name = name;

            Initiative = initiative;
            Agility = agility;
            Hull = hull;

            ImageUrl = imageUrl;

            AbilityType = abilityType;
        }

        public RemoteInfo(string name, int initiative, ShipArcsInfo arcInfo, int agility, int hull, string imageUrl, Type abilityType = null, 
            int charges = 0, int regensCharges = 0, ShipSoundInfo soundInfo = null) : base(name, initiative, 0, charges: charges, regensCharges: regensCharges)
        {
            Name = name;
            Initiative = initiative;
            ArcInfo = arcInfo;
            Agility = agility;
            Hull = hull;

            ImageUrl = imageUrl;

            AbilityType = abilityType;

            SoundInfo = soundInfo;
        }
    }
}
