using System.Collections.Generic;
using UnityEngine;


public class Tile : MonoBehaviour
{

    public TileData tileData;
    public Facelet attachedFacelet;

    private void Start()
    {
        
        //Create prefab that will have an image


    }


    public bool DisplayTileEffect()
    {

        //Hardcoding the first one in the list, but it should be iterated on if there's more than one.
        //To be implemented later, of course.
        attachedFacelet.effectVisual.sprite = tileData.tileEffects[0].ExamineTileEffectData().tileEffectIcon;
        return true;
    }

 

 


    public void AttachFacelet(Facelet facelet)
    {
        attachedFacelet = facelet;
    }
    
}