using UnityEngine;

public class StaticPlayerText : MonoBehaviour
{
    private Transform cameraPosition;

    void Start()
    {
        Camera mainCamera = FindObjectOfType<Camera>();
       
        if (mainCamera != null)
        {
            cameraPosition = mainCamera.transform;
        }
        else
        {
            Debug.LogError("Main camera not found.");
        }
    }

    void Update()
    {
        if (cameraPosition != null)
        {
            // Adjust text to face towards the camera using the camera's rotation
            transform.LookAt(transform.position + cameraPosition.rotation * Vector3.forward,
                             cameraPosition.rotation * Vector3.up);
        }
        else
        {
            // If no camera is found, default the text to face upward
            transform.LookAt(transform.position + Vector3.forward, Vector3.up);
        }
    }
}