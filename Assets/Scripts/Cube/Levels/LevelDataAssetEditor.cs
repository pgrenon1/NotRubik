using UnityEngine;
using UnityEditor;
using System.Collections.Generic;

[CustomEditor(typeof(LevelDataAsset))]
public class LevelDataAssetEditor : Editor
{

    const float RECTSIZE = 200;
    const float START_X = 30;
    const int SIDES = 6;
    const float START_Y = 0;
    const int GRIDSIZE = 11;
    const int TILEDATASIZE = 9;

    private Vector2 currentDrawingPosition;
    Rect inspectorDrawZoneRectangle;

    LevelDataAsset targetAsset;
    
    public bool initialized;

    public override void OnInspectorGUI()
    {

        base.OnInspectorGUI();
        inspectorDrawZoneRectangle = GUILayoutUtility.GetRect(0, 1000, 0, 1200);

        targetAsset = (LevelDataAsset)target;

        if (targetAsset.levelTiles.Count == SIDES) 
            DrawUnwrappedCube();
        else
            DrawInitializer();

        EditorUtility.SetDirty(targetAsset);
    }


    private void DrawInitializer()
    {
        GUI.BeginClip(inspectorDrawZoneRectangle);
        {
            if (GUI.Button(new Rect(START_X, START_Y, 500, 30), "Initialize LevelData"))
            {
                Debug.Log("Initializing level data");
                InitializeLevelData();
            }

        }
        GUI.EndClip();
    }


    private void InitializeLevelData()
    {     
        for (int i = 1; i <= SIDES; i++)
        {
            var tempSideTiles = new SideTiles();
            
            if (!targetAsset.levelTiles.ContainsKey((Side)i))
            {
                targetAsset.levelTiles.Add((Side)i, tempSideTiles);
                targetAsset.levelTiles[(Side)i].tileData = new TileData[TILEDATASIZE];
               
            }
            else
            {
                targetAsset.levelTiles[(Side)i].tileData = new TileData[TILEDATASIZE];
            }
        }
    }


    private void DrawUnwrappedCube()
    {
        // An unwrapped cube is always drawn as such 
        //    ---XXX---
        //    XXXXXXXXX
        //    ---XXX---
        //    ---XXX---

        //*Will need to refactor is we ever go with irregular sizes(doubt it)

        currentDrawingPosition.y = START_Y;


        GUI.BeginClip(inspectorDrawZoneRectangle);
        {
            if (GUI.Button(new Rect(START_X, START_Y, 500, 30), "Reinitialize LevelData"))
            {
                InitializeLevelData();
                Debug.Log("Reset all level data");
            }


            for (int i = 0; i <= GRIDSIZE; i++)
            {
                if (i % 3 == 0 && i <= GRIDSIZE)
                {
                    currentDrawingPosition.y += RECTSIZE;
                    currentDrawingPosition.x = START_X;
                }

                TryDrawFace(currentDrawingPosition.x, currentDrawingPosition.y, i);

                currentDrawingPosition.x += RECTSIZE;
            }
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
        Rect faceRect = new Rect(x, y, RECTSIZE, RECTSIZE);
        EditorGUI.DrawRect(faceRect, color);

        DrawFacelet(x, y, faceRect, side);
    }


    private void DrawFacelet(float x, float y, Rect faceRect, Side side)
    {
        var buttonSize = RECTSIZE / 3;
        var currentX = 2f;
        var currentY = -buttonSize + 2;

        for (int i = 0; i < TILEDATASIZE; i++)
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

        var buttonOffset = 4;
        Rect buttonRect = new Rect(x, y, buttonSize - buttonOffset, buttonSize - buttonOffset);

        GUILayout.BeginArea(buttonRect);
        {
            if (targetAsset.levelTiles.ContainsKey(side))
                targetAsset.levelTiles[side].tileData[buttonIndex] = EditorGUILayout.ObjectField(targetAsset.levelTiles[side].tileData[buttonIndex], typeof(TileData), false) as TileData;
            else
                targetAsset.levelTiles.Add(side, null);

        }
        GUILayout.EndArea();

    }

    

}

