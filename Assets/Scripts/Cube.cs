using Sirenix.OdinInspector;
using System;
using System.Collections.Generic;
using UnityEditor;
using DG.Tweening;
using UnityEngine;

public class Cube : OdinSerializedBehaviour
{
    public Cubelet cubeletPrefab;
    public float rotationSpeed = 1f;
    public float sideRotationSpeed = 0.25f;

    [BoxGroup("Debug"), PropertyOrder(1)]
    public Side debugSide;

    public List<Cubelet> AllCubes { get; private set; } = new List<Cubelet>();
    public Dictionary<Side, List<Cubelet>> Sides { get; private set; } = new Dictionary<Side, List<Cubelet>>();
    public GameObject ActiveSideParent { get; private set; }
    public CubeDimensions Dimensions { get; set; }
    public bool IsRotating { get; private set; }

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

        IsRotating = false;
    }

    public void RotateSide(RotationStep rotationStep)
    {
        if (IsRotating)
            return;

        IsRotating = true;

        GroupSide(rotationStep.side);

        RotateActiveSide(Util.GetAxisForRotationStep(rotationStep));
    }

    private void RotateActiveSide(Vector3 axis)
    {
        var rotation = Quaternion.Euler(axis * 90f);
        var result = transform.localRotation * rotation;

        ActiveSideParent.transform.DORotateQuaternion(result, sideRotationSpeed).OnComplete(RotationCompleted);
    }

    private void RotationCompleted()
    {
        IsRotating = false;
    }

    private void AddCube(int index, Vector3 position)
    {
        var newCube = Instantiate(cubeletPrefab, position, Quaternion.identity, transform);
        newCube.name = "Cube " + index;
        AllCubes.Add(newCube);
    }

    public void GroupSide(Side side)
    {
        ClearActiveSideParent();

        SetupSides();

        ParentCubesToSideRotator(side);

#if UNITY_EDITOR
        Selection.activeObject = ActiveSideParent;
#endif
    }

    private void ParentCubesToSideRotator(Side side)
    {
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

        var upY = float.MinValue;
        var downY = float.MaxValue;

        var leftX = float.MinValue;
        var rightX = float.MaxValue;

        var frontZ = float.MinValue;
        var backZ = float.MaxValue;

        foreach (var cube in AllCubes)
        {
            // greatest x
            if (cube.transform.localPosition.x > leftX)
                leftX = cube.transform.localPosition.x;
            // smallest x
            if (cube.transform.localPosition.x < rightX)
                rightX = cube.transform.localPosition.x;

            // greatest y 
            if (cube.transform.localPosition.y > upY)
                upY = cube.transform.localPosition.y;
            // smallest y 
            if (cube.transform.localPosition.y < downY)
                downY = cube.transform.localPosition.y;

            // greatest z
            if (cube.transform.localPosition.y > frontZ)
                frontZ = cube.transform.localPosition.z;
            // smallest z 
            if (cube.transform.localPosition.y < backZ)
                backZ = cube.transform.localPosition.z;
        }

        foreach (var cube in AllCubes)
        {
            if (Mathf.Abs(cube.transform.localPosition.y - upY) < 0.01f)
            {
                Sides[Side.Up].Add(cube);
            }

            if (Mathf.Abs(cube.transform.localPosition.y - downY) < 0.01f)
            {
                Sides[Side.Down].Add(cube);
            }

            if (Mathf.Abs(cube.transform.localPosition.x - leftX) < 0.01f)
            {
                Sides[Side.Left].Add(cube);
            }

            if (Mathf.Abs(cube.transform.localPosition.x - rightX) < 0.01f)
            {
                Sides[Side.Right].Add(cube);
            }

            if (Mathf.Abs(cube.transform.localPosition.z - frontZ) < 0.01f)
            {
                Sides[Side.Front].Add(cube);
            }

            if (Mathf.Abs(cube.transform.localPosition.z - backZ) < 0.01f)
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

        ActiveSideParent.transform.localRotation = Quaternion.identity;
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
