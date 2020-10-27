using DG.Tweening;
using Pathfinding;
using UnityEngine;

public enum MovementBehaviour
{
    None,
    Forward,
}

[RequireComponent(typeof(Entity))]
public class Mover : MonoBehaviour
{
    public MovementBehaviour movementBehaviour;

    public Entity Entity { get; set; }
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

    public bool TryMove()
    {
        if (movementBehaviour == MovementBehaviour.Forward)
        {
            // check if can move

            Move(transform.forward);
        }
        else if (movementBehaviour == MovementBehaviour.None)
        {
            FinishMove();
        }

        return true;
    }

    public void Move(Vector3 direction)
    {
        IsMoving = true;

        var nNInfoInternal = PointGraph.GetNearest(transform.position + direction);
        var targetNode = nNInfoInternal.node as PointNode;

        var targetFacelet = GraphManager.Instance.GetFaceletForNode(targetNode);
        var currentFacelet = GraphManager.Instance.GetFaceletForNode(Entity.Node);

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

        Entity.Node = targetNode;
    }

    private void FinishMove()
    {
        IsMoving = false;

        if (EndMovement != null)
            EndMovement();
    }
}
