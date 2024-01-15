using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class GameManager : MonoBehaviour
{    
    [SerializeField] private List<GameObject> characters;
    [SerializeField] private GameObject characterParent;
    [SerializeField] private GameObject characterPrefab;
    [SerializeField] private int numberOfPlayers;

    private CameraController cameraController;
    private int playerNum;
    private float searchRadius = 20f;   
    private const int minPlayers = 3;
    private const int maxPlayers = 10;
    public int NumberOfPlayers => numberOfPlayers;

    // Start is called before the first frame update
    void Start()
    {
        cameraController = FindObjectOfType<CameraController>();
        characters = new List<GameObject>();
        numberOfPlayers = Mathf.Clamp(numberOfPlayers, minPlayers, maxPlayers);

        for (int i = 0; i < numberOfPlayers; i++)
        {
            InstantiateCharacter();
        }
    }

    void InstantiateCharacter()
    {
        for (int attempts = 0; attempts < 10; attempts++)
        {
            Vector3 randomPosition = new Vector3(Random.Range(-5f, 5f), 1, Random.Range(-5f, 5f));
            NavMeshHit hit;

            if (NavMesh.SamplePosition(randomPosition, out hit, searchRadius, NavMesh.AllAreas))
            {
                // Check if this position is too close to any existing character
                bool tooClose = false;
                foreach (GameObject existingCharacter in characters)
                {
                    if (Vector3.Distance(hit.position, existingCharacter.transform.position) < 1)
                    {
                        tooClose = true;
                        break; // Break out of the foreach loop
                    }
                }

                if (!tooClose)
                {
                    playerNum++;

                    GameObject character = Instantiate(characterPrefab, hit.position, Quaternion.identity);
                    character.transform.SetParent(characterParent.transform, false);
                    character.name = "Player" + playerNum;
                    characters.Add(character);
                    return; // Exit the method if a suitable position is found
                }
            }
        }

        Debug.LogWarning("Failed to find a walkable position for the character");
    }

    public void SetLeader(int playerId)
    {
        foreach (GameObject character in characters)
        {
            Character charScript = character.GetComponent<Character>();
            charScript.IsLeader = false;
            charScript.Leader = characters[playerId].transform;
            character.GetComponent<Renderer>().material.color = Color.green;
        }

        cameraController.SetLeader(characters[playerId].transform);
        characters[playerId].GetComponent<Character>().IsLeader = true;
        characters[playerId].GetComponent<Renderer>().material.color = Color.red;        
    }
}
