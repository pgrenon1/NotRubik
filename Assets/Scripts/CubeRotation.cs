using UnityEngine;

public class CubeRotation : Manipulation
{
    public Vector3 axis;

    public CubeRotation(Vector3 axis)
    {
        this.axis = axis;
    }

    public override void Execute(Cube cube)
    {
        base.Execute(cube);
        cube.RotateBy(axis);
    }

    public override void Undo(Cube cube)
    {
        cube.RotateBy(-axis);
    }
}
