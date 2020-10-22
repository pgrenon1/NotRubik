using DG.Tweening;
using Pathfinding;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions.Must;

public enum MovementBehaviour
{
    Forward,
}

public class Mover : Entity
{
    public MovementBehaviour movementBehaviour;

    public Facelet Facelet { get; set; }

    public Seeker Seeker { get; private set; }
    public bool IsMoving { get; private set; }

    protected override void Awake()
    {
        base.Awake();

        Seeker = GetComponent<Seeker>();
    }

    private void Update()
    {
        if (!IsMoving)
            TryMove(transform.forward);
    }

    public bool TryMove(Vector3 direction)
    {
        IsMoving = true;

        var nNInfoInternal = PointGraph.GetNearest(transform.position + direction);
        var targetNode = nNInfoInternal.node;

        var targetFacelet = GraphManager.Instance.GetFaceletForNode(targetNode);

        //var angle = Vector3.SignedAngle(Facelet.transform.forward, targetFacelet.transform.forward, transform.right);
        var angle = Vector3.Angle(-Facelet.transform.forward, -targetFacelet.transform.forward);
        var rotation = Quaternion.AngleAxis(angle, transform.right);
        transform.rotation *= rotation;

        //var targetRotation = transform.rotation.eulerAngles;
        //targetRotation += rotation.eulerAngles;
        //transform.rotation *= rotation;

        transform.DOMove((Vector3)targetNode.position, 1f).OnComplete(MovementCompleted);
        //transform.DORotate(targetRotation, 1f);

        Facelet = targetFacelet;

        return true;
    }

    private void MovementCompleted()
    {
        IsMoving = false;
    }
}
