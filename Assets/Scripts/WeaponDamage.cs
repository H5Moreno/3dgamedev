using UnityEngine;

public class WeaponDamage : MonoBehaviour
{
    public float damage = 25f;     // Amount of damage the weapon deals
    public bool canHit = false;    // Whether the weapon is currently in a swing

    private void OnTriggerEnter(Collider other)
    {
        if (canHit && other.CompareTag("Enemy"))
        {
            Debug.Log("Hit enemy: " + other.name);

            EnemyHealth enemy = other.GetComponent<EnemyHealth>();
            if (enemy != null)
            {
                enemy.TakeDamage(damage);
            }

            // Optionally: prevent multiple hits in one swing
            canHit = false;
        }
    }
}
