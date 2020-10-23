using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurnManager : OdinserializedSingletonBehaviour<TurnManager>
{
    private void OnGUI()
    {
        GUILayout.BeginArea(new Rect(0, Screen.height - 60, 200, 60));
        if (GUILayout.Button("Pass Turn"))
        {
            EnemyManager.Instance.TakeEnemyTurns();
        }
        GUILayout.EndArea();
    }
}