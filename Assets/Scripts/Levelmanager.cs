using Sirenix.OdinInspector;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class LevelManager : OdinserializedSingletonBehaviour<LevelManager>
{
    public Cube cubePrefab;
    
    public LevelSetting defaultLevelSettings;

    [BoxGroup("Debug"), PropertyOrder(1)]
    public Side debugSide;

    public Transform LevelParent { get; private set; }
    public LevelSetting CurrentLevelSetting { get; private set; }
    public List<Cube> AllCubes { get; private set; } = new List<Cube>();
    public Dictionary<Side, List<Cube>> Sides { get; private set; } = new Dictionary<Side, List<Cube>>();
    public GameObject ActiveSideParent { get; private set; }

    [Button("Generate Level", ButtonSizes.Large)]
    public void GenerateDefault()
    {
        Generate(defaultLevelSettings);
    }

    [BoxGroup("Debug"), Button("Group Debug Side"), PropertyOrder(1)]
    public void GroupDebugSide()
    {
        GroupSide(debugSide);
    }

    public void Generate(LevelSetting levelSetting)
    {
        DestroyLevel();

        CurrentLevelSetting = levelSetting;

        LevelParent = CreateLevelParent();

        CreateCubes();

        SetupSides();

        ActiveSideParent = new GameObject("ActiveSideParent");
        ActiveSideParent.transform.SetParent(LevelParent);
    }

    private void SetupSides()
    {
        ClearSides();

        var upY = (CurrentLevelSetting.height - 1f) / 2f;
        var downY = -(CurrentLevelSetting.height - 1f) / 2f;

        var leftX = (CurrentLevelSetting.width - 1f) / 2f;
        var rightX = -(CurrentLevelSetting.width - 1f) / 2f;

        var frontZ = (CurrentLevelSetting.depth - 1f) / 2f;
        var backZ = -(CurrentLevelSetting.depth - 1f) / 2f;

        foreach (var cube in AllCubes)
        {
            if (Mathf.Abs(cube.transform.position.y - upY) < 0.01f)
            {
                Sides[Side.Up].Add(cube);
            }

            if (Mathf.Abs(cube.transform.position.y - downY) < 0.01f)
            {
                Sides[Side.Down].Add(cube);
            }

            if (Mathf.Abs(cube.transform.position.x - leftX) < 0.01f)
            {
                Sides[Side.Left].Add(cube);
            }

            if (Mathf.Abs(cube.transform.position.x - rightX) < 0.01f)
            {
                Sides[Side.Right].Add(cube);
            }

            if (Mathf.Abs(cube.transform.position.z - frontZ) < 0.01f)
            {
                Sides[Side.Front].Add(cube);
            }

            if (Mathf.Abs(cube.transform.position.z - backZ) < 0.01f)
            {
                Sides[Side.Back].Add(cube);
            }
        }
    }

    private void GroupSide(Side side)
    {
        ClearActiveSideParent();

        SetupSides();

        foreach (var kvp in Sides)
        {
            if (kvp.Key == side)
            {
                foreach (var cube in kvp.Value)
                {
                    cube.transform.SetParent(ActiveSideParent.transform);
                }
            }
        }

#if UNITY_EDITOR
        Selection.activeObject = ActiveSideParent;
#endif
    }

    private void ClearActiveSideParent()
    {
        foreach (var cube in AllCubes)
        {
            cube.transform.SetParent(LevelParent.transform);
        }
    }

    private void ClearSides()
    {
        Sides.Clear();

        foreach (Side side in Enum.GetValues(typeof(Side)))
        {
            Sides.Add(side, new List<Cube>());
        }
    }

    private void CreateCubes()
    {
        int i = 0;
        for (int x = 0; x < CurrentLevelSetting.width; x++)
        {
            for (int y = 0; y < CurrentLevelSetting.height; y++)
            {
                for (int z = 0; z < CurrentLevelSetting.depth; z++)
                {
                    float xPos = x - (CurrentLevelSetting.width - 1f) / 2f;
                    float yPos = y - (CurrentLevelSetting.height - 1f) / 2f;
                    float zPos = z - (CurrentLevelSetting.depth - 1f) / 2f;

                    AddCube(i, new Vector3(xPos, yPos, zPos));
                    i++;
                }
            }
        }
    }

    private Transform CreateLevelParent()
    {
        var level = new GameObject("LevelParent").transform;
        level.transform.parent = transform;
        return level;
    }

    private void DestroyLevel()
    {
        if (LevelParent != null)
            DestroyImmediate(LevelParent.gameObject);

        AllCubes.Clear();
    }

    private void AddCube(int index, Vector3 position)
    {
        var newCube = Instantiate(cubePrefab, position, Quaternion.identity, LevelParent);
        newCube.name = "Cube " + index;
        AllCubes.Add(newCube);
    }
}
