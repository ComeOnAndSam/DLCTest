using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(LoadPlaybackHand))]
public class LoadPlaybackHandEditor : Editor
{
    public override void OnInspectorGUI()
    {
        LoadPlaybackHand myScript = (LoadPlaybackHand)target;

        if (GUILayout.Button("Toggle Playback"))
        {
            myScript.TogglePlayback();
        }

        DrawDefaultInspector();
    }
}
