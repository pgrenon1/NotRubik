using System.Collections.Generic;
using UnityEngine;


public class Tile : MonoBehaviour
{

    public TileData tileData;


    public void GetAllTileEffects()
    {
        ///As strings for now, just for testing
        foreach (TileEffect tileEffect in tileData.tileEffects)
        {
            tileEffect.ExamineTileEffectData();
        }
    }

    public void LinkToNode()
    {
    }
    
}