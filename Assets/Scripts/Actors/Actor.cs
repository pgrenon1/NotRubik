using Pathfinding;
using System.Collections.Generic;
using UnityEngine;

public class Actor : MonoBehaviour
{
    public PointNode Node { get; set; }
    public bool IsTakingTurn { get; private set; }
    public Mover Mover { get; private set; }
    // not sure what to name this, lmk.
    // @alvaro j'pense que par défaut, tous les effets devraient toujours affecter tous les actors.
    // mais Actor devrait avoir une liste d'immunities or something like that
    public bool CanBeAffectedByEffects { get; private set; }

    public virtual void Init(PointNode node)
    {
        Node = node;

        Mover = GetComponent<Mover>();
        Mover.Actor = this;

        TurnManager.Instance.AddActor(this);
    }

    public virtual void EndTurn()
    {
        IsTakingTurn = false;
    }

    public virtual void TakeTurn()
    {
        IsTakingTurn = true;
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
}