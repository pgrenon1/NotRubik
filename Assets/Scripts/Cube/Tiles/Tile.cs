using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class Tile : MonoBehaviour
{
    public TileData tileType;
    public Facelet Facelet { get; set; }

    public bool DisplayTileEffect(bool visibility)
    {

        //Hardcoding the first one in the list, but it should be iterated on if there's more than one - and displayed accordingly.

      //  attachedFacelet.effectVisual.sprite = tileData.tileEffects[0].GetTileEffectData().tileEffectIcon;
        return true;
    }
}