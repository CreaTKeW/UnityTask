using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIController : MonoBehaviour
{
    [SerializeField] private GameObject buttonPrefab;
    [SerializeField] private Transform buttonParent;
    [SerializeField] private GameObject playerPanel;
    [SerializeField] private GameObject shortPanel;
    private GameManager gameManager;

    void Start()
    {
        gameManager = FindObjectOfType<GameManager>();

        for (int i = 0; i < gameManager.NumberOfPlayers; i++)
        {
            CreateButtonForPlayer(i);
        }
        
        HidePlayersButton();
    }

    void CreateButtonForPlayer(int playerId)
    {
        GameObject buttonObj = Instantiate(buttonPrefab, buttonParent);
        buttonObj.name = "PlayerButton" + (playerId + 1);
        buttonObj.GetComponentInChildren<TextMeshProUGUI>().text = "Player " + (playerId + 1);

        Button button = buttonObj.GetComponent<Button>();
        button.onClick.AddListener(() => SetLeader(playerId));
    }

    void SetLeader(int playerId)
    {
        gameManager.SetLeader(playerId);
    }

    void HidePlayersButton()
    {
        GameObject hidePlayersButtonObj = Instantiate(buttonPrefab, buttonParent);
        hidePlayersButtonObj.name = "HidePlayersButton";
        hidePlayersButtonObj.GetComponentInChildren<TextMeshProUGUI>().text = "Hide Players";
        Button hidePlayersButton = hidePlayersButtonObj.GetComponent<Button>();
        hidePlayersButton.onClick.AddListener(TogglePlayerUI);
    }

    public void TogglePlayerUI()
    {
        playerPanel.SetActive(!playerPanel.activeSelf);
        shortPanel.SetActive(!shortPanel.activeSelf);
    }
    
}
