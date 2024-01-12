using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIController : MonoBehaviour
{
    [SerializeField] private GameObject buttonPrefab;
    [SerializeField] private Transform buttonParrent;
    private GameManager gameManager;

    void Start()
    {
        gameManager = FindObjectOfType<GameManager>();

        for (int i = 0; i < gameManager.NumberOfPlayers; i++)
        {
            CreateButtonForPlayer(i);
        }
    }

    void CreateButtonForPlayer(int playerId)
    {
        GameObject buttonObj = Instantiate(buttonPrefab, buttonParrent);
        buttonObj.name = "PlayerButton" + playerId;
        buttonObj.GetComponentInChildren<TextMeshProUGUI>().text = "Player " + (playerId + 1);

        Button button = buttonObj.GetComponent<Button>();
        button.onClick.AddListener(() => SetLeader(playerId));
    }

    void SetLeader(int playerId)
    {
        gameManager.SetLeader(playerId);
    }
}
