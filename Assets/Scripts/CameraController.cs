using UnityEngine;

public class CameraController : MonoBehaviour
{
    [Header("Camera position and follow behaviour")]
    [SerializeField] private Vector3 initialPosition = new Vector3(-5, 10, -5);
    [SerializeField] private Vector3 initialRotation = new Vector3(45, 45, 0);
    [SerializeField] private Vector3 offset = new Vector3(-6, 9, -6);
    [SerializeField] private float smoothSpeed = 5f;

    private Transform leader;

    void Start()
    {
        // Initialize camera's position and rotation
        transform.position = initialPosition;
        transform.eulerAngles = initialRotation;
    }

    void LateUpdate()
    {
        // Update camera position to follow the leader
        if (leader != null)
        {
            transform.position = Vector3.Lerp(transform.position, leader.position + offset, Time.deltaTime * smoothSpeed);
        }
    }

    // Assign a new leader for the camera to follow
    public void SetLeader(Transform newLeader)
    {
        if (newLeader != null)
        {
            leader = newLeader;
            Debug.Log("New leader set: " + leader.name);
        }
        else
        {
            Debug.LogError("Attempted to set a null leader.");
        }
    }
}