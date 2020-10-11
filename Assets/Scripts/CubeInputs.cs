using System;
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
        PlayerControls.CheatActions.TestRotations.performed += ctx => Shuffle();
    }

    private void Shuffle()
    {
        Cube.Shuffle(10);
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
