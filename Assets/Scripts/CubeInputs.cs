﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.InputSystem;

public class CubeInputs : MonoBehaviour
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
        PlayerControls.PlayerActions.Press.performed += ctx => PointerPress();
        PlayerControls.CheatActions.Shuffle.performed += ctx => Shuffle();
        PlayerControls.CheatActions.CycleSelection.performed += ctx => CycleSelection();

        PlayerControls.PlayerActions.Move.performed += ctx => MoveSelection(PlayerControls.PlayerActions.Move.ReadValue<Vector2>());
        PlayerControls.PlayerActions.RotateSideClockwise.performed += ctx => RotateSelectedSide(true);
        PlayerControls.PlayerActions.RotateSideCounterclockwise.performed += ctx => RotateSelectedSide(false);
    }

    private void RotateSelectedSide(bool clockwise)
    {
        Cube.RotateSelectedSide(clockwise);
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
        if (LevelManager.Instance.CurrentCube.SelectedSide == Side.Right)
            LevelManager.Instance.CurrentCube.SelectedSide = Side.None;
        else
            LevelManager.Instance.CurrentCube.SelectedSide = LevelManager.Instance.CurrentCube.SelectedSide + 1;

        Debug.Log(LevelManager.Instance.CurrentCube.SelectedSide);
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
                LevelManager.Instance.CurrentCube.RotateSide(rotationInput.rotationStep);
            }
        }
    }
}
