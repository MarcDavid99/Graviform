using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(AttributeExamples))]
public class ScriptEditor : Editor {
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();
        if (GUILayout.Button("Do something"))
        {
            Debug.Log("Pressed");
        }
    }
}
