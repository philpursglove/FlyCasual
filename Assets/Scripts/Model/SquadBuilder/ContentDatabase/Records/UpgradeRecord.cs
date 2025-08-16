using Content;
using Editions;
using Ship;
using System;
using System.Collections.Generic;
using System.Linq;
using Upgrade;

namespace SquadBuilderNS
{
    public class UpgradeRecord
    {
        public GenericUpgrade Instance { get; }
        public string UpgradeName => Instance.UpgradeInfo.Name;
        public string UpgradeNameCanonical => Instance.NameCanonical;
        public string UpgradeTypeName => Instance.GetType().ToString();
        public UpgradeType UpgradeType => Instance.UpgradeInfo.UpgradeTypes.First();
        public bool IsAllowedForSquadBuilder => Instance.IsAllowedForSquadBuilder();
        public List<Legality> AllowableFormats => Instance.UpgradeInfo?.LegalityInfo;

        public UpgradeRecord(Type type)
        {
            Instance = (GenericUpgrade)System.Activator.CreateInstance(type);
            Edition.Current.AdaptUpgradeToRules(Instance);
        }
    }
}
