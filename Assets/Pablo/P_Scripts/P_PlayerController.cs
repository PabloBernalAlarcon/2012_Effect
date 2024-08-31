using UnityEngine;

public class P_PlayerController : MonoBehaviour
{
 
  
    public float jumpForce = 5f; // The force applied when jumping
    public Transform groundCheck; // Reference to the child GameObject used to check if grounded
    public float moveSpeed = 5f; // The speed of horizontal movement
    public float groundDistance = 0.1f; // Distance to check for ground
    public LayerMask groundMask; // LayerMask to define what is considered ground

    private Rigidbody2D rb; // Reference to the Rigidbody component
    private bool isGrounded; // Whether the player is grounded
    private Camera mainCamera; // Reference to the main camera


    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        mainCamera = Camera.main; // Get the main camera
    }

    void Update()
    {
       
        MovePlayer();

        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (CheckGrounded())
            {
            Jump(); // Execute the jump
            }
        }
    }

    bool CheckGrounded()
    {
        // Perform a raycast from the child GameObject to check for the ground
        isGrounded = Physics2D.Raycast(groundCheck.position, Vector3.down, groundDistance, groundMask);
        Debug.Log(isGrounded);

        // Optional: visualize the raycast in the editor
        Debug.DrawRay(groundCheck.position, Vector3.down * groundDistance, Color.red);
        return isGrounded;
    }

    void MovePlayer()
    {
        float moveInput = Input.GetAxis("Horizontal"); // Get input from arrow keys or A/D keys
        Vector3 move = new Vector3(moveInput * moveSpeed, rb.velocity.y, 0);

        rb.velocity = move; // Apply movement

        // Clamp the player's position within the camera's view
       // Vector3 clampedPosition = transform.position;
       // clampedPosition.x = Mathf.Clamp(clampedPosition.x, GetCameraMinX(), GetCameraMaxX());
       // transform.position = clampedPosition;
    }

    void Jump()
    {
        rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);

    }

    float GetCameraMinX()
    {
        // Get the left edge of the camera view in world coordinates
        return mainCamera.ViewportToWorldPoint(new Vector3(0, 0, mainCamera.transform.position.z)).x;
    }

    float GetCameraMaxX()
    {
        // Get the right edge of the camera view in world coordinates
        return mainCamera.ViewportToWorldPoint(new Vector3(1, 0, mainCamera.transform.position.z)).x;
    }

}
