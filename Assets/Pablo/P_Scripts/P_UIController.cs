using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class P_UIController : MonoBehaviour
{
    public TMP_Text scoreText;

    public GameObject gameOverCanvas;

    private void OnEnable()
    {
        // Subscribe to the event
        P_GameManager.OnPlayerEnemyCollision += HandlePlayerEnemyCollision;
    }

    private void OnDisable()
    {
        // Unsubscribe from the event
        P_GameManager.OnPlayerEnemyCollision -= HandlePlayerEnemyCollision;
    }

    private void Start()
    {
        P_GameManager.Instance.playerScore = 0;
    }


    private void Update()
    {
        scoreText.text = ((int)P_GameManager.Instance.playerScore).ToString();
    }
    // This method will be called when the event is triggered

   
    private void HandlePlayerEnemyCollision()
    {
        gameOverCanvas.SetActive(true);
    }
}
