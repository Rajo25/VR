using TMPro;
using UnityEngine;

public class HP_Display : MonoBehaviour
{
    [SerializeField] private HP_Controller playerHealth;
    [SerializeField] private TextMeshPro displayText;

    private void Start()
    {
        if (playerHealth != null)
        {
            playerHealth.OnHealthChanged += UpdateDisplay;

            UpdateDisplay(
                playerHealth.CurrentHealth,
                playerHealth.MaxHealth);
        }
    }

    private void OnDestroy()
    {
        if (playerHealth != null)
            playerHealth.OnHealthChanged -= UpdateDisplay;
    }

    private void UpdateDisplay(int currentHealth, int maxHealth)
    {
        int percent = Mathf.RoundToInt((float)currentHealth / maxHealth * 100f);

        displayText.text =
            $"HULL INTEGRITY\n\n{percent}%";
    }
}