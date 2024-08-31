using UnityEngine;
using System.Collections;

public class J_EnemyJump : MonoBehaviour
{
    public float jumpForce = 10f; // Fuerza del salto
    public float rotationSpeed = 100f; // Velocidad de rotación alrededor del punto de pivote
    public Transform groundCheck; // Transform para verificar si está en el suelo
    public float groundCheckRadius = 0.1f; // Radio de la verificación del suelo
    public LayerMask groundLayer; // Capa que representa el suelo

    private Rigidbody2D rb; // Referencia al componente Rigidbody2D
    private bool isGrounded; // Indica si el objeto está en el suelo
    public float lifeSpan;


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
        // Verificar si el objeto está en el suelo
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, groundLayer);

        // Hacer que el objeto salte si está en el suelo
        if (isGrounded && Input.GetKeyDown(KeyCode.Space))
        {
            Jump();
        }

        // Mantener la orientación del objeto sobre el suelo mientras está en el aire
        if (!isGrounded)
        {
            RotateAroundGround();
        }
    }

    void Jump()
    {
        // Aplicar una fuerza hacia arriba para hacer que el objeto salte
        rb.velocity = new Vector2(rb.velocity.x, jumpForce);
    }

    void RotateAroundGround()
    {
        // Rotar el objeto alrededor del punto de pivote (su posición en el suelo)
        transform.RotateAround(groundCheck.position, Vector3.forward, rotationSpeed * Time.deltaTime);
    }

    // Dibujar el área de verificación del suelo en la escena para facilitar el ajuste
    private void OnDrawGizmosSelected()
    {
        if (groundCheck != null)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(groundCheck.position, groundCheckRadius);
        }
    }
}
