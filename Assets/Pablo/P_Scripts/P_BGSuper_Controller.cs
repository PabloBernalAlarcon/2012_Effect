using System.Collections;
using UnityEngine;

public class P_BGSuper_Controller : MonoBehaviour
{
    [SerializeField]
    P_BackgroundChangeController[] backgroudintos;

    int currentBackground;

   
    // Start is called once before the first execution of Update after the MonoBehaviour is created

    private void OnEnable()
    {
        // Subscribe to the event
        P_BackgroundChangeController.OnBGSequenceOver += HandleBackgroundManagement;
    }

    private void OnDisable()
    {
        // Unsubscribe from the event
        P_BackgroundChangeController.OnBGSequenceOver -= HandleBackgroundManagement;
    }

    // This method will be called when the event is triggered
    private void HandleBackgroundManagement()
    {
        StartCoroutine(changeito());
       
    }

    IEnumerator changeito()
    {
        backgroudintos[currentBackground].gameObject.SetActive(false);
         currentBackground++;
         if (currentBackground >= backgroudintos.Length) { currentBackground = 0; }
        yield return new WaitForSeconds(5);
         backgroudintos[currentBackground].gameObject.SetActive(true);
        backgroudintos[currentBackground].AllRaise();
    }


}
