using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.InputSystem;

public class CubeInputs : MonoBehaviour
{
    public PlayerControls PlayerControls { get; set; }

    protected void Awake()
    {
        PlayerControls = new PlayerControls();
        PlayerControls.Enable();
    }

    private void Start()
    {
        PlayerControls.PlayerActions.Press.performed += ctx => PointerPress();
        PlayerControls.PlayerActions.Up.performed += ctx => RotateCube(Vector3.up);
        //PlayerControls.PlayerActions.Down.performed += ctx => RotateCube(Vector3.down);
        //PlayerControls.PlayerActions.Left.performed += ctx => RotateCube(Vector3.left);
        //PlayerControls.PlayerActions.Right.performed += ctx => RotateCube(Vector3.right);
    }

    private void RotateCube(Vector3 up)
    {
        if (LevelManager.Instance.CurrentCube.ActiveSide == Side.Right)
            LevelManager.Instance.CurrentCube.ActiveSide = Side.None;
        else
            LevelManager.Instance.CurrentCube.ActiveSide = LevelManager.Instance.CurrentCube.ActiveSide + 1;

        Debug.Log(LevelManager.Instance.CurrentCube.ActiveSide);
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
