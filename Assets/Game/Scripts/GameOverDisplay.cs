using UnityEngine;

public class GameOverDisplay : MonoBehaviour
{
    [SerializeField] private HP_Controller playerHealth;
    [SerializeField] private GameObject gameOverText;

    private void Start()
    {
        gameOverText.SetActive(false);

        playerHealth.OnPlayerDied += ShowGameOver;
    }

    private void OnDestroy()
    {
        if (playerHealth != null)
            playerHealth.OnPlayerDied -= ShowGameOver;
    }

    private void ShowGameOver()
    {
        gameOverText.SetActive(true);
    }
}
