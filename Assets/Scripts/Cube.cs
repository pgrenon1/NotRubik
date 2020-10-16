﻿using Sirenix.OdinInspector;
using System;
using System.Collections.Generic;
using UnityEditor;
using DG.Tweening;
using UnityEngine;
using UnityEngine.InputSystem;
using System.Collections;
using Random = UnityEngine.Random;

public class Cube : OdinSerializedBehaviour
{
    public Cubelet cubeletPrefab;
    public float rotationDuration = 1f;
    public float sideRotationSpeed = 0.25f;
    public Transform cubeletsParents;
    public bool showRotationStepInputsOnSelectedOnly = true;

    [BoxGroup("Debug"), PropertyOrder(1)]
    public Side debugSide;

    public List<Cubelet> AllCubelets { get; private set; } = new List<Cubelet>();
    public Dictionary<Side, List<Cubelet>> Sides { get; private set; } = new Dictionary<Side, List<Cubelet>>();
    public GameObject Rotator { get; private set; }
    public CubeDimensions Dimensions { get; private set; }
    public bool IsRotatingSide { get; private set; } = false;
    public bool IsRotating { get; private set; } = false;
    public bool IsShuffling { get; private set; } = false;

    public Side SelectedSide { get; set; } = Side.None;

    private bool _lol;

    [BoxGroup("Debug"), Button("Group Debug Side"), PropertyOrder(1)]
    public void GroupDebugSide()
    {
        GroupSide(debugSide);
    }

    public void Init(LevelData levelData)
    {
        Dimensions = levelData.dimensions;

        CreateCubelets();

        Rotator = new GameObject("SideRotator");
        Rotator.transform.SetParent(cubeletsParents);

        GetComponentInChildren<CubeInputs>().Cube = this;

        SetupSides();

        IsRotatingSide = false;
    }

    private void Update()
    {
        UpdateActiveSideVisuals();
    }

    private void UpdateActiveSideVisuals()
    {
        var worldDirection = Util.GetWorldDirectionForSide(SelectedSide);

        foreach (var cubelet in AllCubelets)
        {
            var cubeletIsInSelectedSide = Sides.Count > 1 && Sides[SelectedSide].Contains(cubelet);

            if (cubeletIsInSelectedSide)
                Debug.DrawRay(cubelet.transform.position, worldDirection * 3f, Color.red);

            foreach (var facelet in cubelet.facelets)
            {
                var faceletIsOnActiveSide = !IsRotating &&
                                            !IsRotatingSide &&
                                            SelectedSide != Side.None &&
                                            cubeletIsInSelectedSide &&
                                            facelet == cubelet.GetFaceletAtWorldDirection(worldDirection);
                facelet.highlight.SetActive(faceletIsOnActiveSide);
            }
        }
    }

    public void RotateSelectedSide(bool clockwise)
    {
        RotateSide(new RotationStep(SelectedSide, clockwise));
    }

    public void RotateSide(RotationStep rotationStep)
    {
        if (IsRotatingSide)
            return;

        IsRotatingSide = true;

        GroupSide(rotationStep.side);

        var axis = Util.GetAxisForRotationStep(rotationStep);
        RotateSelectedSide(axis);
    }

    private void RotateSelectedSide(Vector3 axis)
    {
        Rotator.transform.DOBlendableRotateBy(axis * 90f, sideRotationSpeed).OnComplete(SideRotationCompleted);
    }

    private void SideRotationCompleted()
    {
        RoundCubeletsPositions();

        SetupSides();

        IsRotatingSide = false;
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

        SelectedSide = side;

        ParentCubesToActiveSideParent(side);
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
                    cubelet.transform.SetParent(Rotator.transform);
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

        var max = Vector3.one * float.MinValue;
        var min = Vector3.one * float.MaxValue;

        foreach (var cubelet in AllCubelets)
        {
            Vector3 position = cubelet.transform.position;

            // greatest x
            if (position.x > max.x)
                max.x = position.x;
            // smallest x
            if (position.x < min.x)
                min.x = position.x;

            // greatest y 
            if (position.y > max.y)
                max.y = position.y;
            // smallest y 
            if (position.y < min.y)
                min.y = position.y;

            // greatest z
            if (position.z > max.z)
                max.z = position.z;
            // smallest z
            if (position.z < min.z)
                min.z = position.z;
        }

        foreach (var cubelet in AllCubelets)
        {
            Vector3 position = cubelet.transform.position;

            if (Mathf.Abs(position.y - max.y) < 0.1f)
            {
                Sides[Side.Up].Add(cubelet);
            }

            if (Mathf.Abs(position.y - min.y) < 0.1f)
            {
                Sides[Side.Down].Add(cubelet);
            }

            if (Mathf.Abs(position.x - min.x) < 0.1f)
            {
                Sides[Side.Right].Add(cubelet);
            }

            if (Mathf.Abs(position.x - max.x) < 0.1f)
            {
                Sides[Side.Left].Add(cubelet);
            }

            if (Mathf.Abs(position.z - max.z) < 0.1f)
            {
                Sides[Side.Front].Add(cubelet);
            }

            if (Mathf.Abs(position.z - min.z) < 0.1f)
            {
                Sides[Side.Back].Add(cubelet);
            }
        }
    }

    private void SelectSide(Side sideToSelect)
    {
        SetupSides();

        SelectedSide = sideToSelect;
    }

    private void ClearActiveSideParent()
    {
        foreach (var cube in AllCubelets)
        {
            cube.transform.SetParent(cubeletsParents);
        }

        Rotator.transform.localRotation = Quaternion.identity;
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
        _lol = true;
    }

    private IEnumerator DoShuffle(int numberOfSteps)
    {
        var sides = new List<Side>() { Side.Front, Side.Left, Side.Up };

        for (int i = 0; i < numberOfSteps; i++)
        {
            RotateSide(new RotationStep(sides.RandomElement(), UnityEngine.Random.Range(0f, 100f) > 50f));

            yield return new WaitWhile(() => IsRotatingSide);
        }

        IsShuffling = false;
    }

    public void MoveSelection(Vector2 direction)
    {
        if (direction == Vector2.left)
            SelectedSide = Side.Left;
        else if (direction == Vector2.right)
            SelectedSide = Side.Front;
        else if (direction == Vector2.up)
            SelectedSide = Side.Up;
        else if (direction == Vector2.down && SelectedSide == Side.Up)
            SelectedSide = Side.Left;
    }

    public void RotateTo(Quaternion targetRotation)
    {
        cubeletsParents.DORotateQuaternion(targetRotation, rotationDuration).OnComplete(CubeRotationCompleted);
    }

    public void RotateBy(Vector3 rotation)
    {
        if (IsRotating)
            return;

        IsRotating = true;

        cubeletsParents.DOBlendableRotateBy(rotation, rotationDuration).OnComplete(CubeRotationCompleted);
    }

    private void CubeRotationCompleted()
    {
        RoundCubeletsRotation();

        SetupSides();

        IsRotating = false;
    }

    private void RoundCubeletsRotation()
    {
        var euler = cubeletsParents.rotation.eulerAngles;

        euler.x = Mathf.LerpAngle(euler.x, Mathf.Round(euler.x / 90f) * 90f, 1f);
        euler.y = Mathf.LerpAngle(euler.y, Mathf.Round(euler.y / 90f) * 90f, 1f);
        euler.z = Mathf.LerpAngle(euler.z, Mathf.Round(euler.z / 90f) * 90f, 1f);

        cubeletsParents.eulerAngles = euler;
    }

    private void OnGUI()
    {
        if (GUILayout.Button("Shuffle"))
        {
            Shuffle(10);
        }

        if (!_lol)
        {
            if (GUILayout.Button("Solve"))
            {
                Solve();
            }
        }
        else
        {
            if (GUILayout.Button("lol"))
            {
                Debug.Log("nope");
            }
        }

        GUILayout.BeginVertical();
        {
            GUILayout.BeginHorizontal();
            {
                if (GUILayout.Button("X+"))
                {
                    RotateBy(Vector3.right * 90f);
                }
                if (GUILayout.Button("X-"))
                {
                    RotateBy(Vector3.left * 90f);
                }
            }
            GUILayout.EndHorizontal();

            GUILayout.BeginHorizontal();
            {
                if (GUILayout.Button("Y+"))
                {
                    RotateBy(Vector3.up * 90f);
                }
                if (GUILayout.Button("Y-"))
                {
                    RotateBy(Vector3.down * 90f);
                }
            }
            GUILayout.EndHorizontal();

            GUILayout.BeginHorizontal();
            {
                if (GUILayout.Button("Z+"))
                {
                    RotateBy(Vector3.forward * 90f);
                }
                if (GUILayout.Button("Z-"))
                {
                    RotateBy(Vector3.back * 90f);
                }
            }
            GUILayout.EndHorizontal();

            if (GUILayout.Button("Rotate To Origin"))
            {
                RotateTo(Quaternion.identity);
            }
        }
        GUILayout.EndVertical();

        GUILayout.BeginVertical();
        {
            GUILayout.BeginHorizontal();
            if (GUILayout.Button("Select Front"))
            {
                SelectSide(Side.Front);
            }
            if (GUILayout.Button("Select Back"))
            {
                SelectSide(Side.Back);
            }
            GUILayout.EndHorizontal();

            GUILayout.BeginHorizontal();
            if (GUILayout.Button("Select Up"))
            {
                SelectSide(Side.Up);
            }
            if (GUILayout.Button("Select Down"))
            {
                SelectSide(Side.Down);
            }
            GUILayout.EndHorizontal();

            GUILayout.BeginHorizontal();
            if (GUILayout.Button("Select Right"))
            {
                SelectSide(Side.Right);
            }
            if (GUILayout.Button("Select Left"))
            {
                SelectSide(Side.Left);
            }
            GUILayout.EndHorizontal();
        }
        GUILayout.EndVertical();
    }
}