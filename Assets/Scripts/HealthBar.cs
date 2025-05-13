using UnityEngine;
using UnityEngine.UI;

public class HealthBarUI : MonoBehaviour
{
    [Header("Assign your fill image here")]
    public Image healthFillImage;

    [Range(0, 100)]
    public float displayHealth = 100f;  // Simulated health value for now

    public void UpdateHealth(float normalizedValue)
    {
        if (healthFillImage != null)
        {
            healthFillImage.fillAmount = Mathf.Clamp01(normalizedValue);
        }
    }
}
