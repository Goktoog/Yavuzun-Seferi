using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class CharacterController2D : MonoBehaviour
{
    [SerializeField] private JumpButtonHandler jumpButtonHandler;
    private Animator animator;

    [Header("Character Settings")]
    [SerializeField] private float moveSpeed = 5f;
    [SerializeField] private float jumpForce = 12f;
    [SerializeField] private float climbSpeed = 3f;
    [SerializeField] private GameObject jumpParticleEffect;
    [SerializeField] private AudioSource jumpSFX;

    [SerializeField] private Transform groundCheck;
    [SerializeField] private float groundCheckRadius = 0.2f;
    [SerializeField] private LayerMask groundLayer;

    [SerializeField] private Joystick joystick;
    [SerializeField] private Button jumpButton;

    private Rigidbody2D rb;
    private bool isfacingRight = true;
    private bool isGrounded;
    [SerializeField] private bool isOnLadder = false;
    private bool isClimbing = false;
    private bool isJumping = false;

    // Tırmanma sınırını ekliyoruz
    [SerializeField] private float maxClimbHeight = 10f;  // Merdivenin tırmanabileceği maksimum yükseklik
    private float climbStartY = 0f;  // Merdivene başlama yüksekliği

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, groundLayer);

        if (jumpButtonHandler != null && jumpButtonHandler.jumpRequested)
        {
            JumpOrClimb();
            jumpButtonHandler.jumpRequested = false;
        }
        else if (jumpButtonHandler != null && jumpButtonHandler.isButtonHeld)  // Buton basılı tutulduğunda
        {
            if (isOnLadder && transform.position.y < climbStartY + maxClimbHeight)  // Yükseklik kontrolü ekledik
            {
                isClimbing = true;
                rb.gravityScale = 0f;
            }
        }
        else
        {
            isClimbing = false;
            rb.gravityScale = 1f;
        }
    }

    void FixedUpdate()
    {
        float moveInput = joystick != null ? joystick.Horizontal : Input.GetAxisRaw("Horizontal");

        if (isClimbing)
        {
            rb.linearVelocity = new Vector2(moveInput * moveSpeed, climbSpeed);
        }
        else
        {
            rb.linearVelocity = new Vector2(moveInput * moveSpeed, rb.linearVelocity.y);
        }

        if (animator != null)
            animator.SetFloat("Speed", Mathf.Abs(moveInput));

        if (moveInput > 0 && !isfacingRight)
        {
            Flip();
        }
        else if (moveInput < 0 && isfacingRight)
        {
            Flip();
        }
    }

    private void Flip()
    {
        isfacingRight = !isfacingRight;
        Vector3 scale = transform.localScale;
        scale.x *= -1;
        transform.localScale = scale;
    }

    void JumpOrClimb()
    {
        if (isOnLadder && !isJumping)
        {
            isClimbing = true;
        }
        else if (isGrounded && !isOnLadder)
        {
            if (jumpSFX != null)
            {
                jumpSFX.Play();
            }
            Instantiate(jumpParticleEffect, transform.position, Quaternion.identity);
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpForce);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Ladder"))
        {
            isOnLadder = true;
            climbStartY = transform.position.y;  // Merdivene başlama yüksekliğini kaydet
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Ladder"))
        {
            isOnLadder = false;
            isClimbing = false;  // Merdivenden çıkıldığında tırmanmayı durdur
            rb.gravityScale = 1f;  // Yerin çekim kuvvetini geri getir
        }
    }

    private void OnDrawGizmos()
    {
        if (groundCheck != null)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(groundCheck.position, groundCheckRadius);
        }
    }
}
