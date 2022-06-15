using UnityEditor;
using UnityEngine;
using UnityToolbarExtender;

[InitializeOnLoad()]
public static class ToolbarExtensions
{
    #region Textes

    private const string NAV_APP_TEXT = "Navigate the App";
    private const string NAV_APP_TOOLTIP = "Navigate the app object";

    private const string NAV_LEVEL_DB_TEXT = "Navigate the Levels DB";
    private const string NAV_LEVEL_DB_TOOLTIP = "Navigate the levels database";

    #endregion

    static ToolbarExtensions()
    {
        ToolbarExtender.LeftToolbarGUI.Add(DrawLeftGUI);
        ToolbarExtender.RightToolbarGUI.Add(DrawRightGUI);
    }

    private static void DrawLeftGUI()
    {
        GUILayout.FlexibleSpace();

        DrawNavAppButton();
        DrawNavLevelsDatabaseButton();
    }

    private static void DrawRightGUI()
    {

    }

    private static void DrawNavAppButton()
    {
        if (GUILayout.Button(new GUIContent(NAV_APP_TEXT, NAV_APP_TOOLTIP)))
        {
            Selection.activeObject = AssetDatabase.LoadMainAssetAtPath(Constants.Editor.APP_PATH);
        }
    }

    private static void DrawNavLevelsDatabaseButton()
    {
        if (GUILayout.Button(new GUIContent(NAV_LEVEL_DB_TEXT, NAV_LEVEL_DB_TOOLTIP)))
        {
            Selection.activeObject = AssetDatabase.LoadMainAssetAtPath(Constants.Editor.LEVEL_DB_PATH);
        }
    }
}