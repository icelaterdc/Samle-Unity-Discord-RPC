// DiscordEditorSettings.cs
// Editor window to input the Discord App ID and write a small Resources file that runtime reads.
#if UNITY_EDITOR
using UnityEngine;
using UnityEditor;
using System.IO;

public class DiscordEditorSettings : EditorWindow
{
    private string appId = "0";
    private const string savePath = "Assets/Resources/discord_app_settings.json";

    [MenuItem("Window/Discord SDK Settings")]
    public static void ShowWindow()
    {
        GetWindow<DiscordEditorSettings>("Discord SDK Settings");
    }

    void OnGUI()
    {
        GUILayout.Label("Discord SDK Settings", EditorStyles.boldLabel);
        appId = EditorGUILayout.TextField("App ID", appId);

        if (GUILayout.Button("Save App ID to Resources"))
        {
            SaveAppId();
        }
    }

    void SaveAppId()
    {
        if (!Directory.Exists("Assets/Resources")) Directory.CreateDirectory("Assets/Resources");
        var json = "{\"appId\":" + appId + "}";
        File.WriteAllText(savePath, json);
        AssetDatabase.Refresh();
        Debug.Log("Saved Discord App ID to " + savePath);
    }
}
#endif
