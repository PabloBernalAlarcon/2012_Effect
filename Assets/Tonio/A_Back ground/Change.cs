using UnityEngine;
using UnityEngine.SceneManagement;

public class Change : MonoBehaviour
{
    private int NumberBackground = 0;
    private int NumberForeground = 0;
    public GameObject Background;
    public GameObject NewBackground;

    void Start()
    {

    }

    void Update()
    {
        Background.SetActive(true);
        NewBackground.SetActive(false);
        if (Input.GetKeyDown(KeyCode.A)) // Change this to your preferred key
        {
            NumberBackground += 1;
        }
        if(NumberBackground > NumberForeground)
        {
            Background.SetActive(false);
            NewBackground.SetActive(true);
        }
    }

}
