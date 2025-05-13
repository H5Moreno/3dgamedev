using UnityEngine;
using System.Collections;

public class PlayerAttack : MonoBehaviour
{
    public GameObject weaponGameObject;
    private bool isAttacking = false;
    public float cooldownTime = 1f;  // Cooldown between attacks (in seconds)
    private float lastAttackTime = 0f; // Track the time of the last attack

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0) && !isAttacking && Time.time >= lastAttackTime + cooldownTime)
        {
            // Check if enough time has passed since the last attack
            StartCoroutine(AttackSwing());
        }
    }

    IEnumerator AttackSwing()
    {
        isAttacking = true;
        lastAttackTime = Time.time; // Record the time when the attack happens

        WeaponDamage wd = weaponGameObject.GetComponent<WeaponDamage>();
        wd.canHit = true;

        float duration = 0.2f;
        float elapsed = 0f;

        Quaternion startRotation = weaponGameObject.transform.localRotation;
        Quaternion targetRotation = Quaternion.Euler(0, 90, 0); // Swing like opening a door

        // Swing out
        while (elapsed < duration)
        {
            weaponGameObject.transform.localRotation = Quaternion.Slerp(startRotation, targetRotation, elapsed / duration);
            elapsed += Time.deltaTime;
            yield return null;
        }

        weaponGameObject.transform.localRotation = targetRotation;

        yield return new WaitForSeconds(0.1f); // Pause at the peak

        // Swing back
        elapsed = 0f;
        while (elapsed < duration)
        {
            weaponGameObject.transform.localRotation = Quaternion.Slerp(targetRotation, startRotation, elapsed / duration);
            elapsed += Time.deltaTime;
            yield return null;
        }

        weaponGameObject.transform.localRotation = startRotation;

        wd.canHit = false;

        isAttacking = false; // Reset attacking flag
    }
}
