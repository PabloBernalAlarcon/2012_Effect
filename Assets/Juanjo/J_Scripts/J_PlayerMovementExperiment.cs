using UnityEngine;

public class J_PlayerControllerExperiment : MonoBehaviour
{
    public float minJumpForce = 2f; // Fuerza mínima del salto
    public float maxJumpForce = 10f; // Fuerza máxima del salto
    public float maxHoldTime = 1f; // Tiempo máximo que se puede mantener presionada la tecla para aumentar la fuerza del salto
    public Transform groundCheck; // Referencia al GameObject hijo utilizado para verificar si está en el suelo
    public float moveSpeed = 5f; // Velocidad del movimiento horizontal
    public float groundDistance = 0.1f; // Distancia para verificar el suelo
    public LayerMask groundMask; // LayerMask para definir qué se considera suelo

    private Rigidbody2D rb; // Referencia al componente Rigidbody2D
    private bool isGrounded; // Si el jugador está en el suelo
    private Camera mainCamera; // Referencia a la cámara principal
    private Animator anim;
    private float jumpHoldTime; // Tiempo que se ha mantenido presionada la tecla de salto
    private bool isJumping; // Si el jugador está en el proceso de salto

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        mainCamera = Camera.main; // Obtener la cámara principal
    }

    void Update()
    {
        MovePlayer();

        if (Input.GetKeyDown(KeyCode.Space) && CheckGrounded())
        {
            // Iniciar el salto
            isJumping = true;
            jumpHoldTime = 0f;
            anim.SetTrigger("jump");
        }

        if (Input.GetKey(KeyCode.Space) && isJumping)
        {
            // Aumentar el tiempo de mantención de la tecla mientras se mantiene presionada
            jumpHoldTime += Time.deltaTime;
        }

        if (Input.GetKeyUp(KeyCode.Space) && isJumping)
        {
            // Ejecutar el salto con una fuerza proporcional al tiempo que se mantuvo presionada la tecla
            Jump();
            isJumping = false;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Enemy")
        {
            P_GameManager.Instance.PlayerCollidedWithEnemy();
            Destroy(collision.gameObject);
            Destroy(gameObject);
        }
    }

    bool CheckGrounded()
    {
        // Realizar un raycast desde el GameObject hijo para verificar el suelo
        isGrounded = Physics2D.Raycast(groundCheck.position, Vector3.down, groundDistance, groundMask);
        Debug.Log(isGrounded);

        // Visualizar opcionalmente el raycast en el editor
        Debug.DrawRay(groundCheck.position, Vector3.down * groundDistance, Color.red);
        return isGrounded;
    }

    void MovePlayer()
    {
        float moveInput = Input.GetAxis("Horizontal"); // Obtener la entrada de las teclas de dirección o A/D
        Vector3 move = new Vector3(moveInput * moveSpeed, rb.velocity.y, 0);
        rb.velocity = move; // Aplicar movimiento

        // Limitar la posición del jugador dentro de la vista de la cámara
        Vector3 clampedPosition = transform.position;
        clampedPosition.x = Mathf.Clamp(clampedPosition.x, GetCameraMinX(), GetCameraMaxX());
        transform.position = clampedPosition;
    }

    void Jump()
    {
        // Calcular la fuerza del salto basada en el tiempo de mantención de la tecla de salto
        float jumpForce = Mathf.Lerp(minJumpForce, maxJumpForce, jumpHoldTime / maxHoldTime);
        rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
    }

    float GetCameraMinX()
    {
        // Obtener el borde izquierdo de la vista de la cámara en coordenadas del mundo
        return mainCamera.ViewportToWorldPoint(new Vector3(0, 0, mainCamera.transform.position.z)).x;
    }

    float GetCameraMaxX()
    {
        // Obtener el borde derecho de la vista de la cámara en coordenadas del mundo
        return mainCamera.ViewportToWorldPoint(new Vector3(1, 0, mainCamera.transform.position.z)).x;
    }
}