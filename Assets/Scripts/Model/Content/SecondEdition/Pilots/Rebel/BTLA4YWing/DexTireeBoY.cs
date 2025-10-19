using Abilities.SecondEdition;
using Content;
using System.Collections.Generic;
using UnityEngine;
using Upgrade;

namespace Ship.SecondEdition.BTLA4YWing
{
    public class DexTireeBoY : BTLA4YWing
    {
        public DexTireeBoY() : base()
        {
            PilotInfo = new PilotCardInfo25
            (
                "Dex Tiree",
                "Battle of Yavin",
                Faction.Rebel,
                2,
                4,
                0,
                isLimited: true,
                abilityType: typeof(DexTireeBoYAbility),
                extraUpgradeIcons: new List<UpgradeType>
                {
                    UpgradeType.Turret,
                    UpgradeType.Torpedo,
                    UpgradeType.Astromech
                },
                tags: new List<Tags>
                {
                    Tags.YWing
                },
                isStandardLayout: true
            );

            ShipAbilities.Add(new HopeAbility());

            MustHaveUpgrades.Add(typeof(UpgradesList.SecondEdition.DorsalTurret));
            MustHaveUpgrades.Add(typeof(UpgradesList.SecondEdition.AdvProtonTorpedoes));
            MustHaveUpgrades.Add(typeof(UpgradesList.SecondEdition.R4Astromech));

            PilotNameCanonical = "dextiree-battleofyavin";
        }
    }

    public class DexTireeBoYXWA : DexTireeBoY
    {
        public DexTireeBoYXWA() : base()
        {
            var pilot = (PilotCardInfo25)PilotInfo;
            pilot.Cost = 4;
            pilot.LegalityInfo = new List<Legality> { Legality.XWA };
        }
    }
}

namespace Abilities.SecondEdition
{
    public class DexTireeBoYAbility : GenericAbility
    {
        public override void ActivateAbility()
        {
            HostShip.AfterGotNumberOfDefenceDice += TryAddDice;
        }

        public override void DeactivateAbility()
        {
            HostShip.AfterGotNumberOfDefenceDice -= TryAddDice;
        }

        private void TryAddDice(ref int count)
        {
            int anotherFriendlyShipsAtRange = BoardTools.Board.GetShipsAtRange(Combat.Defender, new Vector2(0, 1), Team.Type.Friendly).Count - 1;

            if (Combat.AttackStep == CombatStep.Defence && Combat.Defender == HostShip && anotherFriendlyShipsAtRange > 0)
            {
                Messages.ShowInfo(HostShip.PilotInfo.PilotName + " rolls 1 additional die");
                count++;
            }
        }
    }
}