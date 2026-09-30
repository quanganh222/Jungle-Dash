using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float jumpForce = 15f; // Lực nhảy của nhân vật
    [SerializeField] private Transform groundCheck;         // Vị trí đặt điểm kiểm tra dưới chân nhân vật
    [SerializeField] private float groundCheckRadius = 0.2f; // Bán kính vòng tròn kiểm tra đất
    [SerializeField] private LayerMask groundLayer;         // Layer đại diện cho mặt đất

    // Khai báo các Component
    private Rigidbody2D rb;
    private BoxCollider2D boxcollider;           // Collider dùng khi ĐỨNG
    private CapsuleCollider2D capsulecollider;   // Collider dùng khi CÚI NGƯỜI
    private Animator ani;

    // Biến trạng thái
    private bool isGrounded;

    private void Start()
    {
        // Lấy tham chiếu đến các Component được gắn trên GameObject
        rb = GetComponent<Rigidbody2D>();
        ani = GetComponent<Animator>();
        boxcollider = GetComponent<BoxCollider2D>();
        capsulecollider = GetComponent<CapsuleCollider2D>();

        // Mặc định ban đầu nhân vật đứng thẳng nên tắt Collider cúi người
        capsulecollider.enabled = false;
    }

    private void Update()
    {
        // Cập nhật trạng thái kiểm tra chạm đất liên tục mỗi frame
        isGrounded = CheckIfGrounded();

        // Xử lý hành động Nhảy và Cúi
        HandleJump();
        HandleDuck();
    }

    // Hàm kiểm tra nhân vật có đang đứng trên mặt đất hay không
    private bool CheckIfGrounded()
    {
        if (groundCheck == null) return false;

        // Tạo một vòng tròn ảo tại vị trí groundCheck để xem có đè lên Layer mặt đất không
        return Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, groundLayer);
    }

    // Vẽ bán kính kiểm tra đất trong Scene (chỉ hiển thị trong Editor để dễ căn chỉnh)
    private void OnDrawGizmosSelected()
    {
        if (groundCheck == null) return;

        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(groundCheck.position, groundCheckRadius);
    }

    // Xử lý logic Nhảy
    private void HandleJump()
    {
        // Nhấn phím Space và phải đang ở trên mặt đất thì mới được nhảy
        if (Keyboard.current.spaceKey.wasPressedThisFrame && isGrounded)
        {
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpForce);
        }
    }

    // Xử lý logic Cúi người
    private void HandleDuck()
    {
        // Khi GIỮ phím Mũi tên xuống
        if (Keyboard.current.downArrowKey.isPressed)
        {
            boxcollider.enabled = false;        // Tắt collider đứng
            capsulecollider.enabled = true;     // Bật collider cúi (thu nhỏ hitbox)
            ani.SetBool("IsDuck", true);         // Kích hoạt Animation cúi
        }
        else // Khi THẢ phím Mũi tên xuống (trở về trạng thái bình thường)
        {
            boxcollider.enabled = true;         // Bật lại collider đứng
            capsulecollider.enabled = false;    // Tắt collider cúi
            ani.SetBool("IsDuck", false);        // Tắt Animation cúi
        }
    }
}
