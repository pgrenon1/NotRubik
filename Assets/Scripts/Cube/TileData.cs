﻿using UnityEngine;
using System.Collections.Generic;
using UnityEngine.UI;

[CreateAssetMenu(fileName = "TileData", menuName = "Assets/TileData", order = 0)]
public class TileData : OdinSerializedScriptableObject
{

    /// <summary>
    /// Contains all the data required to populate a tile. 
    /// A tile could potentially have multiple effects?
    /// </summary>
   
    public List<TileEffect> tileEffects = new List<TileEffect>();
    
}