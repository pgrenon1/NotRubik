using Pathfinding;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Mover))]
public class Player : Actor
{
    public bool NodeIsOrthogonal(PointNode node)
    {
        var orthogonalNodes = GetOrthogonalNodes();

        return orthogonalNodes.Contains(node);
    }
}