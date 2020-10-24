using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurnManager : OdinserializedSingletonBehaviour<TurnManager>
{
    public Queue<Actor> Actors { get; set; } = new Queue<Actor>();

    

    private void Start()
    {
        StartCoroutine(TurnOrder());
    }

    private IEnumerator TurnOrder()
    {
        while (true)
        {
            if (Actors.Count > 1)
            {
                var currentActor = Actors.Dequeue();

                currentActor.TakeTurn();

                yield return new WaitWhile(() => currentActor.IsTakingTurn);

                Actors.Enqueue(currentActor);
            }
        }
    }

    public void PassTurn()
    {

    }
}