using UnityEngine;

public class P_Mayan_Wheel: MonoBehaviour
{

    [SerializeField]
    float wheelRotSpeed=1;

    private void FixedUpdate()
    {
        transform.Rotate(0,0,wheelRotSpeed);
    }

}
