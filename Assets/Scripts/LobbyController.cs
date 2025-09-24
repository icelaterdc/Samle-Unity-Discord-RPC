// LobbyController.cs
// Example wrappers around LobbyManager: create, update, connect, leave.
using System;
using UnityEngine;
using Discord;

public class LobbyController : MonoBehaviour
{
    public DiscordManagerAdvanced discordManager;
    private LobbyManager lobbyManager;
    private long currentLobbyId = 0;
    private string currentSecret = null;

    void Start()
    {
        if (discordManager == null)
            discordManager = FindObjectOfType<DiscordManagerAdvanced>();

        if (discordManager != null && discordManagerApplicationReady())
        {
            lobbyManager = discordManagerApplicationGetLobbyManager();
        }
    }

    private bool discordManagerApplicationReady()
    {
        // Simple null check
        return discordManager != null && discordManager.gameObject != null;
    }

    private LobbyManager discordManagerApplicationGetLobbyManager()
    {
        // Access via reflection or public property in DiscordManagerAdvanced in a real project.
        // For this sample assume a public property would exist.
        return discordManager == null ? null : discordManager.gameObject.GetComponent<DiscordManagerAdvanced>()?.GetType()?.GetProperty("lobbyManager")?.GetValue(discordManager) as LobbyManager;
    }

    public void CreateLobbyExample()
    {
        if (lobbyManager == null)
        {
            Debug.LogWarning("LobbyManager not available");
            return;
        }

        var txn = lobbyManager.GetCreateLobbyTransaction();
        txn.SetCapacity(4);
        txn.SetType(Discord.LobbyType.Public);

        lobbyManager.CreateLobby(txn, (Result result, ref Lobby lobby) =>
        {
            if (result == Result.Ok)
            {
                currentLobbyId = lobby.Id;
                currentSecret = lobby.Secret;
                Debug.Log($"Lobby created: {lobby.Id} secret={lobby.Secret}");
                // Update presence to include lobby secret so friends can join
            }
            else
            {
                Debug.LogWarning($"CreateLobby failed: {result}");
            }
        });
    }

    public void ConnectToLobby(long lobbyId, string secret)
    {
        if (lobbyManager == null) return;
        lobbyManager.ConnectLobby(lobbyId, secret, (Result res, ref Lobby lobby) =>
        {
            if (res == Result.Ok)
            {
                Debug.Log($"Connected to lobby {lobby.Id}");
            }
            else
            {
                Debug.LogWarning($"ConnectLobby failed: {res}");
            }
        });
    }

    public void LeaveLobby()
    {
        if (lobbyManager == null || currentLobbyId == 0) return;
        lobbyManager.DisconnectLobby(currentLobbyId, (Result res) =>
        {
            Debug.Log($"Left lobby result: {res}");
            currentLobbyId = 0;
            currentSecret = null;
        });
    }
}
