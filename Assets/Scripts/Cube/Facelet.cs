using UnityEngine;
using System.Collections.Generic;

public class Facelet : MonoBehaviour
{
    public Tile tile;
    public GameObject highlight;
    public SpriteRenderer effectVisual;
    
    public Cubelet Cubelet { get; set; }
    public bool IsOutwardFacing { get; set; }

    public void UpdateHighlightVisibility(bool isHighlighted)
    {
        highlight.SetActive(isHighlighted);
    }
}
