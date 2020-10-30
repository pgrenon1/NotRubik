using Pathfinding;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Actor, IMoveable
{
    public bool CanMove(PointNode pointNode)
    {
        return Mover.NodeIsOrthogonal(pointNode);
    }
}

public interface IMoveable
{
    bool CanMove(PointNode pointNode);
}