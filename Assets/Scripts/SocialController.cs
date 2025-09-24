// SocialController.cs
// Small helpers for friends/relationships and invites.
using System;
using UnityEngine;
using Discord;

public class SocialController : MonoBehaviour
{
    public DiscordManagerAdvanced discordManager;
    private RelationshipManager relationshipManager;

    void Start()
    {
        // Example: get RelationshipManager via DiscordManagerAdvanced
        // Implementation detail depends on your DiscordManagerAdvanced exposing managers
    }

    public void PrintFriends()
    {
        Debug.Log("SocialController: Enumerating friends is SDK-dependent and may require permission.");
        // RelationshipManager APIs can be used here if available.
    }
}
