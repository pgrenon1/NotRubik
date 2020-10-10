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
    public RotationStep rotationStep;

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

    private void Update()
    {
        UpdateIsHovered();
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