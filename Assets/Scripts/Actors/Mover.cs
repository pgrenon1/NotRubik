using DG.Tweening;
using Pathfinding;
using System.Collections.Generic;
using UnityEngine;

public class Mover : MonoBehaviour
{
    public PointNode CurrentNode { get; set; }
    public Seeker Seeker { get; private set; }
    public bool IsMoving { get; private set; }

    private PointGraph _pointGraph;
    public PointGraph PointGraph
    {
        get
        {
            if (_pointGraph == null)
                _pointGraph = GraphManager.Instance.PointGraph;

            return _pointGraph;
        }
    }

    public delegate void OnEndMovement();
    public event OnEndMovement EndMovement;

    private void Awake()
    {
        Seeker = GetComponent<Seeker>();
    }

    public void Move(PointNode targetNode)
    {
        var targetFacelet = GraphManager.Instance.GetFaceletForNode(targetNode);
        var currentFacelet = GraphManager.Instance.GetFaceletForNode(CurrentNode);

        var angle = Vector3.SignedAngle(-currentFacelet.transform.forward, -targetFacelet.transform.forward, transform.right);
        var rotation = Quaternion.AngleAxis(angle, transform.right);
        var targetRotation = rotation * transform.rotation;

        var targetPosition = (Vector3)targetNode.position;
        var projected = Vector3.Project(transform.position - targetPosition, -transform.up);
        DebugExtension.DebugPoint(projected, 1f, 0.8f);

        var sequence = DOTween.Sequence();

        // This could be bad, nothing makes it certain that the rotation is finished at the same exact time that movement is finished, 
        // and yet it's the DoMove that calls FinishMove().
        transform.DORotateQuaternion(targetRotation, 1f);

        transform.DOMove(targetPosition, 1f).OnComplete(FinishMove);

        CurrentNode = targetNode;
    }

    public void Move(Vector3 direction)
    {
        IsMoving = true;

        var nNInfoInternal = PointGraph.GetNearest(transform.position + direction);
        var targetNode = nNInfoInternal.node as PointNode;

        Move(targetNode);
    }

    private void FinishMove()
    {
        IsMoving = false;

        if (EndMovement != null)
            EndMovement();
    }

    public PointNode GetNodeAtDirection(Vector3 direction)
    {
        var nNInfoInternal = GraphManager.Instance.PointGraph.GetNearest(transform.position + direction);
        var targetNode = nNInfoInternal.node as PointNode;

        return targetNode;
    }

    public List<PointNode> GetOrthogonalNodes()
    {
        var directions = new List<Vector3>() { transform.forward, -transform.forward, transform.right, -transform.right };

        var orthogonalNodes = new List<PointNode>();

        foreach (var direction in directions)
        {
            orthogonalNodes.Add(GetNodeAtDirection(direction));
        }

        return orthogonalNodes;
    }

    public bool NodeIsOrthogonal(PointNode node)
    {
        var orthogonalNodes = GetOrthogonalNodes();

        return orthogonalNodes.Contains(node);
    }
}
