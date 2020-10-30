using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInputs : MonoBehaviour
{
    public PlayerControls PlayerControls { get; set; }
    public Cube Cube { get; set; }
    public CubeDebugMenu CubeDebugMenu { get; set; }

    protected void Awake()
    {
        PlayerControls = new PlayerControls();
        PlayerControls.Enable();
    }

    private void Start()
    {
        // Cheat Actions
        PlayerControls.CheatActions.Shuffle.performed += ctx => Shuffle();
        PlayerControls.CheatActions.CycleSelection.performed += ctx => CycleSelection();
        PlayerControls.CheatActions.ToggleDebugMenu.performed += ctx => ToggleDebugMenu();

        // Player Actions
        PlayerControls.PlayerActions.Press.performed += ctx => PointerPress();
        PlayerControls.PlayerActions.Undo.performed += ctx => Undo();
        PlayerControls.PlayerActions.Move.performed += ctx => MoveSelection(PlayerControls.PlayerActions.Move.ReadValue<Vector2>());
        PlayerControls.PlayerActions.RotateSideClockwise.performed += ctx => RotateSelectedSide(true);
        PlayerControls.PlayerActions.RotateSideCounterclockwise.performed += ctx => RotateSelectedSide(false);
    }

    private void ToggleDebugMenu()
    {
        CubeDebugMenu.Toggle();
    }

    private void Undo()
    {
        Cube.Undo();
    }

    private void RotateSelectedSide(bool clockwise)
    {
        var sideRotation = new SideRotation(Cube.SelectedSide, clockwise);
        sideRotation.TryExecute(Cube);
    }

    private void MoveSelection(Vector2 direction)
    {
        if (direction != Vector2.zero)
            Cube.MoveSelection(direction);
    }

    private void Shuffle()
    {
        Cube.Shuffle(10);
    }

    private void CycleSelection()
    {
        if (Cube.SelectedSide == Side.Right)
            Cube.SelectedSide = Side.None;
        else
            Cube.SelectedSide = Cube.SelectedSide + 1;

        Debug.Log(Cube.SelectedSide);
    }

    private void PointerPress()
    {
        Ray ray = Camera.main.ScreenPointToRay(Pointer.current.position.ReadValue());
        RaycastHit hitInfo;
        if (Physics.Raycast(ray, out hitInfo, LayerMaskManager.Instance.playerInputsLayerMask))
        {
            HandlePointerPress(hitInfo.collider);
        }
    }

    private void HandlePointerPress(Collider collider)
    {
        RotationInput rotationInput = collider.GetComponentInParent<RotationInput>();
        if (rotationInput != null)
        {
            rotationInput.rotationStep.TryExecute(Cube);
        }

        Facelet facelet = collider.GetComponent<Facelet>();
        if (facelet != null)
        {
            var side = Cube.GetSideForFacelet(facelet);
            var faceletIsOnTopSide = side == Side.Up;
            var node = GraphManager.Instance.GetNodeForFacelet(facelet);

            if (faceletIsOnTopSide && GameManager.Instance.Player.CanMove(node))
            {
                GameManager.Instance.Player.Mover.Move(node);
            }
        }
    }
}
