using Sirenix.OdinInspector;
using Sirenix.Utilities;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CreateAssetMenu]
public class Shuffle : ScriptableObject
{
    [InlineButton("UpdateSteps")]
    public List<SideRotation> rotationSteps = new List<SideRotation>();

    [InlineButton("UpdateStepsText")]
    public string stepsText;

    private void UpdateSteps()
    {
        var stepTexts = stepsText.Split(' ');

        rotationSteps = new List<SideRotation>();
        foreach (var stepText in stepTexts)
        {
            if (stepText.IsNullOrWhitespace())
                continue;

            var clockwise = !stepText.Contains("'");
            var letter = stepText.Replace("'", "")[0];
            rotationSteps.Add(new SideRotation(Utils.GetSideFromChar(letter), clockwise));

        }

        EditorUtility.SetDirty(this);
        AssetDatabase.SaveAssets();
    }

    private void UpdateStepsText()
    {
        stepsText = "";

        foreach (var step in rotationSteps)
        {
            var letter = Utils.GetLetterForSide(step);
            var sign = step.clockwise ? "" : "'";
            stepsText += letter + sign + " ";
        }

        EditorUtility.SetDirty(this);
        AssetDatabase.SaveAssets();
    }
}