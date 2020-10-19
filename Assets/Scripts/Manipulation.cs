public abstract class Manipulation
{
    public virtual void Execute(Cube cube)
    {
        cube.RecordManipulation(this);
    }

    public abstract void Undo(Cube cube);
}