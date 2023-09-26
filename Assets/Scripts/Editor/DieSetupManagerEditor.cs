using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(DieSetup))]
public class DieSetupManagerEditor : Editor
{
    public override void OnInspectorGUI()
    {
        EditorGUILayout.HelpBox("To configure the die, assign the side number prefab and use the \"Create side number\" button to instantiate the desired number of side numbers. After that, you can navigate to them in a hierarchy to set them up individually.", MessageType.Info);

        base.OnInspectorGUI();

        DieSetup dieSetupManager = (DieSetup)target;
        if (GUILayout.Button("Create side number"))
        {
            dieSetupManager.CreateSideNumber();
        }
        if (GUILayout.Button("Remove all side numbers"))
        {
            dieSetupManager.RemoveAllSideNumbers();
        }
    }
}
