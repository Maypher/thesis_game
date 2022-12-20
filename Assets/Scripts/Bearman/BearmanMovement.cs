using UnityEngine;

public class BearmanMovement : MonoBehaviour
{
    [Header("Components")]
    private Rigidbody2D rb;
    [SerializeField] private Transform groundCheck;
    private BearmanCombat combatController;
    private CapsuleCollider2D hitbox;

    [Header("Movement variables")]
    [SerializeField] private float maxSpeed = 5f;
    // When running multiply targetSpeed by this value
    [SerializeField] private float runMultiplier = 2f;
    [SerializeField] private float acceleration = 5f;
    [SerializeField] private float jumpForce = 10f;
    [Range (0, 1)]
    [SerializeField] private float lerpAmount = 0f;
    [SerializeField] private float gravFallMultiplier = 2f;

    [Header("Checks")]
    [SerializeField] private LayerMask whatIsGround;

    public float groundCheckRadius;
    
    public float HorizontalDirection { get; private set; }
    public bool IsGrounded { get; private set; }
    public bool IsRunning { get; private set; } = false;
    public bool IsCrouching { get; private set; } = false;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        groundCheck = this.transform.Find("GroundCheck");
        combatController = GetComponent<BearmanCombat>();
        hitbox = GetComponent<CapsuleCollider2D>();
    }

    private void Update()
    {
        IsGrounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, whatIsGround);

        HorizontalDirection = GetInput().x;
        IsRunning = Input.GetKey(KeyCode.LeftShift);

        if (IsGrounded && Input.GetKeyDown(KeyCode.Space) && !combatController.IsAttacking)
        {
            Jump();
        }

        if (rb.velocity.y < 0)
        {
            rb.gravityScale = gravFallMultiplier;
        } else
        {
            rb.gravityScale = 1;
        }

        if (Input.GetKeyDown(KeyCode.LeftControl)) ChangeCrouchState();
        else if (Input.GetKeyUp(KeyCode.LeftControl)) ChangeCrouchState();
    }

    private void FixedUpdate()
    {
        // Give control to the player only if on the ground
        if (CanMove()) MoveCharacter();
        
    }

    private Vector2 GetInput()
    {
        return new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
    }

    // This function moves the character a certain amount depending on the difference between its current speed and its target speed.
    // The closer it is to maxSpeed the smaller the force it will be applied, the further it is the more force it will be applied.
    private void MoveCharacter()
    {
        if (IsCrouching) return;
        // The target speed is the max speed towards the facing direction
        float targetSpeed = HorizontalDirection * maxSpeed;

        // Set target speed to an interpolated value between the current speed and the target speed [a + (b – a) * t]
        targetSpeed = Mathf.Lerp(rb.velocity.x, targetSpeed, lerpAmount);

        // If shift is pressed increase the speed
        if (IsRunning)
        {
            targetSpeed *= runMultiplier;
        }

        // Get the difference between the target speed and the current speed
        float speedDiff = targetSpeed - rb.velocity.x;

        // The actual force of movement is the speed difference multiplied by the acceleration constant
        float movement = speedDiff * acceleration;

        rb.AddForce(movement * Vector2.right, ForceMode2D.Force);
    }

    private void Jump()
    {
        rb.AddForce(jumpForce * Vector2.up, ForceMode2D.Impulse);
    }

    public bool CanMove()
    {
        return IsGrounded;
    }

    private void ChangeCrouchState()
    {
        // If not crouching crouch and half the height of the hitbox 
        if (!IsCrouching)
        {
            IsCrouching = true;
            hitbox.size = new Vector2(hitbox.size.x, hitbox.size.y * 0.5f);
        }
        else
        {
            // If crouching stand up and double the size of the hitbox
            IsCrouching = false;
            hitbox.size = new Vector2(hitbox.size.x, hitbox.size.y * 2);
        }
    }
    
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.black;
        Gizmos.DrawWireSphere(groundCheck.position, groundCheckRadius);
    }
}