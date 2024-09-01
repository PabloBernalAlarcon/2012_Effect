using UnityEngine;

public class P_PlayerController : MonoBehaviour
{
 
  
    public float jumpForce = 5f; // The force applied when jumping
    public float maxJumpTime = 0.5f; // The maximum time the jump button can be held
    public float jumpTimeFactor = 3f; // The factor to scale jump force while the button is held
    public Transform groundCheck; // Reference to the child GameObject used to check if grounded
    public float moveSpeed = 5f; // The speed of horizontal movement
    public float groundDistance = 0.1f; // Distance to check for ground
    public LayerMask groundMask; // LayerMask to define what is considered ground

    private Rigidbody2D rb; // Reference to the Rigidbody component
    private bool isGrounded; // Whether the player is grounded
    private bool isJumping; // Whether the player is in the process of jumping
    private float jumpTimeCounter;
    private Camera mainCamera; // Reference to the main camera
    private Animator anim;


    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        mainCamera = Camera.main; // Get the main camera
    }

    void Update()
    {
       
        MovePlayer();
        CheckGrounded();

        if (isGrounded && Input.GetKeyDown(KeyCode.Space))
        {
            StartJump(); // Start the jump
        }
        if (Input.GetKey(KeyCode.Space) && isJumping)
        {
            ContinueJump(); // Continue jumping while the button is held
        }

        if (Input.GetKeyUp(KeyCode.Space))
        {
            EndJump(); // End the jump when the button is released
        }

       // if (Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.W))
       // {
       //     if (CheckGrounded())
       //     {
       //     Jump(); // Execute the jump
       //         anim.SetTrigger("jump");
       //     }
       // }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
       if(collision.gameObject.tag == "Enemy")
        {
            P_GameManager.Instance.PlayerCollidedWithEnemy();
            Destroy(collision.gameObject);
            Destroy(gameObject);
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
       // Debug.Log (move);
       // rb.AddForce(move,ForceMode2D.Impulse);
        rb.velocity = move; // Apply movement

       // Clamp the player's position within the camera's view
       Vector3 clampedPosition = transform.position;
       clampedPosition.x = Mathf.Clamp(clampedPosition.x, GetCameraMinX(), GetCameraMaxX());
       transform.position = clampedPosition;
    }

    void Jump()
    {
        rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);

    }

    void StartJump()
    {
        isJumping = true;
        jumpTimeCounter = maxJumpTime;
        rb.AddForce(Vector3.up * jumpForce, ForceMode2D.Impulse); // Apply the initial jump force
    }

    void ContinueJump()
    {
        if (jumpTimeCounter > 0)
        {
            rb.AddForce(Vector3.up * jumpForce * jumpTimeFactor * Time.deltaTime, ForceMode2D.Force); // Apply continuous force
            jumpTimeCounter -= Time.deltaTime;
        }
        else
        {
            EndJump(); // Stop applying force if the max jump time is reached
        }
    }

    void EndJump()
    {
        isJumping = false;
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
