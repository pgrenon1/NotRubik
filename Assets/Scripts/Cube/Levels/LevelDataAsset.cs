using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System;

[CreateAssetMenu(fileName = "LevelData", menuName = "Assets/Levels/LevelDataAsset", order = 0)]



public class LevelDataAsset : OdinSerializedScriptableObject
{

    public string levelName;
    public string levelDescription;
    public CubeDimensions dimensions;

    
    public List<Tile> levelTiles;


}