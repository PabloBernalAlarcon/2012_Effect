using System.Collections;
using UnityEngine;

public class P_GodController : MonoBehaviour
{
    Animator anim;

    private void Start()
    {
        anim = GetComponent<Animator>();
    }
    private void OnEnable()
    {
        // Subscribe to the event
        P_GameManager.OnGodTriggerWarning += HandleAngryGod;
    }

    private void OnDisable()
    {
        // Unsubscribe from the event
        P_GameManager.OnGodTriggerWarning -= HandleAngryGod;
    }

    // This method will be called when the event is triggered
    private void HandleAngryGod()
    {
        StartCoroutine(rage());
    }

    IEnumerator rage()
    {
        anim.SetBool("rage",true);
        yield return new WaitForSeconds(3);
        anim.SetBool("rage", false);
    }
}
