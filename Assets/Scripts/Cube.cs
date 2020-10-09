using Sirenix.OdinInspector;
using System;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class Cube : OdinSerializedBehaviour
{
    public Cubelet cubeletPrefab;

    [BoxGroup("Debug"), PropertyOrder(1)]
    public Side debugSide;

    public List<Cubelet> AllCubes { get; private set; } = new List<Cubelet>();
    public Dictionary<Side, List<Cubelet>> Sides { get; private set; } = new Dictionary<Side, List<Cubelet>>();
    public GameObject ActiveSideParent { get; private set; }

    public CubeDimensions Dimensions { get; set; }

    [BoxGroup("Debug"), Button("Group Debug Side"), PropertyOrder(1)]
    public void GroupDebugSide()
    {
        GroupSide(debugSide);
    }

    public void Init(LevelData levelData)
    {
        Dimensions = levelData.dimensions;

        CreateCubelets();

        SetupSides();

        ActiveSideParent = new GameObject("ActiveSideParent");
        ActiveSideParent.transform.SetParent(transform);
    }

    private void AddCube(int index, Vector3 position)
    {
        var newCube = Instantiate(cubeletPrefab, position, Quaternion.identity, transform);
        newCube.name = "Cube " + index;
        AllCubes.Add(newCube);
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

    private void CreateCubelets()
    {
        int i = 0;
        for (int x = 0; x < Dimensions.width; x++)
        {
            for (int y = 0; y < Dimensions.height; y++)
            {
                for (int z = 0; z < Dimensions.depth; z++)
                {
                    float xPos = x - (Dimensions.width - 1f) / 2f;
                    float yPos = y - (Dimensions.height - 1f) / 2f;
                    float zPos = z - (Dimensions.depth - 1f) / 2f;

                    AddCube(i, new Vector3(xPos, yPos, zPos));
                    i++;
                }
            }
        }
    }

    private void SetupSides()
    {
        ClearSides();

        var upY = (Dimensions.height - 1f) / 2f;
        var downY = -(Dimensions.height - 1f) / 2f;

        var leftX = (Dimensions.width - 1f) / 2f;
        var rightX = -(Dimensions.width - 1f) / 2f;

        var frontZ = (Dimensions.depth - 1f) / 2f;
        var backZ = -(Dimensions.depth - 1f) / 2f;

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

    private void ClearActiveSideParent()
    {
        foreach (var cube in AllCubes)
        {
            cube.transform.SetParent(transform);
        }
    }

    private void ClearSides()
    {
        Sides.Clear();

        foreach (Side side in Enum.GetValues(typeof(Side)))
        {
            Sides.Add(side, new List<Cubelet>());
        }
    }
}
