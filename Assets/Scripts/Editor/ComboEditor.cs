using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(ComboTracker))]
public class ComboEditor : Editor
{
    public override void OnInspectorGUI()
    {
        ComboTracker s = (ComboTracker)target;

        DrawDefaultInspector();

        if (GUILayout.Button("Increase Combo"))
        {
            s.IncreaseCombo();
        }

        if (GUI.changed)
        {
            serializedObject.ApplyModifiedProperties();
        }
    }
}