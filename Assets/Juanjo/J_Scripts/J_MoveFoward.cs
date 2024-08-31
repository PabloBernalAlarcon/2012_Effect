using UnityEngine;

public class MoveForward2D : MonoBehaviour
{
    public float speed = 5f; // Velocidad del movimiento
    private bool isMoving = true; // Controla si el objeto sigue moviéndose
    public string groundTag = "Ground"; // Tag del objeto que se considera como suelo
    

   
   

    void Update()
    {
        if (isMoving)
        {
            // Mover el objeto hacia la derecha (en la dirección de su frente) basado en la velocidad y el tiempo transcurrido
            transform.Translate(Vector2.right * speed * Time.deltaTime);
        }
    }

    
    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag(groundTag))
        {
            GetComponent<Rigidbody2D>().freezeRotation = true;
            speed = 0;

           
        }
    }
}