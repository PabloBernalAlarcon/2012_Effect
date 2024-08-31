using UnityEngine;

public class P_PlayerController : MonoBehaviour
{
    public float jumpForce = 5f; // The force applied when jumping
    public Transform groundCheck; // Reference to the child GameObject used to check if grounded
    public float groundDistance = 0.1f; // Distance to check for ground
    public LayerMask groundMask; // LayerMask to define what is considered ground

    private Rigidbody2D rb; // Reference to the Rigidbody component
    private bool isGrounded; // Whether the player is grounded

    void Start()
    {
        rb = GetComponent<Rigidbody2D>(); // Get the Rigidbody component
    }

    void Update()
    {
       

        if (Input.GetKeyDown(KeyCode.Space))
        {
            Jump(); // Execute the jump
        }
    }

    bool CheckGrounded()
    {
        // Perform a raycast from the child GameObject to check for the ground
        isGrounded = Physics.Raycast(groundCheck.position, Vector3.down, groundDistance);
        Debug.Log(isGrounded);

        // Optional: visualize the raycast in the editor
        Debug.DrawRay(groundCheck.position, Vector3.down * groundDistance, Color.red);
        return isGrounded;
    }

    void Jump()
    {
        rb.AddForce(Vector3.up * jumpForce, ForceMode2D.Impulse); // Apply the jump force
    }
}
