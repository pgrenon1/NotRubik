using UnityEngine;

public class CubeRotation : Manipulation
{
    public Vector3 axis;

    public CubeRotation(Vector3 axis)
    {
        this.axis = axis;
    }

    protected override void Execute(Cube cube, Player player)
    {
        base.Execute(cube, player);

        cube.RotateBy(axis);
    }

    public override void Undo(Cube cube)
    {
        cube.RotateBy(-axis);
    }
}