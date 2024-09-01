using System.Collections;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class P_Background_Item : MonoBehaviour
{
    [SerializeField]
    int duration;
   Vector3 initialY;
    [SerializeField]
    Vector3 finalY;

    MeshRenderer mr;


    private void Start()
    {
        initialY =transform.position;
        initialY.y = 1;
        mr = GetComponent<MeshRenderer>();
       // LowerBackground();
    }

    public void LowerBackground()
    {
        StartCoroutine(lower());
    }

    IEnumerator lower()
    {
        float elapsedTime = 0;
        transform.position = initialY;

        while (elapsedTime < duration)
        {
            transform.position = new Vector3(transform.position.x,
                Vector3.Lerp(initialY, finalY, (elapsedTime / duration)).y,
                initialY.z);
            elapsedTime += Time.deltaTime;
            yield return null;
        }
        transform.position = finalY;

    }

    public void RaiseBackGround()
    {
        StartCoroutine(raise());
    }

    IEnumerator raise()
    {


       
        float elapsedTime = 0;

        while (elapsedTime < duration)
        {
            transform.position = new Vector3(transform.position.x,
                Vector3.Lerp(finalY, initialY, (elapsedTime / duration)).y,
                initialY.z);
            elapsedTime += Time.deltaTime;
            yield return null;
        }
        transform.position = initialY;
    }
}
