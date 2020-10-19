using Sirenix.OdinInspector;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.InputSystem;

public class RotationInput : MonoBehaviour
{
    public Color hoveredColor;
    public Color normalColor;
    public MeshRenderer meshRenderer;
    public SideRotation rotationStep;

    public Cube Cube { get; private set; }

    private bool _isHovered = true;
    public bool IsHovered
    {
        get
        {
            return _isHovered;
        }
        set
        {
            if (_isHovered != value)
            {
                meshRenderer.materials[0].SetColor("_Color", _isHovered ? hoveredColor : normalColor);
            }

            _isHovered = value;
        }
    }

    private void Start()
    {
        Cube = GetComponentInParent<Cube>();
    }

    private void Update()
    {
        UpdateVisibility();

        UpdateIsHovered();
    }

    private void UpdateVisibility()
    {
        var visible = true;

        if (Cube.showRotationStepInputsOnSelectedOnly)
        {
            visible = Cube.SelectedSide == rotationStep.side;
        }

        meshRenderer.gameObject.SetActive(visible);
    }

    private void UpdateIsHovered()
    {
        Ray ray = Camera.main.ScreenPointToRay(Pointer.current.position.ReadValue());
        RaycastHit hitInfo;
        if (Physics.Raycast(ray, out hitInfo))
        {
            if (hitInfo.collider.gameObject == this.gameObject)
            {
                IsHovered = true;
                return;
            }
        }

        IsHovered = false;
    }
}