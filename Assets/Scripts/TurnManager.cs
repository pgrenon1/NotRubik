using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurnManager : OdinserializedSingletonBehaviour<TurnManager>
{
    public Queue<Actor> Actors { get; set; } = new Queue<Actor>();

    private Coroutine _turnOrderCoroutine;

    public Actor CurrentActor { get { return Actors.Peek(); } }

    private void Start()
    {
        _turnOrderCoroutine = StartCoroutine(TurnOrder());
    }

    private IEnumerator TurnOrder()
    {
        while (true)
        {
            if (Actors.Count > 0)
            {
                var currentActor = Actors.Dequeue();

                currentActor.TakeTurn();

                yield return new WaitWhile(() => currentActor.IsTakingTurn);

                Actors.Enqueue(currentActor);
            }

            yield return null;
        }
    }    
}