using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputManager : OdinserializedSingletonBehaviour<InputManager>
{
    private PlayerControls _playerControls;
    private List<RotationInput> _rotationInputs = new List<RotationInput>();

    protected override void Awake()
    {
        base.Awake();

        _playerControls = new PlayerControls();
        _playerControls.Enable();

        _playerControls.PlayerActions.Press.performed += ctx => PointerPress();

        _rotationInputs = GetComponentsInChildren<RotationInput>().ToList();
    }

    private void PointerPress()
    {
        Vector2 worldPos = Camera.main.ScreenToWorldPoint(Pointer.current.position.ReadValue());

        RaycastHit hitInfo;
            Debug.Log(worldPos);
        if (Physics.Raycast(Camera.main.transform.position, worldPos, out hitInfo))
        {
            RotationInput rotationInput = hitInfo.collider.GetComponentInParent<RotationInput>();
            if (rotationInput != null)
            {
                LevelManager.Instance.CurrentCube.RotateSide(rotationInput.rotationStep);
            }
        }
    }
}
