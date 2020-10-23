using Pathfinding;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Entity : MonoBehaviour
{
    public Facelet Facelet { get; set; }
    public bool IsTakingTurn { get; private set; }

    public virtual void Init(Facelet facelet)
    {
        Facelet = facelet;
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
