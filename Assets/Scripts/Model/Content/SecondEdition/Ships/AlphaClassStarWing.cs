using Actions;
using ActionsList;
using Arcs;
using Movement;
using Ship;
using Ship.CardInfo;
using SubPhases;
using System.Collections.Generic;
using Tokens;
using UnityEngine;

namespace Ship
{
    namespace SecondEdition.AlphaClassStarWing
    {
        public class AlphaClassStarWing : GenericShip
        {
            public AlphaClassStarWing() : base()
            {
                ShipInfo = new ShipCardInfo25
                (
                    "Alpha-class Star Wing",
                    BaseSize.Small,
                    new FactionData
                    (
                        new Dictionary<Faction, System.Type>
                        {
                            { Faction.Imperial, typeof(MajorVynder) }
                        }
                    ),
                    new ShipArcsInfo(ArcType.Front, 2), 2, 4, 3,
                    new ShipActionsInfo(
                        new ActionInfo(typeof(FocusAction)),
                        new ActionInfo(typeof(TargetLockAction)),
                        new ActionInfo(typeof(SlamAction)),
                        new ActionInfo(typeof(ReloadAction))
                    ),
                    new ShipUpgradesInfo()
                );

                ModelInfo = new ShipModelInfo(
                    "Alpha-class Star Wing",
                    "Gray",
                    new Vector3(-4, 8, 5.5f),
                    1.8f
                );

                DialInfo = new ShipDialInfo(
                    new ManeuverInfo(ManeuverSpeed.Speed1, ManeuverDirection.Left, ManeuverBearing.Bank, MovementComplexity.Normal),
                    new ManeuverInfo(ManeuverSpeed.Speed1, ManeuverDirection.Forward, ManeuverBearing.Straight, MovementComplexity.Easy),
                    new ManeuverInfo(ManeuverSpeed.Speed1, ManeuverDirection.Right, ManeuverBearing.Bank, MovementComplexity.Normal),

                    new ManeuverInfo(ManeuverSpeed.Speed2, ManeuverDirection.Left, ManeuverBearing.Turn, MovementComplexity.Normal),
                    new ManeuverInfo(ManeuverSpeed.Speed2, ManeuverDirection.Left, ManeuverBearing.Bank, MovementComplexity.Easy),
                    new ManeuverInfo(ManeuverSpeed.Speed2, ManeuverDirection.Forward, ManeuverBearing.Straight, MovementComplexity.Easy),
                    new ManeuverInfo(ManeuverSpeed.Speed2, ManeuverDirection.Right, ManeuverBearing.Bank, MovementComplexity.Easy),
                    new ManeuverInfo(ManeuverSpeed.Speed2, ManeuverDirection.Right, ManeuverBearing.Turn, MovementComplexity.Normal),

                    new ManeuverInfo(ManeuverSpeed.Speed3, ManeuverDirection.Left, ManeuverBearing.Turn, MovementComplexity.Normal),
                    new ManeuverInfo(ManeuverSpeed.Speed3, ManeuverDirection.Left, ManeuverBearing.Bank, MovementComplexity.Normal),
                    new ManeuverInfo(ManeuverSpeed.Speed3, ManeuverDirection.Forward, ManeuverBearing.Straight, MovementComplexity.Normal),
                    new ManeuverInfo(ManeuverSpeed.Speed3, ManeuverDirection.Right, ManeuverBearing.Bank, MovementComplexity.Normal),
                    new ManeuverInfo(ManeuverSpeed.Speed3, ManeuverDirection.Right, ManeuverBearing.Turn, MovementComplexity.Normal),

                    new ManeuverInfo(ManeuverSpeed.Speed4, ManeuverDirection.Forward, ManeuverBearing.Straight, MovementComplexity.Complex)
                );

                SoundInfo = new ShipSoundInfo(
                    new List<string>()
                    {
                        "Slave1-Fly1",
                        "Slave1-Fly2"
                    },
                    "Slave1-Fire", 2
                );

                ShipIconLetter = '&';
            }
        }
    }
}

namespace Abilities.SecondEdition
{
    public class AlphaClassStarWingSLAbility : GenericAbility
    {
        public override void ActivateAbility()
        {
            HostShip.OnCombatActivation += RegisterAlphaClassStarWingSLAbility;
        }

        public override void DeactivateAbility()
        {
            HostShip.OnCombatActivation -= RegisterAlphaClassStarWingSLAbility;
        }

        public void RegisterAlphaClassStarWingSLAbility(GenericShip ship)
        {
            RegisterAbilityTrigger(TriggerTypes.OnCombatActivation, CheckStrain);
        }

        public void CheckStrain(object sender, System.EventArgs e)
        {
            if (HostShip.Tokens.HasToken<WeaponsDisabledToken>())
            {
                AskToUseAbility(
                    HostShip.PilotInfo.PilotName,
                    AlwaysUseByDefault,
                    UseAbility,
                    descriptionLong: "Do you want to receive 1 Strain Token to remove Disarm Token?",
                    imageHolder: HostShip
                );
            }
            else
            {
                Triggers.FinishTrigger();
            }
        }

        public void UseAbility(object sender, System.EventArgs e)
        {
            Messages.ShowInfo(HostShip.PilotInfo.PilotName + " recieved Strain token to remove a Disarm Token");

            HostShip.Tokens.RemoveToken(
                typeof(WeaponsDisabledToken),
                delegate
                {
                    HostShip.Tokens.AssignToken(typeof(StrainToken), DecisionSubPhase.ConfirmDecision);
                }
            );
        }
    }
}