using System;

[Serializable]
public class SideRotation : Manipulation
{
    public Side side;
    public bool clockwise;

    public SideRotation(Side side, bool clockwise)
    {
        this.side = side;
        this.clockwise = clockwise;
    }

    public override string ToString()
    {
        var clockwiseString = clockwise ? "" : "'";

        return Utils.GetLetterForSide(this).ToString() + clockwiseString;
    }

    public override void Execute(Cube cube)
    {
        base.Execute(cube);

        cube.RotateSide(this);
    }

    public override void Undo(Cube cube)
    {
        cube.RotateSide(new SideRotation(side, !clockwise));
    }
}
