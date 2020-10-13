using Sirenix.OdinInspector;
using System;
using System.Collections.Generic;
using UnityEditor;
using DG.Tweening;
using UnityEngine;
using UnityEngine.InputSystem;
using System.Collections;

public class Cube : OdinSerializedBehaviour
{
    public Cubelet cubeletPrefab;
    public float rotationSpeed = 1f;
    public float sideRotationSpeed = 0.25f;
    public Transform cubeletsParents;

    [BoxGroup("Debug"), PropertyOrder(1)]
    public Side debugSide;

    public List<Cubelet> AllCubelets { get; private set; } = new List<Cubelet>();
    public Dictionary<Side, List<Cubelet>> Sides { get; private set; } = new Dictionary<Side, List<Cubelet>>();
    public GameObject ActiveSideParent { get; private set; }
    public CubeDimensions Dimensions { get; private set; }
    public bool IsRotating { get; private set; } = false;
    public bool IsShuffling { get; private set; } = false;
    public Side ActiveSide { get; set; } = Side.None;

    [BoxGroup("Debug"), Button("Group Debug Side"), PropertyOrder(1)]
    public void GroupDebugSide()
    {
        GroupSide(debugSide);
    }

    public void Init(LevelData levelData)
    {
        Dimensions = levelData.dimensions;

        CreateCubelets();

        ActiveSideParent = new GameObject("ActiveSideParent");
        ActiveSideParent.transform.SetParent(cubeletsParents);

        GetComponentInChildren<CubeInputs>().Cube = this;

        SetupSides();

        IsRotating = false;
    }

    private void Update()
    {
        UpdateActiveSideVisuals();
    }

    private void UpdateActiveSideVisuals()
    {
        var worldDirection = Util.GetWorldDirectionForSide(ActiveSide);
        foreach (var cubelet in AllCubelets)
        {
            var cubeletIsInActiveSide = Sides.Count > 1 && Sides[ActiveSide].Contains(cubelet);

            foreach (var facelet in cubelet.facelets)
            {
                var faceletIsOnActiveSide = !IsRotating &&
                                            ActiveSide != Side.None &&
                                            cubeletIsInActiveSide &&
                                            facelet == cubelet.GetFaceletAtWorldDirection(worldDirection);
                facelet.highlight.SetActive(faceletIsOnActiveSide);
            }
        }
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
        RoundCubeletsPositions();

        SetupSides();

        IsRotating = false;
    }

    private void RoundCubeletsPositions()
    {
        var widthIsEven = Dimensions.width % 2 == 0;
        var heightIsEven = Dimensions.height % 2 == 0;
        var depthIsEven = Dimensions.depth % 2 == 0;

        foreach (var cubelet in AllCubelets)
        {
            RoundCubeletPosition(cubelet, widthIsEven, heightIsEven, depthIsEven);
            // fuck me lol
            cubelet.transform.localScale = Vector3.one;
        }
    }

    private static void RoundCubeletPosition(Cubelet cubelet, bool widthIsEven, bool heightIsEven, bool depthIsEven)
    {
        var xLocalPos = cubelet.transform.localPosition.x;
        var yLocalPos = cubelet.transform.localPosition.y;
        var zLocalPos = cubelet.transform.localPosition.z;

        if (widthIsEven)
            xLocalPos = Mathf.Round(xLocalPos * 2f) / 2f;
        else
            xLocalPos = Mathf.RoundToInt(xLocalPos);

        if (heightIsEven)
            yLocalPos = Mathf.Round(yLocalPos * 2f) / 2f;
        else
            yLocalPos = Mathf.RoundToInt(yLocalPos);

        if (depthIsEven)
            zLocalPos = Mathf.Round(zLocalPos * 2f) / 2f;
        else
            zLocalPos = Mathf.RoundToInt(zLocalPos);

        cubelet.transform.localPosition = new Vector3(xLocalPos, yLocalPos, zLocalPos);
    }

    public void GroupSide(Side side)
    {
        SetupSides();

        ActiveSide = side;

        ParentCubesToActiveSideParent(side);

#if UNITY_EDITOR
        Selection.activeObject = ActiveSideParent;
#endif
    }

    private void ParentCubesToActiveSideParent(Side side)
    {
        foreach (var kvp in Sides)
        {
            var cubelets = kvp.Value;
            var cubeletSide = kvp.Key;

            if (cubeletSide == side)
            {
                foreach (var cubelet in cubelets)
                {
                    cubelet.transform.SetParent(ActiveSideParent.transform);
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

                    var newCubelet = Instantiate(cubeletPrefab, new Vector3(xPos, yPos, zPos), Quaternion.identity, cubeletsParents);
                    newCubelet.name = "Cubelet " + i;
                    newCubelet.Cube = this;
                    newCubelet.Init();
                    AllCubelets.Add(newCubelet);
                    i++;
                }
            }
        }
    }

    private void SetupSides()
    {
        ClearActiveSideParent();

        ClearSides();

        var upY = float.MinValue;
        var downY = float.MaxValue;

        var leftX = float.MinValue;
        var rightX = float.MaxValue;

        var frontZ = float.MinValue;
        var backZ = float.MaxValue;

        foreach (var cube in AllCubelets)
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

        foreach (var cubelet in AllCubelets)
        {
            if (Mathf.Abs(cubelet.transform.localPosition.y - upY) < 0.01f)
            {
                Sides[Side.Up].Add(cubelet);
            }

            if (Mathf.Abs(cubelet.transform.localPosition.y - downY) < 0.01f)
            {
                Sides[Side.Down].Add(cubelet);
            }

            if (Mathf.Abs(cubelet.transform.localPosition.x - leftX) < 0.01f)
            {
                Sides[Side.Left].Add(cubelet);
            }

            if (Mathf.Abs(cubelet.transform.localPosition.x - rightX) < 0.01f)
            {
                Sides[Side.Right].Add(cubelet);
            }

            if (Mathf.Abs(cubelet.transform.localPosition.z - frontZ) < 0.01f)
            {
                Sides[Side.Front].Add(cubelet);
            }

            if (Mathf.Abs(cubelet.transform.localPosition.z - backZ) < 0.01f)
            {
                Sides[Side.Back].Add(cubelet);
            }
        }
    }

    private void ClearActiveSideParent()
    {
        foreach (var cube in AllCubelets)
        {
            cube.transform.SetParent(cubeletsParents);
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

    public void Shuffle(int numberOfSteps)
    {
        if (IsShuffling)
            return;

        IsShuffling = true;

        StartCoroutine(DoShuffle(numberOfSteps));
    }

    public void Solve()
    {

    }

    private IEnumerator DoShuffle(int numberOfSteps)
    {
        var sides = new List<Side>() { Side.Front, Side.Left, Side.Up };

        for (int i = 0; i < numberOfSteps; i++)
        {
            RotateSide(new RotationStep(sides.RandomElement(), UnityEngine.Random.Range(0f, 100f) > 50f));

            yield return new WaitWhile(() => IsRotating);
        }

        IsShuffling = false;
    }

    private void OnGUI()
    {
        if (GUILayout.Button("Shuffle"))
        {
            Shuffle(10);
        }

        if (GUILayout.Button("Solve"))
        {
            Solve();
        }
    }
}
