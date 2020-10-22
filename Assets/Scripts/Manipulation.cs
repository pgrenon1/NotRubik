public abstract class Manipulation
{
    public void TryExecute(Cube cube)
    {
        if (cube.CanBeManipulated)
        {
            Execute(cube);
        }
    }

    public virtual void Execute(Cube cube)
    {
        cube.RecordManipulation(this);
    }

    public abstract void Undo(Cube cube);
}