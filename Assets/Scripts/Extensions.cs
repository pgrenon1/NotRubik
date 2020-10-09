using System;
using UnityEngine;

public static class Extensions
{

}

public static class Util
{
    public static Side GetSideFromChar(char c)
    {
        foreach (Side side in Enum.GetValues(typeof(Side)))
        {
            if (Enum.GetName(typeof(Side), side)[0] == c)
                return side;
        }

        Debug.LogError("oh no, problem here");
        return Side.Back;
    }

    public static char GetLetterForSide(RotationStep step)
    {
        return Enum.GetName(typeof(Side), step.side)[0];
    }

    public static Vector3 GetAxisForSide(Side side)
    {
        switch (side)
        {
            case Side.Back:
                return Vector3.back;
            case Side.Front:
                return Vector3.forward;
            case Side.Right:
                return Vector3.right;
            case Side.Left:
                return Vector3.left;
            case Side.Up:
                return Vector3.up;
            default:
                return Vector3.down;
        }
    }
}