using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.AI;

public class GameManager : MonoBehaviour
{
    [SerializeField] private List<GameObject> characters;
    [SerializeField] private GameObject characterParent;
    [SerializeField] private GameObject characterPrefab;
    [SerializeField] private int numberOfPlayers;
    [SerializeField] private CameraController cameraController;

    private int playerNum;
    private float searchRadius = 20f;
    private const int minPlayers = 3;
    private const int maxPlayers = 10;
    public int NumberOfPlayers => numberOfPlayers; // Public getter for number of players

    void Start()
    {
        // Initialize characters list and clamp numberOfPlayers between min and max values
        characters = new List<GameObject>();
        numberOfPlayers = Mathf.Clamp(numberOfPlayers, minPlayers, maxPlayers);

        // Instantiate characters based on the number of players
        for (int i = 0; i < numberOfPlayers; i++)
        {
            InstantiateCharacter();
        }
    }

    // Method to instantiate a character at a random position
    void InstantiateCharacter()
    {
        // Try to instantiate a character within 10 attempts
        for (int attempts = 0; attempts < 10; attempts++)
        {
            Vector3 randomPosition = new Vector3(Random.Range(-5f, 5f), 1, Random.Range(-5f, 5f));
            NavMeshHit hit;

            // Check for a valid position on the NavMesh within searchRadius
            if (NavMesh.SamplePosition(randomPosition, out hit, searchRadius, NavMesh.AllAreas))
            {
                // Ensure the position is not too close to other characters
                if (!IsPositionTooCloseToOthers(hit.position))
                {
                    playerNum++;
                    GameObject character = Instantiate(characterPrefab, hit.position, Quaternion.identity);
                    character.transform.SetParent(characterParent.transform, false);
                    character.name = "Player" + playerNum;
                    character.GetComponentInChildren<TextMeshPro>().text = ("Player " + playerNum);
                    characters.Add(character);
                    return;
                }
            }
        }

        Debug.LogWarning("Failed to find a walkable position for the character");
    }

    // Check if the new position is too close to any existing characters
    bool IsPositionTooCloseToOthers(Vector3 position)
    {
        foreach (GameObject existingCharacter in characters)
        {
            if (Vector3.Distance(position, existingCharacter.transform.position) < 1)
                return true;
        }
        return false;
    }

    // Method to set a player as the leader
    public void SetLeader(int playerId)
    {
        // Validate the player ID
        if (playerId < 0 || playerId >= characters.Count)
        {
            Debug.LogError("Invalid player ID");
            return;
        }

        // Set all characters to follow the new leader and change colors accordingly
        foreach (GameObject character in characters)
        {
            Character charScript = character.GetComponent<Character>();
            charScript.IsLeader = false;
            charScript.Leader = characters[playerId].transform;
            character.GetComponent<Renderer>().material.color = Color.green;
        }

        // Update camera focus and mark the leader
        cameraController.SetLeader(characters[playerId].transform);
        characters[playerId].GetComponent<Character>().IsLeader = true;
        characters[playerId].GetComponent<Renderer>().material.color = Color.red;
    }
}