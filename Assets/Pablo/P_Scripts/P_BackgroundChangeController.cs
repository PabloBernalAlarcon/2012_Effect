using UnityEngine;

public class P_BackgroundChangeController : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    [SerializeField]
    P_Background_Item[] BItems;

    int currentBack = 0;

    private void OnEnable()
    {
        // Subscribe to the event
        P_GameManager.OnGodTriggerWarning += triggerBackChange;
    }

    private void OnDisable()
    {
        // Unsubscribe from the event
        P_GameManager.OnGodTriggerWarning -= triggerBackChange;
    }

    // This method will be called when the event is triggered
    private void triggerBackChange()
    {
        if (currentBack >= BItems.Length)
        {
            for (int i = 0;i < BItems.Length; i++)
            {
                BItems[i].RaiseBackGround();
            }

            currentBack = 0;
        }
        else
        {
        BItems[currentBack].LowerBackground();
            currentBack++;
        }


      
    }


}
