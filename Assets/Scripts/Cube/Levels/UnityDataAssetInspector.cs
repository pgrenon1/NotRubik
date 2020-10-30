using UnityEngine;
using UnityEditor;



[CustomEditor(typeof(LevelDataAsset))]
public class LevelDataAssetEditor : Editor
{


    private float rectSize = 150;
    private float startX = 30;
    private float startY = 125;
    private Vector3 currentPosition;


    private Vector2 mousePosition;


    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();
        LevelDataAsset targetAsset = (LevelDataAsset)target;


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

        Rect rect = GUILayoutUtility.GetRect(10, 10000, 200,900);
        GUI.Label(new Rect(10, 200, 100, 30), "Cube editor");
      
        currentPosition.y = startY;

        if (Event.current.type == EventType.Repaint)


        {

            GUI.BeginClip(rect);
            //This is the Slider that changes the size of the Rectangle drawn

            for (int i = 0; i <= 11; i++)
            {
                if (i % 3 == 0 && i <= 11)
                {
                    currentPosition.y += rectSize;
                    currentPosition.x = startX;
                }

                TryDrawFace(currentPosition.x, currentPosition.y, i);

                currentPosition.x += rectSize;
            };

            GUI.EndClip();
        }





    }



    private void TryDrawFace(float x, float y, int faceNumber)
    {
        switch (faceNumber)
        {
            case 0: break;
            case 1: DrawFace(x, y, Color.white); break;
            case 2: break;
            case 3: DrawFace(x, y, Color.magenta); break;
            case 4: DrawFace(x, y, Color.green); break;
            case 5: DrawFace(x, y, Color.red); break;
            case 6: break;
            case 7: DrawFace(x, y, Color.yellow); break;
            case 8: break;
            case 9: break;
            case 10: DrawFace(x, y, Color.blue); break;
            case 11: break;
            default: break;



        }
    }

    private void DrawFace(float x, float y, Color color)
    {




        //  GUI.Label(new Rect(x, y, 100, 100), "Y");
        //70, -50
        EditorGUI.DrawRect(new Rect(x, y, rectSize, rectSize), color);
        DrawFacelet(x, y);



    }


    private void DrawFacelet(float x, float y)
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

            if (GUI.Button(new Rect(x + currentX, y + currentY, buttonSize - 4, buttonSize - 4), i.ToString()))
            {
                Debug.Log(i.ToString());
            }



            currentX += buttonSize;



        }
    }
}