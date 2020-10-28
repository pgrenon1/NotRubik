using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileEffectVisual : MonoBehaviour
{

    private void Start()
    {
        UpdateEffectVisualDirection();

    }

    public void UpdateEffectVisualDirection()
    {
        transform.LookAt(Camera.main.transform.position, -Vector3.up);

    }
}
