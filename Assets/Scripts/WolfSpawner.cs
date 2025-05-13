using UnityEngine;

public class WolfSpawner : MonoBehaviour
{
    public GameObject wolfPrefab;         // Assign your wolf prefab here
    public Transform player;              // Assign your player here
    public float spawnDelay = 2f;         // Delay between spawns
    public Transform[] spawnPoints;       // Multiple spawn locations

    private int waveNumber = 1;           // Starting wave number
    private int wolvesInCurrentWave = 0;  // Number of wolves in the current wave
    private int wolvesAlive = 0;          // Number of wolves currently alive in the current wave

    private int wolvesKilled = 0;         // Track the number of wolves killed

    private PlayerHealth playerHealth;    // Reference to the PlayerHealth script

    private void Start()
    {
        if (player == null)
        {
            Debug.LogError("Player Transform is not assigned in WolfSpawner!");
            return;
        }

        playerHealth = player.GetComponent<PlayerHealth>();

        if (playerHealth == null)
        {
            Debug.LogError("PlayerHealth component not found on the player!");
            return;
        }

        SpawnWolvesForWave(); // Spawn wolves for the first wave
    }

    // Method to spawn wolves for the current wave
    public void SpawnWolvesForWave()
    {
        wolvesInCurrentWave = waveNumber;  // For example: 1 wolf on wave 1, 2 wolves on wave 2, etc.
        wolvesAlive = wolvesInCurrentWave; // Set the number of wolves alive for the current wave

        // Spawn the wolves
        for (int i = 0; i < wolvesInCurrentWave; i++)
        {
            SpawnWolf(); // Spawn each wolf
        }

        // Increase the wave number for the next wave
        waveNumber++;
    }

    // This method is used to spawn a wolf
    public void SpawnWolf()
    {
        Transform spawnPoint = spawnPoints[Random.Range(0, spawnPoints.Length)];

        GameObject newWolf = Instantiate(wolfPrefab, spawnPoint.position, Quaternion.identity);

        EnemyFollow followScript = newWolf.GetComponent<EnemyFollow>();
        if (followScript != null)
        {
            followScript.player = player;
        }

        EnemyHealth healthScript = newWolf.GetComponent<EnemyHealth>();
        if (healthScript != null)
        {
            healthScript.spawner = this; // Assign the spawner to the wolf's health script
        }
    }

    // This method is called by the wolf when it dies
    public void OnWolfDeath()
    {
        wolvesKilled++; // Increment wolves killed

        Debug.Log("A wolf died. Wolves killed: " + wolvesKilled);

        // Check if the player should enter blood rage
        if (wolvesKilled >= 3)
        {
            playerHealth.StartBloodRage(); // Activate the blood rage buff for the player
            wolvesKilled = 0; // Reset wolf kills after blood rage is triggered
        }

        wolvesAlive--; // Decrease the number of wolves alive

        // If all wolves are dead, spawn the next wave
        if (wolvesAlive <= 0)
        {
            Invoke(nameof(SpawnWolvesForWave), spawnDelay); // Spawn the next wave after a delay
        }
    }
}
