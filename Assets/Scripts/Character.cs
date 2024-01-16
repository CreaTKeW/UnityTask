using UnityEngine;
using UnityEngine.AI;
using UnityEngine.EventSystems;

public class Character : MonoBehaviour
{
    public bool IsLeader { get; set; }
    public Transform Leader { get; set; }

    private Camera mainCamera;
    private NavMeshAgent agent;
    private float followDistance = 2f; // Distance between leader and other players

    private float speed;
    private float agility;
    private float stamina;

    // Randomized movement attributes
    [Header("Movement values")]
    [SerializeField] private float minSpeed = 3f;
    [SerializeField] private float maxSpeed = 6f;
    [SerializeField] private float minAgility = 100f;
    [SerializeField] private float maxAgility = 200f;
    [SerializeField] private float minStamina = 6f;
    [SerializeField] private float maxStamina = 12f;

    void Start()
    {
        mainCamera = FindObjectOfType<Camera>();
        agent = GetComponent<NavMeshAgent>();

        // Assign randomized values to attributes
        speed = Random.Range(minSpeed, maxSpeed);
        agility = Random.Range(minAgility, maxAgility);
        stamina = Random.Range(minStamina, maxStamina);

        // Apply attributes to NavMeshAgent
        agent.speed = speed;
        agent.angularSpeed = agility;
        agent.acceleration = stamina;
    }

    void Update()
    {
        // Leader controls via mouse input
        if (IsLeader)
        {
            LeaderMovement();
        }
        // Follower behavior towards leader
        else if (Leader != null)
        {
            FollowerMovement();
        }
    }

    private void LeaderMovement()
    {
        // Move to clicked position if not clicking on UI
        if (Input.GetMouseButtonDown(0) && !EventSystem.current.IsPointerOverGameObject())
        {
            Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out RaycastHit hit))
            {
                agent.SetDestination(hit.point);
            }
        }
    }

    private void FollowerMovement()
    {
        // Move towards the leader maintaining a specific distance
        float distanceToLeader = Vector3.Distance(transform.position, Leader.position);
        if (distanceToLeader > followDistance)
        {
            Vector3 followPosition = Leader.position - Leader.forward * followDistance;
            agent.SetDestination(followPosition);
        }
        else
        {
            agent.ResetPath();
        }
    }
}