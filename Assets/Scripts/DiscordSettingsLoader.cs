// DiscordSettingsLoader.cs
using UnityEngine;

[System.Serializable]
public class DiscordAppSettings
{
    public long appId;
}

public static class DiscordSettingsLoader
{
    public static DiscordAppSettings Load()
    {
        var txt = Resources.Load<TextAsset>("discord_app_settings");
        if (txt == null) return null;
        return JsonUtility.FromJson<DiscordAppSettings>(txt.text);
    }
}
