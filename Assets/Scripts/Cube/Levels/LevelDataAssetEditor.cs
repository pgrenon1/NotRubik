using UnityEngine;
using UnityEditor;

using System.Collections.Generic;


[CustomEditor(typeof(LevelDataAsset))]
public class LevelDataAssetEditor : Editor
{


    private float rectSize = 200;
    private float startX = 30;
    private float startY = 0;
    private Vector2 currentPosition;

    LevelDataAsset targetAsset;

    public bool initialized;


    int _selected = 0;
    string[] _options = new string[] { "Test1", "Test2", "Test3" };

    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();
        
        targetAsset = (LevelDataAsset)target;
        EditorUtility.SetDirty(targetAsset);

        DrawUnwrappedCube();
    }

    

    private void DrawUnwrappedCube()
    {
        // An unwrapped cube is always drawn as such
        //    ---XXX---
        //    XXXXXXXXX
        //    ---XXX---
        //    ---XXX---

        //*Will need to refactor is we ever go with irregular sizes(doubt it)


        Rect rect = GUILayoutUtility.GetRect(0, 10000, 0, 1200);
        currentPosition.y = startY;


       
        GUI.BeginClip(rect);

        for (int i = 0; i <= 11; i++)
        {
            if (i % 3 == 0 && i <= 11)
            {
                currentPosition.y += rectSize;
                currentPosition.x = startX;
            }

            TryDrawFace(currentPosition.x, currentPosition.y, i);

            currentPosition.x += rectSize;
        }

        GUI.EndClip();
    }



    private void TryDrawFace(float x, float y, int faceNumber)
    {
        switch (faceNumber)
        {
            case 0: break;
            case 1: DrawFace(x, y, Color.white, Side.Back); break;
            case 2: break;
            case 3: DrawFace(x, y, Color.magenta, Side.Left); break;
            case 4: DrawFace(x, y, Color.green, Side.Up); break;
            case 5: DrawFace(x, y, Color.red, Side.Right); break;
            case 6: break;
            case 7: DrawFace(x, y, Color.yellow, Side.Front); break;
            case 8: break;
            case 9: break;
            case 10: DrawFace(x, y, Color.blue, Side.Down); break;
            case 11: break;
            default: break;
        }
    }

    private void DrawFace(float x, float y, Color color, Side side)
    {
        Rect faceRect = new Rect(x, y, rectSize, rectSize);
        EditorGUI.DrawRect(faceRect, color);




        DrawFacelet(x, y, faceRect, side);
    }


    private void DrawFacelet(float x, float y, Rect faceRect, Side side)
    {
        var buttonSize = rectSize / 3;
        var currentX = 2f;
        var currentY = -buttonSize + 2;

        for (int i = 0; i <= 8; i++)
        {
            if (i % 3 == 0)
            {
                currentY += buttonSize;
                currentX = 2;
            }

            DrawFaceletButton(x + currentX, y + currentY, buttonSize, side, i);
            currentX += buttonSize;
        }
    }

    void DrawFaceletButton(float x, float y, float buttonSize, Side side, int buttonIndex)
    {
        Rect buttonRect = new Rect(x, y, buttonSize - 4, buttonSize - 4);



           GUILayout.BeginArea(buttonRect);


        EditorGUI.BeginChangeCheck();
        _selected = EditorGUILayout.Popup("", _selected, _options);
       // var tempTileData = new TileData();
       //     GUI.Button(buttonRect, "None");


            GUILayout.EndArea();


    }

    

}

