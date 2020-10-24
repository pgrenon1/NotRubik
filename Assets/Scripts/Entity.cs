using Pathfinding;
using System.Collections;
using System.Collections.Generic;
public class Entity : Actor
{
    public Facelet Facelet { get; set; }

    public virtual void Init(Facelet facelet)
    {
        Facelet = facelet;
    }
}
