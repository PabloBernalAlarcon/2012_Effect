using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class A_ComportmientoDeHongito : MonoBehaviour
{

    public float IcreasedBy;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            Debug.Log("You hit me");
        }
    }
}
