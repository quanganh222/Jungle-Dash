using UnityEngine;

public class GroundLoop : MonoBehaviour
{
    [SerializeField] private float parallaxFactor = 0.01f; // Tỉ lệ cuộn nền so với tốc độ game
    private Material material; // Tham chiếu tới Material của MeshRenderer
    private float offset;         // Tọa độ cuộn tích lũy của Texture

    private void Start()
    {
        // Lấy Component MeshRenderer được gắn trên GameObject này
        MeshRenderer meshRenderer = GetComponent<MeshRenderer>();
        if (meshRenderer != null)
        {
            material = meshRenderer.material;
        }
    }

    private void Update()
    {
        ParallaxScroll();
    }

    private void ParallaxScroll()
    {
        // Kiểm tra an toàn: Nếu thiếu Material hoặc GameManager chưa sẵn sàng thì dừng lại để tránh văng lỗi Null
        if (material == null || GameManager.instance == null) return;
        // Tính tốc độ cuộn nền dựa trên tốc độ chung của game và hệ số parallaxFactor
        float speed = GameManager.instance.GetGameSpeed() * parallaxFactor;
        // Tích lũy quãng đường cuộn qua từng khung hình
        offset += Time.deltaTime * speed;
        // Dịch chuyển Texture sang trái (Vector2.left) tạo cảm giác nhân vật đang tiến về phía trước
        material.SetTextureOffset("_MainTex", Vector2.left * offset);
    }
}