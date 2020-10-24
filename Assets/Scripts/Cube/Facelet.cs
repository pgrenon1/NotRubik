using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Facelet : MonoBehaviour
{
    public GameObject highlight;

    public Cubelet Cubelet { get; set; }

    public void UpdateHighlightVisibility(bool isHighlighted)
    {
        highlight.SetActive(isHighlighted);
    }
}
