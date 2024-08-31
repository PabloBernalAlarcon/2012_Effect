using UnityEngine;

public class P_GameManager : MonoBehaviour
{
    // Static instance of GameManager which allows it to be accessed by any other script.
    public static P_GameManager Instance { get; private set; }

    [SerializeField]
    P_UIController UIController;

    // Define the delegate type
    public delegate void PlayerCollisionHandler();

    // Define an event of the delegate type
    public static event PlayerCollisionHandler OnPlayerEnemyCollision;

    public delegate void GodTriggerWarning();

    public static event GodTriggerWarning OnGodTriggerWarning;

   
    // Called when the script instance is being loaded
    private void Awake()
    {
        // Check if the instance already exists and destroy this one if it does
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        // Set the instance to this object and make it persistent across scenes
        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    // Example of a variable or a game state
    public float playerScore = 1;

    private void Update()
    {
        playerScore += Time.deltaTime;
        UIController.scoreText.text = ((int)playerScore).ToString();

        if (((int)playerScore) % 10 == 0)
        {         
            GodWasTriggered();
            playerScore++;           
        }
    }

    // This method will be called when a collision is detected
    public void PlayerCollidedWithEnemy()
    {
        // Invoke the event
        OnPlayerEnemyCollision?.Invoke();
        Time.timeScale = 0;
    }

    public void GodWasTriggered()
    {
        OnGodTriggerWarning?.Invoke();
    }

}
