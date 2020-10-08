using Sirenix.OdinInspector;
using Sirenix.Utilities;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class Shuffle : ScriptableObject
{
    [InlineButton("UpdateSteps")]
    public List<RotationStep> rotationSteps = new List<RotationStep>();

    [InlineButton("UpdateStepsText")]
    public string stepsText;

    private void UpdateSteps()
    {
        var stepTexts = stepsText.Split(' ');

        rotationSteps = new List<RotationStep>();
        foreach (var stepText in stepTexts)
        {
            if (stepText.IsNullOrWhitespace())
                continue;

            var clockwise = !stepText.Contains("'");
            var letter = stepText.Replace("'", "")[0];
            rotationSteps.Add(new RotationStep(GetSideFromChar(letter), clockwise));
        }
    }

    private void UpdateStepsText()
    {
        stepsText = "";

        foreach (var step in rotationSteps)
        {
            var letter = GetLetterForSide(step);
            var sign = step.clockwise ? "" : "'";
            stepsText += letter + sign + " ";
        }
    }

    private static Side GetSideFromChar(char c)
    {
        foreach (Side side in Enum.GetValues(typeof(Side)))
        {
            if (Enum.GetName(typeof(Side), side)[0] == c)
                return side;
        }

        Debug.LogError("oh no, problem here");
        return Side.Back;
    }

    private static char GetLetterForSide(RotationStep step)
    {
        return Enum.GetName(typeof(Side), step.side)[0];
        //switch (step.side)
        //{
        //    case Side.Back:
        //        return "B";
        //    case Side.Front:
        //        return "F";
        //    case Side.Right:
        //        return "R";
        //    case Side.Left:
        //        return "L";
        //    case Side.Up:
        //        return "U";
        //    default:
        //        return "D";
        //}
    }
}

[Serializable]
public class RotationStep
{
    public Side side;
    public bool clockwise;
    public RotationStep(Side side, bool clockwise)
    {
        this.side = side;
        this.clockwise = clockwise;
    }
}
