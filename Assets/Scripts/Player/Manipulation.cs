public abstract class Manipulation
{
    public void TryExecute(Cube cube, Player player = null)
    {
        if (player != null) {
            if (!player.CanExecuteManipulation(this))
                return;
        }

        if (!cube.CanBeManipulated)
            return;
        
        Execute(cube, player);
    }

    protected virtual void Execute(Cube cube, Player player = null)
    {
        cube.RecordManipulation(this);

        if (player != null)
            player.ConsumeManipulation();
    }

    public abstract void Undo(Cube cube);
}