// ExampleUI.cs
using UnityEngine;
using UnityEngine.UI;

public class ExampleUI : MonoBehaviour
{
    public DiscordManagerAdvanced discordManager;
    public InputField detailsInput;
    public InputField stateInput;
    public Button updateButton;
    public Button createLobbyButton;

    void Start()
    {
        if (updateButton != null)
            updateButton.onClick.AddListener(OnUpdateClicked);
        if (createLobbyButton != null)
            createLobbyButton.onClick.AddListener(OnCreateLobbyClicked);
    }

    void OnUpdateClicked()
    {
        if (discordManager == null) return;
        if (detailsInput != null) discordManager.details = detailsInput.text;
        if (stateInput != null) discordManager.state = stateInput.text;
        discordManager.UpdatePresenceNow();
    }

    void OnCreateLobbyClicked()
    {
        var lc = FindObjectOfType<LobbyController>();
        if (lc != null) lc.CreateLobbyExample();
    }
}
