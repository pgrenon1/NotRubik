using Sirenix.OdinInspector;
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
    public int numberOfRecordedManipulations = 10;

    [BoxGroup("Debug"), PropertyOrder(1)]
    public Side debugSide;

    public List<Cubelet> AllCubelets { get; private set; } = new List<Cubelet>();
    public Dictionary<Side, List<Cubelet>> Sides { get; private set; } = new Dictionary<Side, List<Cubelet>>();
    public GameObject Rotator { get; private set; }
    public CubeDimensions Dimensions { get; private set; }

    private bool _isRotatingSide = false;
    private bool _isRotating = false;
    private bool _isShuffling = false;
    public bool CanBeManipulated { get { return !_isRotating && !_isRotatingSide; } }
    public List<Manipulation> Manipulations { get; private set; } = new List<Manipulation>();
    public GraphManager Graph { get; private set; }

    public Side SelectedSide { get; set; } = Side.None;

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

        var playerInputs = GetComponentInChildren<PlayerInputs>();
        playerInputs.Cube = this;
        playerInputs.CubeDebugMenu = GetComponent<CubeDebugMenu>();

        SetupSides();

        Graph = GetComponent<GraphManager>();
        Graph.Init(this);

        _isRotatingSide = false;
    }

    private void Update()
    {
        UpdateActiveSideVisuals();
    }

    private void UpdateActiveSideVisuals()
    {
        var worldDirection = Utils.GetWorldDirectionForSide(SelectedSide);

        foreach (var cubelet in AllCubelets)
        {
            var cubeletIsInSelectedSide = Sides.Count > 1 && Sides[SelectedSide].Contains(cubelet);

            if (cubeletIsInSelectedSide)
                Debug.DrawRay(cubelet.transform.position, worldDirection * 3f, Color.red);

            foreach (var facelet in cubelet.facelets)
            {
                var faceletIsOnActiveSide = CanBeManipulated &&
                                            SelectedSide != Side.None &&
                                            cubeletIsInSelectedSide &&
                                            facelet == cubelet.GetFaceletAtWorldDirection(worldDirection);
                facelet.highlight.SetActive(faceletIsOnActiveSide);
            }
        }
    }

    public void RotateSelectedSide(bool clockwise)
    {
        RotateSide(new SideRotation(SelectedSide, clockwise));
    }

    public void RotateSide(SideRotation rotationStep)
    {
        _isRotatingSide = true;

        GroupSide(rotationStep.side);

        var axis = Utils.GetAxisForRotationStep(rotationStep);

        Rotator.transform.DOBlendableRotateBy(axis * 90f, sideRotationSpeed).OnComplete(SideRotationCompleted);
    }

    private void SideRotationCompleted()
    {
        RoundCubeletsPositions();

        SetupSides();

        _isRotatingSide = false;

        GraphManager.Instance.NodeToFaceletCacheIsDirty = true;
    }

    private void RoundCubeletsPositions()
    {
        var widthIsEven = Dimensions.width % 2 == 0;
        var heightIsEven = Dimensions.height % 2 == 0;
        var depthIsEven = Dimensions.depth % 2 == 0;

        foreach (var cubelet in AllCubelets)
        {
            RoundCubeletPosition(cubelet, widthIsEven, heightIsEven, depthIsEven);
            // Dotween seems to have  inconsistency in retaining the scale of objects, so we make sure, after every side rotation, to reset the scale.
            // fuck me lol
            // <3
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
        foreach (KeyValuePair<Side, List<Cubelet>> entry in Sides)
        {
            var cubelets = entry.Value;
            var cubeletSide = entry.Key;

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

    public void SelectSide(Side sideToSelect)
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
        _isShuffling = true;

        StartCoroutine(DoShuffle(numberOfSteps));
    }

    public void Solve()
    {
        // lol
    }

    private IEnumerator DoShuffle(int numberOfSteps)
    {
        var sides = new List<Side>() { Side.Front, Side.Left, Side.Up };

        for (int i = 0; i < numberOfSteps; i++)
        {
            var sideRotation = new SideRotation(sides.RandomElement(), Random.Range(0f, 100f) > 50f);

            sideRotation.TryExecute(this);

            yield return new WaitWhile(() => !CanBeManipulated);
        }

        _isShuffling = false;
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
        _isRotating = true;

        cubeletsParents.DORotateQuaternion(targetRotation, rotationDuration).OnComplete(CubeRotationCompleted);
    }

    public void RotateBy(Vector3 rotation)
    {
        _isRotating = true;

        cubeletsParents.DOBlendableRotateBy(rotation, rotationDuration).OnComplete(CubeRotationCompleted);
    }

    private void CubeRotationCompleted()
    {
        RoundCubeletsRotation();

        SetupSides();

        _isRotating = false;
    }

    private void RoundCubeletsRotation()
    {
        var euler = cubeletsParents.rotation.eulerAngles;

        euler.x = Mathf.LerpAngle(euler.x, Mathf.Round(euler.x / 90f) * 90f, 1f);
        euler.y = Mathf.LerpAngle(euler.y, Mathf.Round(euler.y / 90f) * 90f, 1f);
        euler.z = Mathf.LerpAngle(euler.z, Mathf.Round(euler.z / 90f) * 90f, 1f);

        cubeletsParents.eulerAngles = euler;
    }

    public void Undo()
    {
        if (Manipulations.Count <= 0 || !CanBeManipulated)
            return;

        var lastManipulation = Manipulations[Manipulations.Count - 1];
        lastManipulation.Undo(this);

        Manipulations.Remove(lastManipulation);
    }

    public void RecordManipulation(Manipulation manipulation)
    {
        if (Manipulations.Count >= numberOfRecordedManipulations)
            Manipulations.RemoveAt(0);

        Manipulations.Add(manipulation);
    }
}