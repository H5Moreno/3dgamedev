using UnityEngine;
using UnityEngine.AI;

public class EnemyFollow : MonoBehaviour
{
    private NavMeshAgent wolf;       // Automatically assigned
    public Transform player;         // Still needs to be assigned
    public float followSpeed = 10f;

    private bool shouldChase = false;

    void Awake()
    {
        // Get the NavMeshAgent component attached to this GameObject
        wolf = GetComponent<NavMeshAgent>();
        if (wolf == null)
        {
            Debug.LogError("NavMeshAgent is missing from the enemy!");
        }
    }

    void Start()
    {
        // Set follow speed
        if (wolf != null)
            wolf.speed = followSpeed;

        // Begin 5-second delay before chasing
        Invoke("StartChasing", 1f);
    }

    void StartChasing()
    {
        shouldChase = true;
    }

    void Update()
    {
        if (shouldChase && player != null && wolf != null && wolf.isOnNavMesh)
        {
            wolf.SetDestination(player.position);
        }
    }
}
