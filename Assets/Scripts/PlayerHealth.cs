using UnityEngine;
using UnityEngine.UI;
using TMPro; // At the top



public class PlayerHealth : MonoBehaviour
{
    [Header("Health Settings")]
    public float maxHealth = 100f;
    private float currentHealth;

    [Header("UI")]
    public GameObject gameOverUI;
    public Image bloodRageTint;
    public HealthBarUI healthBarUI;
    public TextMeshProUGUI bloodRageText; // Instead of UnityEngine.UI.Text
    public TextMeshProUGUI wolfKillCountText;
    private int totalWolvesKilled = 0;

    [Header("Blood Rage")]
    public float bloodRageDuration = 5f;
    private float bloodRageTimer = 0f;
    private bool isBloodRageActive = false;
    private int killCount = 0;

    [Header("Damage")]
    public float baseDamage = 10f;
    private float currentDamage;

    void Start()
    {
        currentHealth = maxHealth;
        currentDamage = baseDamage;

        if (gameOverUI != null) gameOverUI.SetActive(false);
        if (bloodRageTint != null) SetTintAlpha(0f);
        if (bloodRageText != null)
            bloodRageText.gameObject.SetActive(false);
        if (wolfKillCountText != null)
            wolfKillCountText.text = "Wolves Killed: 0";

        UpdateHealthUI(); // ✅ Ensure the UI reflects starting health
    }

    void Update()
    {
        if (isBloodRageActive)
        {
            bloodRageTimer -= Time.deltaTime;

            if (bloodRageTimer <= 0f)
            {
                EndBloodRage();
            }
        }
    }

    public void TakeDamage(float amount)
    {
        currentHealth -= amount;

        // Update health UI
        if (healthBarUI != null)
        {
            healthBarUI.UpdateHealth(currentHealth / maxHealth);
        }

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        Time.timeScale = 0f;
        if (gameOverUI != null)
        {
            gameOverUI.SetActive(true);
        }
    }
    void UpdateKillCountUI()
    {
        if (wolfKillCountText != null)
        {
            wolfKillCountText.text = "Wolves Killed: " + totalWolvesKilled;
        }
    }
    public void RegisterKill()
    {
        killCount++;
        totalWolvesKilled++;

        UpdateKillCountUI(); // Update UI with new value

        if (killCount >= 3 && !isBloodRageActive)
        {
            StartBloodRage();
            killCount = 0; // ✅ Reset kills after triggering
        }
    }



    public void StartBloodRage()
    {
        isBloodRageActive = true;
        currentDamage = baseDamage * 2;
        bloodRageTimer = bloodRageDuration;
        SetTintAlpha(0.4f);

        if (bloodRageText != null)
            bloodRageText.gameObject.SetActive(true); // ✅ Show text
    }

    void EndBloodRage()
    {
        isBloodRageActive = false;
        currentDamage = baseDamage;
        SetTintAlpha(0f);

        if (bloodRageText != null)
            bloodRageText.gameObject.SetActive(false); // ✅ Hide text
    }


    void SetTintAlpha(float alpha)
    {
        if (bloodRageTint != null)
        {
            Color c = bloodRageTint.color;
            c.a = alpha;
            bloodRageTint.color = c;
        }
    }

    // ✅ New method to update the health bar fill amount
    void UpdateHealthUI()
    {
        if (healthBarUI != null)
        {
            healthBarUI.UpdateHealth(currentHealth / maxHealth);
        }
    }


    public float GetCurrentDamage()
    {
        return currentDamage;
    }
}
