using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    [SerializeField] private TextMeshProUGUI scoreText; // Component hiển thị điểm số trên màn hình
    private float gameSpeed = 5f; // Tốc độ di chuyển hiện tại của game
    private float score = 0;      // Điểm số tích lũy

    private void Awake()
    {
        // Khởi tạo Singleton
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject); // Xóa bản sao thừa nếu đã tồn tại GameManager
        }
    }

    private void Update()
    {
        UpdateGameSpeed(); // Cập nhật tốc độ game theo các giai đoạn điểm
        UpdateScore();     // Tính và cập nhật điểm số liên tục
    }

    // Hàm cập nhật tốc độ game dựa theo điểm số hiện tại
    private void UpdateGameSpeed()
    {
        if (score < 100)
        {
            gameSpeed = 5f; // Giai đoạn 1: Tốc độ chậm ban đầu
        }
        else if (score < 300)
        {
            // Giai đoạn 2: Tăng tốc mượt từ 5 đến 8
            gameSpeed = Mathf.Lerp(5f, 8f, (score - 100) / 200f);
        }
        else if (score < 600)
        {
            // Giai đoạn 3: Tăng tốc mượt từ 8 đến 12
            gameSpeed = Mathf.Lerp(8f, 12f, (score - 300) / 300f);
        }
        else
        {
            gameSpeed = 15f; // Giai đoạn 4: Tốc độ tối đa
        }
    }

    // Hàm tính điểm và cập nhật lên UI
    private void UpdateScore()
    {
        score += Time.deltaTime * 10; // Tăng điểm theo thời gian

        // Kiểm tra an toàn: chỉ cập nhật chữ khi đã kéo thả Text UI vào Inspector
        if (scoreText != null)
        {
            scoreText.text = "Score: " + Mathf.FloorToInt(score);
        }
    }
    // Lấy tốc độ game hiện tại
    public float GetGameSpeed()
    {
        return gameSpeed;
    }   

    // Lấy điểm số hiện tại
    public float GetScore()
    {
        return score;
    }
}
