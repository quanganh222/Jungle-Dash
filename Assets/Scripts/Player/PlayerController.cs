using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float jumpForce = 15f;
    private Rigidbody2D rb;
    private BoxCollider2D boxcollider;
    private CapsuleCollider2D capsulecollider;
    private bool isGrounded;

    [SerializeField] private Transform groundCheck;
    [SerializeField] private float groundCheckRadius = 0.2f;
    [SerializeField] private LayerMask groundLayer;
    private Animator ani;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        ani = GetComponent<Animator>();
        boxcollider = GetComponent<BoxCollider2D>();
        capsulecollider = GetComponent<CapsuleCollider2D>();

        capsulecollider.enabled = false;
    }

    private void Update()
    {
        isGrounded = CheckIfGrounded();
        HandleJump();
        HandleDuck();
    }

    private bool CheckIfGrounded()
    {
        if (groundCheck == null) return false;
        return Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, groundLayer);
    }

    private void OnDrawGizmosSelected()
    {
        if (groundCheck == null) return;

        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(groundCheck.position, groundCheckRadius);
    }

    private void HandleJump()
    {
        if (Keyboard.current.spaceKey.wasPressedThisFrame && isGrounded)
        {
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpForce);
        }
    }

    private void HandleDuck()
    {
        if (Keyboard.current.downArrowKey.isPressed)
        {
            boxcollider.enabled = false;
            capsulecollider.enabled = true;
            ani.SetBool("IsDuck", true);
        }
        else
        {
            boxcollider.enabled = true;
            capsulecollider.enabled = false;
            ani.SetBool("IsDuck", false);
        }
    }
}
