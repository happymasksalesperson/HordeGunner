using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(HealthComponent))]
public class HealthComponentEditor : Editor
{
    public override void OnInspectorGUI()
    {
        HealthComponent hp = (HealthComponent)target;

        DrawDefaultInspector();

        if (GUILayout.Button("Test Change Health"))
        {
            hp.TestChangeHealth();
        }
        
        if (GUILayout.Button("Rez"))
        {
            hp.Resurrect();
        }
        
        if (GUILayout.Button("Kill"))
        {
            hp.Kill();
        }
        
        if (GUI.changed)
        {
            serializedObject.ApplyModifiedProperties();
        }
    }
}