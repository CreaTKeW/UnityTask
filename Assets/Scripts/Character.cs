using UnityEngine;
using UnityEngine.AI;
using UnityEngine.EventSystems;

public class Character : MonoBehaviour
{
    private Camera mainCamera;
    private NavMeshAgent agent;

    [Header("Movement values")]
    [SerializeField] private float speed;
    [SerializeField] private float agility;
    [SerializeField] private float stamina;

    // Start is called before the first frame update
    void Start()
    {
        mainCamera = FindObjectOfType<Camera>();
        agent = GetComponent<NavMeshAgent>();
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetMouseButtonDown(0) && !EventSystem.current.IsPointerOverGameObject()) // Checks if LMB was pressed and also if cursor is on gameobject
        {
            Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if(Physics.Raycast(ray, out hit))
            {
                agent.SetDestination(hit.point);
            }
        }
    }
}
