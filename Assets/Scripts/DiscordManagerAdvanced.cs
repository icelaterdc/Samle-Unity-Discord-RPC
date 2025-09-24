// DiscordManagerAdvanced.cs
// More featureful Discord Game SDK integration for Unity.
// This example shows initialization, activity updates, activity callbacks,
// an internal queue to avoid rapid UpdateActivity calls, and basic lobby integration hooks.

using System;
using System.Collections.Generic;
using UnityEngine;
using Discord;

public class DiscordManagerAdvanced : MonoBehaviour
{
    [Tooltip("Discord App ID (set in inspector or via Editor Settings)")]
    public long appId = 0;

    public bool autoInitialize = true;
    public int activityQueueLimit = 8;

    private Discord.Discord discord;
    private ActivityManager activityManager;
    private LobbyManager lobbyManager;
    private UserManager userManager;

    private bool initialized = false;
    private Queue<Activity> activityQueue = new Queue<Activity>();

    // Example fields
    public string details = "Playing Advanced Sample";
    public string state = "Idle";
    public string largeImageKey = "large_image";
    public string smallImageKey = "small_image";
    public int partySize = 1;
    public int partyMax = 4;

    // Reconnection/backoff
    private int reconnectAttempts = 0;
    private const int maxReconnectAttempts = 5;

    void Start()
    {
        if (autoInitialize)
            Initialize();
    }

    public void Initialize()
    {
        if (initialized) return;
        if (appId == 0)
        {
            Debug.LogError("DiscordManagerAdvanced: appId is 0. Set your App ID.");
            return;
        }

        try
        {
            discord = new Discord.Discord((ulong)appId, (System.UInt64)Discord.CreateFlags.Default);
            activityManager = discord.GetActivityManager();
            lobbyManager = discord.GetLobbyManager();
            userManager = discord.GetUserManager();

            // Register activity event handlers
            activityManager.OnActivityJoin += OnActivityJoin;
            activityManager.OnActivitySpectate += OnActivitySpectate;
            activityManager.OnActivityInvite += OnActivityInvite;

            // initial presence
            EnqueueActivity(BuildActivity());

            initialized = true;
            Debug.Log("DiscordManagerAdvanced: Initialized");
        }
        catch (Exception ex)
        {
            Debug.LogError("DiscordManagerAdvanced: Init error: " + ex);
        }
    }

    void Update()
    {
        if (!initialized || discord == null) return;

        // Pump callbacks
        discord.RunCallbacks();

        // Process activity queue (simple rate-limited drain)
        if (activityQueue.Count > 0)
        {
            var activity = activityQueue.Dequeue();
            try
            {
                activityManager.UpdateActivity(activity, result =>
                {
                    if (result == Result.Ok)
                        Debug.Log("Activity updated");
                    else
                        Debug.LogWarning($"UpdateActivity failed: {result}");
                });
            }
            catch (Exception ex)
            {
                Debug.LogError("Exception updating activity: " + ex);
            }
        }
    }

    public Activity BuildActivity()
    {
        var act = new Activity
        {
            Details = details,
            State = state,
            Assets =
            {
                LargeImage = largeImageKey,
                SmallImage = smallImageKey
            },
            Party =
            {
                Id = Guid.NewGuid().ToString(),
                Size =
                {
                    CurrentSize = partySize,
                    MaxSize = partyMax
                }
            },
            Instance = true,
            Timestamps =
            {
                Start = DateTimeOffset.UtcNow.ToUnixTimeSeconds()
            }
        };
        return act;
    }

    public void EnqueueActivity(Activity a)
    {
        if (activityQueue.Count >= activityQueueLimit)
        {
            // drop oldest
            activityQueue.Dequeue();
        }
        activityQueue.Enqueue(a);
    }

    public void UpdatePresenceNow()
    {
        EnqueueActivity(BuildActivity());
    }

    // Activity callbacks
    private void OnActivityJoin(string secret)
    {
        Debug.Log("OnActivityJoin: " + secret);
        // Game-specific: parse secret and perform join flow
        // Example: trigger matchmaking or direct connect
    }

    private void OnActivitySpectate(string secret)
    {
        Debug.Log("OnActivitySpectate: " + secret);
        // Game-specific spectate flow
    }

    private void OnActivityInvite(ref ActivityActionType type, ref User user)
    {
        Debug.Log($"OnActivityInvite: type={type} from user {user.Username}#{user.Discriminator}");
        // Show an in-game invite UI or accept automatically for testing
    }

    void OnApplicationQuit()
    {
        Dispose();
    }

    public void Dispose()
    {
        if (discord == null) return;

        try
        {
            // unregister
            if (activityManager != null)
            {
                activityManager.OnActivityJoin -= OnActivityJoin;
                activityManager.OnActivitySpectate -= OnActivitySpectate;
                activityManager.OnActivityInvite -= OnActivityInvite;
            }

            discord.Dispose();
        }
        catch (Exception ex)
        {
            Debug.LogError("Error disposing Discord: " + ex);
        }
        finally
        {
            discord = null;
            initialized = false;
        }
    }
}
