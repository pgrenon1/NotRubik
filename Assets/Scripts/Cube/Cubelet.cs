using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cubelet : MonoBehaviour
{
    public List<Facelet> facelets = new List<Facelet>();

    public Cube Cube { get; set; }
    public bool IsInActiveSide { get; set; }

    public void Init()
    {
        foreach (var facelet in facelets)
        {
            facelet.Cubelet = this;
        }
    }

    public Facelet GetFaceletAtWorldDirection(Vector3 worldDirection)
    {
        foreach (var facelet in facelets)
        {
            // -forward because for some reason the quads of the facelets are visible only from their back, not the forward???
            Vector3 forward = -facelet.transform.forward;

            var angle = Vector3.Angle(forward, worldDirection);

            if (angle < 5)
                return facelet;
        }

        return null;
    }
}
