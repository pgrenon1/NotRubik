using System.Collections.Generic;
using UnityEngine;


public class Tile : MonoBehaviour
{

    public TileData tileData;
    public Facelet attachedFacelet;


    private void Start()
    {
        DisplayTileEffect(true);
    }

    public bool DisplayTileEffect(bool visibility)
    {

        //Hardcoding the first one in the list, but it should be iterated on if there's more than one - and displayed accordingly.

        attachedFacelet.effectVisual.sprite = tileData.tileEffects[0].GetTileEffectData().tileEffectIcon;
        return true;
    }

 

 


    public void AttachFacelet(Facelet facelet)
    {
        attachedFacelet = facelet;
    }
    
}