using System;
using System.Collections.Generic;
using UnityEngine;

public static class Extensions
{
    public static T RandomElement<T>(this List<T> list, bool remove = false)
    {
        var index = UnityEngine.Random.Range(0, list.Count);
        var result = list[index];

        if (remove)
        {
            list.RemoveAt(index);
        }

        return result;
    }
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

    public static Vector3 GetWorldDirectionForSide(Side side)
    {
        // I know this is wierd but it works so http://gph.is/1gT9SpQ
        switch (side)
        {
            case Side.Back:
                return Vector3.forward;
            case Side.Front:
                return Vector3.back;
            case Side.Right:
                return Vector3.right;
            case Side.Left:
                return Vector3.left;
            case Side.Up:
                return Vector3.down;
            case Side.Down:
                return Vector3.up;
            default:
                return Vector3.zero;
        }
    }

    public static Vector3 GetAbsoluteAxisForSide(Side side)
    {
        switch (side)
        {
            case Side.Back:
            case Side.Front:
                return Vector3.forward;
            case Side.Right:
            case Side.Left:
                return Vector3.right;
            case Side.Up:
            default:
                return Vector3.up;
        }
    }

    public static Vector3 GetAxisForRotationStep(RotationStep rotationStep)
    {
        var axis = GetAbsoluteAxisForSide(rotationStep.side);

        if (rotationStep.clockwise)
            return axis;
        else
            return -axis;
    }
}