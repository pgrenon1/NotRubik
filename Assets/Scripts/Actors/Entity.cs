using Pathfinding;
using System.Collections;
using System.Collections.Generic;

public class Entity : Actor
{
    public Facelet Facelet { get; set; }
    public Mover Mover { get; private set; }

    public virtual void Init(Facelet facelet)
    {
        Facelet = facelet;

        Mover = GetComponent<Mover>();
        Mover.Entity = this;
    }
}
