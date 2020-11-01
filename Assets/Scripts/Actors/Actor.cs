using Pathfinding;
using System.Collections.Generic;
using UnityEngine;

public class Actor : MonoBehaviour
{
    public bool IsTakingTurn { get; private set; }
    public Mover Mover { get; private set; }

    // not sure what to name this, lmk.
    // @alvaro j'pense que par défaut, tous les effets devraient toujours affecter tous les actors.
    // mais Actor devrait avoir une liste d'immunities or something like that
    public bool CanBeAffectedByEffects { get; private set; }

    public virtual void Init(PointNode node)
    {
        Mover = GetComponent<Mover>();
        if (Mover)
        {
            Mover.CurrentNode = node;
        }

        TurnManager.Instance.RegisterActor(this);
    }

    public virtual void EndTurn()
    {
        Debug.Log(gameObject + " ends turn");
        IsTakingTurn = false;
    }

    public virtual void TakeTurn()
    {
        Debug.Log(gameObject + " takes turn");
        IsTakingTurn = true;
    }
}