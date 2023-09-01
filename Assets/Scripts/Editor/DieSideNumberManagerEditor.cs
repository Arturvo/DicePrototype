using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(DieSideNumberManager))]
public class DieSideNumberManagerEditor : Editor
{
    public override void OnInspectorGUI()
    {
        EditorGUILayout.HelpBox("To configure the side number, move this object close to a desired wall of the die and press the \"Snap to mesh\" button. Then continue to move and rotate the object to your liking. After that, modify the parameters below to set the side number to the desired value.", MessageType.Info);

        base.OnInspectorGUI();

        DieSideNumberManager dieSideNumberManager = (DieSideNumberManager)target;
        if (GUILayout.Button("Snap to mesh"))
        {
            dieSideNumberManager.SnapToMesh();
        }
    }
}
