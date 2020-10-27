using Pathfinding;
using UnityEngine;

public class Actor : MonoBehaviour
{
    public PointNode Node { get; set; }
    public bool IsTakingTurn { get; private set; }
    public Mover Mover { get; private set; }


    // not sure what to name this, lmk.
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
}