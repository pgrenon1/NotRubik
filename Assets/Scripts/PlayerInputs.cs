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

    protected void Awake()
    {
        PlayerControls = new PlayerControls();
        PlayerControls.Enable();
    }

    private void Start()
    {
        PlayerControls.CheatActions.Shuffle.performed += ctx => Shuffle();
        PlayerControls.CheatActions.CycleSelection.performed += ctx => CycleSelection();

        PlayerControls.PlayerActions.Press.performed += ctx => PointerPress();
        PlayerControls.PlayerActions.Undo.performed += ctx => Undo();
        PlayerControls.PlayerActions.Move.performed += ctx => MoveSelection(PlayerControls.PlayerActions.Move.ReadValue<Vector2>());
        PlayerControls.PlayerActions.RotateSideClockwise.performed += ctx => RotateSelectedSide(true);
        PlayerControls.PlayerActions.RotateSideCounterclockwise.performed += ctx => RotateSelectedSide(false);
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
        if (Physics.Raycast(ray, out hitInfo))
        {
            RotationInput rotationInput = hitInfo.collider.GetComponentInParent<RotationInput>();
            if (rotationInput != null)
            {
                rotationInput.rotationStep.TryExecute(Cube);
            }
        }
    }
}
