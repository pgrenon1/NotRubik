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

    public Dictionary<Side, List<TileData>> levelTiles = new Dictionary<Side, List<TileData>>()
    {
        { Side.Front, new List<TileData>() },
        { Side.Back, new List<TileData>() },
        { Side.Up, new List<TileData>() },
        { Side.Down, new List<TileData>() },
        { Side.Left, new List<TileData>() },
        { Side.Right, new List<TileData>() }


    };



}