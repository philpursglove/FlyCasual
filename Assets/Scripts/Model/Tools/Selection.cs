﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using Ship;

public static class Selection {

    public static GenericShip ThisShip;
    public static GenericShip AnotherShip;
    public static GenericShip ActiveShip;
    public static GenericShip HoveredShip;
    public static List<GenericShip> MultiSelectedShips { get; private set; }

    public static void Initialize()
    {
        ThisShip = null;
        AnotherShip = null;
        ActiveShip = null;
        HoveredShip = null;
        MultiSelectedShips = new List<GenericShip>();
    }

    //TODO: BUG - enemy ship can be selected
    public static void UpdateSelection()
    {
        if (!EventSystem.current.IsPointerOverGameObject() && 
            (Input.touchCount == 0 || !EventSystem.current.IsPointerOverGameObject(Input.GetTouch(0).fingerId)))
        {
            TryMarkShipByModel();
            int mouseKeyIsPressed = 0;
            // On touch devices, select on down instead of up event so dragging in ship setup can begin immediately
            // TODO: Could make that only apply during setup rather than for all selections. I don't think this is a big issues though?
            if ((CameraScript.InputMouseIsEnabled && Input.GetKeyUp(KeyCode.Mouse0)) ||
                (CameraScript.InputTouchIsEnabled && Input.touchCount == 1 && Input.GetTouch(0).phase == TouchPhase.Began))
            {
                mouseKeyIsPressed = 1;
            }
            else if (CameraScript.InputMouseIsEnabled && Input.GetKeyUp(KeyCode.Mouse1))
            {
                mouseKeyIsPressed = 2;
            }

            if (mouseKeyIsPressed > 0)
            {
                bool isShipHit = false;
                RaycastHit hitInfo = new RaycastHit();
                if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hitInfo))
                {
                    if (hitInfo.transform.tag.StartsWith("ShipId:"))
                    {
                        isShipHit = TryToChangeShip(hitInfo.transform.tag, mouseKeyIsPressed);
                    }
                }
                if (!isShipHit)
                {
                    if (mouseKeyIsPressed == 1) ProcessClick();
                    UI.HideTemporaryMenus();
                }
            }
        }
    }

    private static void TryMarkShipByModel()
    {
        RaycastHit hitInfo = new RaycastHit();
        if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hitInfo))
        {
            TryMarkShip(hitInfo.transform.tag);
        }
    }

    public static void TryMarkShip(string tag)
    {
        if (tag.StartsWith("ShipId:"))
        {
            TryUnmarkPreviousHoveredShip();
            HoveredShip = Roster.AllUnits[tag];
            if ((HoveredShip != ThisShip) && (HoveredShip != AnotherShip))
            {
                HoveredShip.HighlightAnyHovered();
                Roster.MarkShip(HoveredShip, Color.yellow);
            }
        }
        else
        {
            TryUnmarkPreviousHoveredShip();
        }
    }

    public static void TryUnmarkPreviousHoveredShip()
    {
        if (HoveredShip != null)
        {
            if ((HoveredShip != ThisShip) && (HoveredShip != AnotherShip))
            {
                HoveredShip.HighlightSelectedOff();
                Roster.UnMarkShip(HoveredShip);
                HoveredShip = null;
            }
        }
    }

    public static bool TryToChangeShip(string shipId, int mouseKeyIsPressed = 1)
    {
        bool result = false;

        if (Phases.CurrentSubPhase != null)
        {
            GenericShip ship = Roster.GetShipById(shipId);
            if (Phases.CurrentSubPhase.AllowsMultiplayerSelection)
            {
                result = TryToChangeThisShip(shipId, mouseKeyIsPressed);
            }
            else
            {
                if (ship.Owner.PlayerNo == Phases.CurrentSubPhase.RequiredPlayer)
                {
                    result = TryToChangeThisShip(shipId, mouseKeyIsPressed);
                }
                else
                {
                    result = TryToChangeAnotherShip(shipId, mouseKeyIsPressed);
                }
            }
        }

        return result;
    }

    private static void ProcessClick()
    {
        if (Phases.CurrentSubPhase != null) Phases.CurrentSubPhase.ProcessClick();
    }

    //TODO: call from roster info panel click too
    public static bool TryToChangeAnotherShip(string shipId, int mouseKeyIsPressed = 1)
    {
        bool result = false;
        GenericShip targetShip = Roster.GetShipById(shipId);
        result = Phases.CurrentSubPhase.AnotherShipCanBeSelected(targetShip, mouseKeyIsPressed);

        if (result == true)
        {
            ChangeAnotherShip(shipId);
            DoSelectAnotherShip(mouseKeyIsPressed);
        }
        return result;
    }

    public static bool TryToChangeThisShip(string shipId, int mouseKeyIsPressed = 1)
    {
        bool result = false;

        GenericShip ship = Roster.GetShipById(shipId);

        result = Phases.CurrentSubPhase.ThisShipCanBeSelected(ship, mouseKeyIsPressed);

        if (result == true)
        {
            Selection.ChangeActiveShip(shipId);
            DoSelectThisShip(mouseKeyIsPressed);
        }

        return result;
    }

    public static void ChangeActiveShip(string shipId)
    {
        DeselectThisShip();
        ThisShip = Roster.GetShipById(shipId);
        ChangeActiveShipUsingThisShip();
    }

    public static void ChangeActiveShip(GenericShip genShip)
    {
        DeselectThisShip();
        ThisShip = genShip;

        ChangeActiveShipUsingThisShip();
    }

    private static void ChangeActiveShipUsingThisShip()
    {
        ThisShip.ToggleCollisionDetection(true);
        Roster.MarkShip(ThisShip, Color.green);
        ThisShip.HighlightThisSelected();
    }

    private static void DoSelectThisShip(int mouseKeyIsPressed)
    {
        if (Roster.GetPlayer(Phases.CurrentPhasePlayer).GetType() == typeof(Players.HumanPlayer) || Phases.CurrentSubPhase.AllowsMultiplayerSelection)
        {
            Phases.CurrentSubPhase.DoSelectThisShip(ThisShip, mouseKeyIsPressed);
        }
    }

    public static void DeselectThisShip()
    {
        if (ThisShip != null)
        {
            DeselectShip(ThisShip);
            ThisShip = null;
        }
    }

    public static void ChangeAnotherShip(GenericShip ship)
    {
        ChangeAnotherShip("ShipId:" + ship.ShipId);
    }

    public static void ChangeAnotherShip(string shipId)
    {
        if (AnotherShip != null)
        {
            Roster.UnMarkShip(AnotherShip);
            AnotherShip.HighlightSelectedOff();
        }
        AnotherShip = Roster.GetShipById(shipId);
        Roster.MarkShip(AnotherShip, Color.red);
        AnotherShip.HighlightEnemySelected();
    }

    private static void DoSelectAnotherShip(int mouseKeyIsPressed)
    {
        if (Roster.GetPlayer(Phases.CurrentPhasePlayer).GetType() == typeof(Players.HumanPlayer)) Phases.CurrentSubPhase.DoSelectAnotherShip(AnotherShip, mouseKeyIsPressed);
    }

    public static void DeselectAnotherShip()
    {
        if (AnotherShip != null)
        {
            DeselectShip(AnotherShip);
            AnotherShip = null;
        }
    }

    private static void DeselectShip(GenericShip ship)
    {
        ship.ToggleCollisionDetection(false);
        Roster.UnMarkShip(ship);
        ship.HighlightSelectedOff();
    }

    public static void DeselectAllShips()
    {
        DeselectThisShip();
        DeselectAnotherShip();
    }

    public static void ToggleMultiSelection(GenericShip ship)
    {
        if (MultiSelectedShips.Contains(ship))
        {
            MultiSelectedShips.Remove(ship);
        }
        else
        {
            MultiSelectedShips.Add(ship);
        }

        ship.ToggleMultiSelectionProjector();
    }

    public static void ClearMultiSelection()
    {
        HideMultiSelectionHighlight();
        MultiSelectedShips = new List<GenericShip>();
    }

    public static void HideMultiSelectionHighlight()
    {
        foreach (GenericShip ship in MultiSelectedShips)
        {
            ship.TurnOffMultiSelectionProjector();
        }
    }
}
