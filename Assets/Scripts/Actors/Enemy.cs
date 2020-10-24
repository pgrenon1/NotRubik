public class Enemy : Entity
{
    public Mover Mover { get; private set; }

    public override void Init(Facelet facelet)
    {
        base.Init(facelet);

        Mover = GetComponent<Mover>();
        Mover.Entity = this;
    }

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
