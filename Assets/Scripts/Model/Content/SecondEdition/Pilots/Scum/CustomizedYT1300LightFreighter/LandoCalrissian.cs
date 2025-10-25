using Content;
using System;
using System.Collections.Generic;
using Upgrade;

namespace Ship
{
    namespace SecondEdition.CustomizedYT1300LightFreighter
    {
        public class LandoCalrissian : CustomizedYT1300LightFreighter
        {
            public LandoCalrissian() : base()
            {
                PilotInfo = new PilotCardInfo25
                (
                    "Lando Calrissian",
                    "Smooth-talking Gambler",
                    Faction.Scum,
                    4,
                    5,
                    10,
                    isLimited: true,
                    abilityType: typeof(Abilities.SecondEdition.LandoCalrissianScumPilotAbility),
                    extraUpgradeIcons: new List<UpgradeType>
                    {
                        UpgradeType.Talent,
                        UpgradeType.Crew,
                        UpgradeType.Gunner,
                        UpgradeType.Illicit,
                        UpgradeType.Illicit,
                        UpgradeType.Modification,
                        UpgradeType.Missile,
                        UpgradeType.Title
                    },
                    tags: new List<Tags>
                    {
                        Tags.Freighter,
                        Tags.YT1300
                    },
                    seImageNumber: 223,
                    legality: new List<Legality> { Legality.StandardLegal, Legality.ExtendedLegal }
                );
            }
        }

        public class LandoCalrissianXWA : LandoCalrissian
        {
            public LandoCalrissianXWA() : base()
            {
                (PilotInfo as PilotCardInfo25).Cost = 13;
                (PilotInfo as PilotCardInfo25).LoadoutValue = 16;
                (PilotInfo as PilotCardInfo25).LegalityInfo = new List<Legality> { Legality.XWA };
                (PilotInfo as PilotCardInfo25).ExtraUpgrades = new List<UpgradeType>
                {
                    UpgradeType.Talent,
                    UpgradeType.Crew,
                    UpgradeType.Crew,
                    UpgradeType.Gunner,
                    UpgradeType.Illicit,
                    UpgradeType.Modification,
                    UpgradeType.Modification,
                    UpgradeType.Title
                };
            }
        }
    }
}

namespace Abilities.SecondEdition
{
    //After you roll dice, if you are not stressed, you may gain 1 stress token to reroll all of your blank results.
    public class LandoCalrissianScumPilotAbility : GenericAbility
    {
        public override void ActivateAbility()
        {
            AddDiceModification(
                "Lando Calrissian",
                IsDiceModificationAvailable,
                GetAiPriority,
                DiceModificationType.Reroll,
                int.MaxValue,
                new List<DieSide>() { DieSide.Blank },
                timing: DiceModificationTimingType.AfterRolled,
                payAbilityCost: PayAbilityCost
            );
        }

        private bool IsDiceModificationAvailable()
        {
            return !HostShip.IsStressed;
        }

        private int GetAiPriority()
        {
            return 95;
        }

        public override void DeactivateAbility()
        {
            RemoveDiceModification();
        }

        private void PayAbilityCost(Action<bool> callback)
        {
            HostShip.Tokens.AssignToken(typeof(Tokens.StressToken), () => callback(true));
        }
    }
}

