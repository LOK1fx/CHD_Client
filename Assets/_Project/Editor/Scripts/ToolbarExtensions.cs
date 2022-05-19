using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityToolbarExtender;

[InitializeOnLoad()]
public static class ToolbarExtensions
{
    static ToolbarExtensions()
    {
        ToolbarExtender.LeftToolbarGUI.Add(DrawLeftGUI);
        ToolbarExtender.RightToolbarGUI.Add(DrawRightGUI);
    }

    private static void DrawLeftGUI()
    {
        GUILayout.FlexibleSpace();

        if (GUILayout.Button(new GUIContent("Navigate the App", "Navigate the app object")))
        {
            Selection.activeObject = AssetDatabase.LoadMainAssetAtPath(Constants.Editor.APP_PATH);
        }
    }

    private static void DrawRightGUI()
    {

    }
}