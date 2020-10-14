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
    public float rotationDuration = 1f;
    public float sideRotationSpeed = 0.25f;
    public Transform cubeletsParents;

    [BoxGroup("Debug"), PropertyOrder(1)]
    public Side debugSide;

    public List<Cubelet> AllCubelets { get; private set; } = new List<Cubelet>();
    public Dictionary<Side, List<Cubelet>> Sides { get; private set; } = new Dictionary<Side, List<Cubelet>>();
    public GameObject RotatorParent { get; private set; }
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

        RotatorParent = new GameObject("SideRotator");
        RotatorParent.transform.SetParent(cubeletsParents);

        GetComponentInChildren<CubeInputs>().Cube = this;

        SetupSides(false);

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

            foreach (var facelet in cubelet.facelets)
            {
                var faceletIsOnActiveSide = !IsRotatingSide &&
                                            SelectedSide != Side.None &&
                                            cubeletIsInSelectedSide &&
                                            facelet == cubelet.GetFaceletAtWorldDirection(worldDirection);
                facelet.highlight.SetActive(faceletIsOnActiveSide);
            }
        }
    }

    public void RotateSide(RotationStep rotationStep)
    {
        if (IsRotatingSide)
            return;

        IsRotatingSide = true;

        GroupSide(rotationStep.side);

        RotateActiveSide(Util.GetAxisForRotationStep(rotationStep));
    }

    private void RotateActiveSide(Vector3 axis)
    {
        var rotation = Quaternion.Euler(axis * 90f);
        var result = transform.localRotation * rotation;

        RotatorParent.transform.DORotateQuaternion(result, sideRotationSpeed).OnComplete(SideRotationCompleted);
    }

    private void SideRotationCompleted()
    {
        RoundCubeletsPositions();

        SetupSides(false);

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
        SetupSides(false);

        SelectedSide = side;

        ParentCubesToActiveSideParent(side);

#if UNITY_EDITOR
        Selection.activeObject = RotatorParent;
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
                    cubelet.transform.SetParent(RotatorParent.transform);
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

    private void SetupSides(bool worldSides)
    {
        ClearActiveSideParent();

        ClearSides();

        var upY = float.MinValue;
        var downY = float.MaxValue;

        var leftX = float.MinValue;
        var rightX = float.MaxValue;

        var frontZ = float.MinValue;
        var backZ = float.MaxValue;

        foreach (var cubelet in AllCubelets)
        {
            Vector3 position;
            if (worldSides)
                position = cubelet.transform.position;
            else
                position = cubelet.transform.localPosition;

            // greatest x
            if (position.x > leftX)
                leftX = position.x;
            // smallest x
            if (position.x < rightX)
                rightX = position.x;

            // greatest y 
            if (position.y > upY)
                upY = position.y;
            // smallest y 
            if (position.y < downY)
                downY = position.y;

            // greatest z
            if (position.y > frontZ)
                frontZ = position.z;
            // smallest z 
            if (position.y < backZ)
                backZ = position.z;
        }

        foreach (var cubelet in AllCubelets)
        {
            Vector3 position;
            if (worldSides)
                position = cubelet.transform.position;
            else
                position = cubelet.transform.localPosition;

            if (Mathf.Abs(position.y - upY) < 0.01f)
            {
                Sides[Side.Up].Add(cubelet);
            }

            if (Mathf.Abs(position.y - downY) < 0.01f)
            {
                Sides[Side.Down].Add(cubelet);
            }

            if (Mathf.Abs(position.x - leftX) < 0.01f)
            {
                Sides[Side.Left].Add(cubelet);
            }

            if (Mathf.Abs(position.x - rightX) < 0.01f)
            {
                Sides[Side.Right].Add(cubelet);
            }

            if (Mathf.Abs(position.z - frontZ) < 0.01f)
            {
                Sides[Side.Front].Add(cubelet);
            }

            if (Mathf.Abs(position.z - backZ) < 0.01f)
            {
                Sides[Side.Back].Add(cubelet);
            }
        }
    }

    private void SelectSide(Side sideToSelect)
    {
        SetupSides(true);

        SelectedSide = sideToSelect;

        //SelectedSide = GetWorldSide(sideToSelect);
    }

    //private Side GetWorldSide(Side sideToSelect)
    //{

    //}

    private void ClearActiveSideParent()
    {
        foreach (var cube in AllCubelets)
        {
            cube.transform.SetParent(cubeletsParents);
        }

        RotatorParent.transform.localRotation = Quaternion.identity;
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
            
    }

    public void Rotate(Quaternion rotation)
    {
        var result = transform.localRotation * rotation;

        RotateTo(result);
    }

    public void RotateTo(Quaternion targetRotation)
    {
        if (IsRotating)
            return;

        IsRotating = true;

        transform.DORotateQuaternion(targetRotation, rotationDuration).OnComplete(CubeRotationCompleted);
    }

    private void CubeRotationCompleted()
    {
        IsRotating = false;

        SetupSides(true);
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
                    Rotate(Quaternion.AngleAxis(90f, Vector3.right));
                }
                if (GUILayout.Button("X-"))
                {
                    Rotate(Quaternion.AngleAxis(90f, Vector3.left));
                }
            }
            GUILayout.EndHorizontal();

            GUILayout.BeginHorizontal();
            {
                if (GUILayout.Button("Y+"))
                {
                    Rotate(Quaternion.AngleAxis(90f, Vector3.up));
                }
                if (GUILayout.Button("Y-"))
                {
                    Rotate(Quaternion.AngleAxis(90f, Vector3.down));
                }
            }
            GUILayout.EndHorizontal();

            GUILayout.BeginHorizontal();
            {
                if (GUILayout.Button("Z+"))
                {
                    Rotate(Quaternion.AngleAxis(90f, Vector3.forward));
                }
                if (GUILayout.Button("Z-"))
                {
                    Rotate(Quaternion.AngleAxis(90f, Vector3.back));
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