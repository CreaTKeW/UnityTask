using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class GameManager : MonoBehaviour
{    
    [SerializeField] private List<GameObject> characters;
    [SerializeField] private GameObject characterPrefab;
    [SerializeField] private int numberOfPlayers;

    
    private CameraController cameraController;
    private float searchRadius = 10f;
    public int NumberOfPlayers => numberOfPlayers;

    // Start is called before the first frame update
    void Start()
    {
        cameraController = FindObjectOfType<CameraController>();
        characters = new List<GameObject>();
        numberOfPlayers = Mathf.Max(3, numberOfPlayers);

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
                GameObject character = Instantiate(characterPrefab, hit.position, Quaternion.identity);
                characters.Add(character);
                return;
            }
        }

        Debug.LogWarning("Failed to find a walkable position for the character");
    }

    public void SetLeader(int playerId)
    {
        foreach (GameObject character in characters)
        {
            character.GetComponent<Renderer>().material.color = Color.green;
        }

        cameraController.SetLeader(characters[playerId].transform);
        characters[playerId].GetComponent<Renderer>().material.color = Color.red;
    }
}
