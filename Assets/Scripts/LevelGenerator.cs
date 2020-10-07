using Sirenix.OdinInspector;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelGenerator : OdinSerializedBehaviour
{
    public GameObject cubePrefab;
    
    [Header("Level Settings")]
    public LevelSetting defaultLevelSettings;

    public Transform Level { get; private set; }

    [Button("Generate Level")]
    public void GenerateDefault()
    {
        Generate(defaultLevelSettings);
    }

    public void Generate(LevelSetting levelSettings)
    {
        DestroyLevel();

        Level = CreateLevelParent();

        int i = 0;
        for (int x = 0; x < levelSettings.width; x++)
        {
            for (int y = 0; y < levelSettings.height; y++)
            {
                for (int z = 0; z < levelSettings.depth; z++)
                {
                    float xPos = x - (levelSettings.width - 1f) / 2f;
                    float yPos = y - (levelSettings.height - 1f) / 2f;
                    float zPos = z - (levelSettings.depth - 1f) / 2f;

                    AddCube(i, new Vector3(xPos, yPos, zPos));
                    i++;
                }
            }
        }
    }

    private Transform CreateLevelParent()
    {
        var level = new GameObject("Level").transform;
        level.transform.parent = transform;
        return level;
    }

    private void DestroyLevel()
    {
        if (Level)
            DestroyImmediate(Level.gameObject);
    }

    private void AddCube(int index, Vector3 position)
    {
        var newCube = Instantiate(cubePrefab, position, Quaternion.identity, Level);
        newCube.name = "Cube " + index;
    }
}

[System.Serializable]
public class LevelSetting
{
    public int width = 1;
    public int height = 1;
    public int depth = 1;
}