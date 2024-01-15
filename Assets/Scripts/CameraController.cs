using UnityEngine;

public class CameraController : MonoBehaviour
{
    private Vector3 initialPosition = new Vector3(-5, 10, -5);
    private Vector3 initialRotation = new Vector3(45, 45, 0);

    public Transform leader;
    private Vector3 offset = new Vector3(-6, 9, -6);

    [SerializeField] private float smoothSpeed = 5f;


    void Start()
    {
        transform.position = initialPosition;
        transform.eulerAngles = initialRotation;
    }

    // Update is called once per frame
    void Update()
    {
        if(leader != null)
        {
            UpdatePosition();
        }       
    }

    public void SetLeader(Transform newLeader)
    {
        leader = newLeader;
        Debug.Log("New leader set: " + newLeader.name);
    }

    void UpdatePosition()
    {
        transform.position = Vector3.Lerp(transform.position, leader.position + offset, Time.deltaTime * smoothSpeed);
    }
}
