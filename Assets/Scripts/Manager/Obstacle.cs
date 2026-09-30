using UnityEngine;

public class Obstacle : MonoBehaviour
{
    [SerializeField] private float leftBoundary = -10f;

    private void Update()
    {
        MoveObstacle();
    }

    // Hàm di chuyển chướng ngại vật sang trái và tự hủy khi ra khỏi màn hình
    private void MoveObstacle()
    {
        // Kiểm tra an toàn: Nếu chưa có GameManager.instance thì dừng để tránh lỗi Null Reference
        if (GameManager.instance == null) return;

        // Di chuyển chướng ngại vật sang trái dựa theo tốc độ chung của game từ GameManager
        transform.position += Vector3.left * GameManager.instance.GetGameSpeed() * Time.deltaTime;

        // Nếu vị trí X của vật thể nhỏ hơn mốc giới hạn bên trái -> Tự hủy GameObject này
        if (transform.position.x < leftBoundary)
        {
            Destroy(gameObject);
        }
    }
}
