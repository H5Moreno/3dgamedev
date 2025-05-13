using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    public float maxHealth = 100f;
    private float currentHealth;

    // Reference to the spawner (set from inspector or found in Start)
    public WolfSpawner spawner;
    private PlayerHealth playerHealth; // ? Reference to player

    void Start()
    {
        currentHealth = maxHealth;

        // ? Find the player by tag and get PlayerHealth component
        GameObject playerObj = GameObject.FindGameObjectWithTag("Player");
        if (playerObj != null)
        {
            playerHealth = playerObj.GetComponent<PlayerHealth>();
        }
    }

    public void TakeDamage(float amount)
    {
        currentHealth -= amount;
        if (currentHealth <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        // ? Tell spawner
        if (spawner != null)
        {
            spawner.OnWolfDeath();
        }

        // ? Register kill on player
        if (playerHealth != null)
        {
            playerHealth.RegisterKill();
        }

        Destroy(gameObject);
    }
}
