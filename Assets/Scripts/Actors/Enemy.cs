﻿public class Enemy : Actor
{
    public override void TakeTurn()
    {
        base.TakeTurn();

        Mover.TryMove();

        Mover.EndMovement += Mover_EndMovement;
    }

    private void Mover_EndMovement()
    {
        EndTurn();
    }
}
