using UnityEngine;

public class P_BackgroundChangeController : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    [SerializeField]
    public P_Background_Item[] BItems;

    int currentBack = 0;

    // Define the delegate type
    public delegate void BGChangeHandler();

    // Define an event of the delegate type
    public static event BGChangeHandler OnBGSequenceOver;

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
            BackgroundIsOver();
          
        }
        else
        {
        BItems[currentBack].LowerBackground();
            currentBack++;
        }


      
    }

    public void AllRaise()
    {
        currentBack = 0;
        for (int i = 0; i < BItems.Length; i++)
        {
            BItems[i].RaiseBackGround();
        }

    }

    public void BackgroundIsOver()
    {
       
        OnBGSequenceOver?.Invoke();
       //this.gameObject.SetActive(false);
       

    }


}
