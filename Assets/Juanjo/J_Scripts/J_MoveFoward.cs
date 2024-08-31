using System.Collections;
using UnityEngine;

public class MoveForward2D : MonoBehaviour
{
    public float speed = 5f; // Velocidad del movimiento
    private bool isMoving = true; // Controla si el objeto sigue moviéndose
    public string groundTag = "Ground"; // Tag del objeto que se considera como suelo
    public float lifeSpan;
    

   IEnumerator moristes()
    {
        yield return new WaitForSeconds(lifeSpan);
        Destroy(gameObject);
    }
   

    void Update()
    {
        if (isMoving)
        {
            // Mover el objeto hacia la derecha (en la dirección de su frente) basado en la velocidad y el tiempo transcurrido
            transform.Translate(Vector2.right * speed * Time.deltaTime);
        }
    }

    GameObject largeSphere;
    
    void OnCollisionEnter2D(Collision2D collision)
    {
       
           // GetComponent<Rigidbody2D>().freezeRotation = true;
           // speed = 0;

            if (collision.gameObject.CompareTag(groundTag) && !isStuck)
            {
                largeSphere = collision.gameObject;
                // Calculate the direction from the large sphere's center to the small sphere's center
                Vector2 direction = (transform.position - largeSphere.transform.position).normalized;

                // Calculate the sticking point on the surface of the large sphere
                float largeRadius = largeSphere.GetComponent<CircleCollider2D>().radius*largeSphere.transform.localScale.x;
                Vector2 stickingPoint = (Vector2)largeSphere.transform.position + direction * largeRadius;

                // Position the small sphere on the surface and make it a child of the large sphere
                transform.position = stickingPoint;
                transform.SetParent(largeSphere.transform);

                // Optionally, disable Rigidbody2D to stop further movement
                GetComponent<Rigidbody2D>().isKinematic = true;

                isStuck = true;

            StartCoroutine(moristes());
            
        }

        if (collision.gameObject.tag == "Player")
        {
            Destroy(collision.gameObject);
            Destroy(gameObject);
        }
    }

  
    private bool isStuck = false;

    
}