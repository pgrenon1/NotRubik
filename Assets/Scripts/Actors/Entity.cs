using Pathfinding;
using System.Collections;
using System.Collections.Generic;

public class Entity : Actor
{
    public PointNode Node { get; set; }
    public Mover Mover { get; private set; }

    public virtual void Init(PointNode node)
    {
        Node = node;

        Mover = GetComponent<Mover>();
        Mover.Entity = this;
    }
}
