using Pathfinding;
using UnityEngine;

public enum MovementBehaviour
{
    None,
    Forward,
}

public class Enemy : Actor, IMoveable
{
    public MovementBehaviour movementBehaviour;

    public override void TakeTurn()
    {
        base.TakeTurn();

        Mover.EndMovement += Mover_EndMovement;

        var direction = GetMovementDirection();

        Mover.Move(direction);
    }

    private void Mover_EndMovement()
    {
        EndTurn();
    }

    public bool CanMove(PointNode pointNode)
    {
        return true;
    }

    public Vector3 GetMovementDirection()
    {
        if (movementBehaviour == MovementBehaviour.Forward)
        {
            return transform.forward;
        }
        else
        {
            return Vector3.zero;
        }
    }
}
