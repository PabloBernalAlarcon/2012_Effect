using UnityEngine;

public class P_Mayan_Wheel: MonoBehaviour
{

    [SerializeField]
    float wheelRotSpeed=1;

    private void Update()
    {
        transform.Rotate(0,0,wheelRotSpeed);
    }

}
