using UnityEngine;

public class MoveForward2D : MonoBehaviour
{
    public float speed = 5f; // Velocidad del movimiento
    private bool isMoving = true; // Controla si el objeto sigue moviéndose

    void Update()
    {
        if (isMoving)
        {
            // Mover el objeto hacia la derecha (en la dirección de su frente) basado en la velocidad y el tiempo transcurrido
            transform.Translate(Vector2.right * speed * Time.deltaTime);
        }
    }

    // Detener el movimiento al entrar en contacto con otro objeto
    void OnCollisionEnter2D(Collision2D collision)
    {
        isMoving = false;
    }
}