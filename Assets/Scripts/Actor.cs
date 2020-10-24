using UnityEngine;

public class Actor : MonoBehaviour
{
    public bool IsTakingTurn { get; private set; }

    public virtual void EndTurn()
    {
        IsTakingTurn = false;
    }

    public virtual void TakeTurn()
    {
        IsTakingTurn = true;
    }
}