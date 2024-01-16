using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIController : MonoBehaviour
{
    [SerializeField] private GameObject buttonPrefab;
    [SerializeField] private Transform buttonParent;
    [SerializeField] private GameObject playerPanel;
    [SerializeField] private GameObject shortPanel;
    [SerializeField] private GameManager gameManager;

    void Start()
    {
        if (gameManager == null)
        {
            Debug.LogError("GameManager not found in the scene.");
            return;
        }

        InitializePlayerButtons();
        InitializeUIControlButton();
    }

    // Creates buttons for each player
    private void InitializePlayerButtons()
    {
        for (int i = 0; i < gameManager.NumberOfPlayers; i++)
        {
            CreateButtonForPlayer(i);
        }
    }

    private void CreateButtonForPlayer(int playerId)
    {
        if (buttonPrefab == null || buttonParent == null) return;

        // Instantiate the button and set its properties
        GameObject buttonObj = Instantiate(buttonPrefab, buttonParent);
        buttonObj.name = "PlayerButton" + (playerId + 1);
        buttonObj.GetComponentInChildren<TextMeshProUGUI>().text = "Player " + (playerId + 1);

        Button button = buttonObj.GetComponent<Button>();
        button.onClick.AddListener(() => SetLeader(playerId));
    }

    private void SetLeader(int playerId)
    {
        gameManager.SetLeader(playerId);
    }

    private void InitializeUIControlButton()
    {
        if (buttonPrefab == null || buttonParent == null) return;

        // Instantiate the hide button and set its properties
        GameObject hidePlayersButtonObj = Instantiate(buttonPrefab, buttonParent);
        hidePlayersButtonObj.name = "HidePlayersButton";
        hidePlayersButtonObj.GetComponentInChildren<TextMeshProUGUI>().text = "Hide Players";

        Button hidePlayersButton = hidePlayersButtonObj.GetComponent<Button>();
        hidePlayersButton.onClick.AddListener(TogglePlayerUI);
    }

    // Toggles the visibility of player panels
    public void TogglePlayerUI()
    {
        playerPanel.SetActive(!playerPanel.activeSelf);
        shortPanel.SetActive(!shortPanel.activeSelf);
    }
}