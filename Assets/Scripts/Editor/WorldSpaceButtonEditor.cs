using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(WorldSpaceButton)), CanEditMultipleObjects]
class WorldSpaceButtonEditor : Editor
{
    protected virtual void OnSceneGUI()
    {
        WorldSpaceButton button = (WorldSpaceButton)target;

        Vector3 position = button.transform.position + Vector3.up * 2f;
        float size = 2f;
        float pickSize = size * 2f;

        if (Handles.Button(position, Quaternion.identity, size, pickSize, Handles.RectangleHandleCap))
            Debug.Log("The button was pressed!");
    }
}
