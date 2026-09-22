using UnityEngine;

public class GroundLoop : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 5f;
    [SerializeField] private float groundWidth = 20f;

    void Update()
    {
        // để mặt dất di chuyển sang bên trái 
        transform.Translate(Vector2.left * moveSpeed * Time.deltaTime);
        
        // Nếu mặt đất đã chạy hết sang bên  trái
        if (transform.position.x <= -groundWidth)
        {
            // đưa nó sang phía bên phải 
            transform.position += Vector3.right * groundWidth * 2f; 
        }
    }
}
