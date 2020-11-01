using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System;
using Sirenix.Serialization;
using Sirenix.OdinInspector;

[CreateAssetMenu(fileName = "LevelData", menuName = "Assets/Levels/LevelDataAsset", order = 0)]

public class LevelDataAsset : OdinSerializedScriptableObject
{

    public string levelName;
    public string levelDescription;
    public CubeDimensions dimensions;

    [OdinSerialize]
    public Dictionary<Side, SideTiles> levelTiles = new Dictionary<Side, SideTiles>();

}
