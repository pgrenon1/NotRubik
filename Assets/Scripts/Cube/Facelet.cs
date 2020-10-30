using UnityEngine;
using System.Collections.Generic;



public class Facelet : MonoBehaviour
{

    public GameObject highlight;
    public SpriteRenderer effectVisual;
    
   
    //The amount of tiles in a facelet is always Cube subdivisions ** 2
   

    public List<Tile> tiles;
    
    public Cubelet Cubelet { get; set; }



    public void UpdateHighlightVisibility(bool isHighlighted)
    {
        highlight.SetActive(isHighlighted);
    }
}
