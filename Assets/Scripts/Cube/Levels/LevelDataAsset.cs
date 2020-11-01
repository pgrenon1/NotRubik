using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System;
using Sirenix.Serialization;
using Sirenix.OdinInspector;

[CreateAssetMenu(fileName = "LevelData", menuName = "Assets/Levels/LevelDataAsset", order = 0)]

//TODO : Fuse this into LevelData
public class LevelDataAsset : OdinSerializedScriptableObject
{

    public string levelName;
    public string levelDescription;

    //At the moment I don't think we need other dimensions than the default ones. Making this editor less error prone by hiding the CubeDimensions property
    public CubeDimensions Dimensions { get; set;}

    [OdinSerialize]
    public Dictionary<Side, SideTiles> levelTiles = new Dictionary<Side, SideTiles>();


}
