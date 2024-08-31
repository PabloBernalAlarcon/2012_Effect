using UnityEngine;

public class P_GameManager : MonoBehaviour
{
    // Static instance of GameManager which allows it to be accessed by any other script.
    public static P_GameManager Instance { get; private set; }

    [SerializeField]
    P_UIController UIController;

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
    public float playerScore = 0;

    private void Update()
    {
        playerScore += Time.deltaTime;
        UIController.scoreText.text = ((int)playerScore).ToString();
    }


}
