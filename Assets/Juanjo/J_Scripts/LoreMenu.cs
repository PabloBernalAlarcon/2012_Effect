using UnityEngine;
using UnityEngine.SceneManagement;
public class LoreMenu : MonoBehaviour
{
    public void Jugar()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);

    }


    


}
