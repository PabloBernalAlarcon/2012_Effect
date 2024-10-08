using UnityEngine;
using System.Collections;

public class J_EnemyJump : MonoBehaviour
{
    public float jumpForce = 10f; // Fuerza del salto
    public float rotationSpeed = 100f; // Velocidad de rotaci�n alrededor del punto de pivote
    public Transform groundCheck; // Transform para verificar si est� en el suelo
    public float groundCheckRadius = 0.1f; // Radio de la verificaci�n del suelo
    public LayerMask groundLayer; // Capa que representa el suelo

    private Rigidbody2D rb; // Referencia al componente Rigidbody2D
    private bool isGrounded = true; // Indica si el objeto est� en el suelo
    public float lifeSpan;


    private void OnEnable()
    {
        // Subscribe to the event
        P_GameManager.OnGodTriggerWarning += HandlegODNAGRY;
    }

    private void OnDisable()
    {
        // Unsubscribe from the event
        P_GameManager.OnGodTriggerWarning -= HandlegODNAGRY;
    }

    // This method will be called when the event is triggered
    private void HandlegODNAGRY()
    {
        if (Random.Range(1,100) > 40)
             Jump();
    }

    IEnumerator moristes()
    {
        yield return new WaitForSeconds(lifeSpan);
        Destroy(gameObject);
    }

    void Start()
    {
        StartCoroutine(moristes());
        rb = GetComponent<Rigidbody2D>(); // Obtener el componente Rigidbody2D
    }

    void Update()
    {
        // Verificar si el objeto est� en el suelo
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, groundLayer);

        // Hacer que el objeto salte si est� en el suelo
       // if (Random.Range(1,1000) > 996)
       // {
       //     Jump();
       // }

        // Mantener la orientaci�n del objeto sobre el suelo mientras est� en el aire
        if (!isGrounded)
        {
            RotateAroundGround();
        }

        
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
          if (collision.gameObject.name == "Meteorito(Clone)")
        {
            Jump();
            RotateAroundGround();
        }
    }

    void Jump()
    {
        GetComponent<AudioSource>().Play();
        // Aplicar una fuerza hacia arriba para hacer que el objeto salte
        rb.velocity = new Vector2(rb.velocity.x, jumpForce);
    }

    void RotateAroundGround()
    {
        // Rotar el objeto alrededor del punto de pivote (su posici�n en el suelo)
        transform.RotateAround(groundCheck.position, Vector3.forward, rotationSpeed * Time.deltaTime);
    }

    // Dibujar el �rea de verificaci�n del suelo en la escena para facilitar el ajuste
    private void OnDrawGizmosSelected()
    {
        if (groundCheck != null)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(groundCheck.position, groundCheckRadius);
        }
    }
}
