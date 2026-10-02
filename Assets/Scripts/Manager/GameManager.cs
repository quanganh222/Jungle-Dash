using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement; 

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    [SerializeField] private TextMeshProUGUI scoreText;       
    [SerializeField] private TextMeshProUGUI highScoreText;   
    [SerializeField] private GameObject gameOver;            

    private float gameSpeed = 5f; 
    private float score = 0f;
    private static int sessionHighScore = 0; 
    
    private bool isGameOver = false;


    [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
    private static void ResetDataOnGameLaunch()
    {
        // 1. Đưa điểm kỷ lục phiên chơi về 0
        sessionHighScore = 0;

        // 2. Xóa sạch file lưu số 105 cũ dính trên ổ cứng máy tính
        if (PlayerPrefs.HasKey("HighScore"))
        {
            PlayerPrefs.DeleteKey("HighScore");
            PlayerPrefs.Save();
        }
    }

    private void Awake()
    {
        if (instance == null) instance = this;
        else Destroy(gameObject);
    }

    private void Start()
    {
        StartPlay();
    }

    private void Update()
    {
        if (!isGameOver)
        {
            UpdateGameSpeed();
            UpdateScore();     
        }
    }

    public void StartPlay()
    {
        Time.timeScale = 1f; 
        isGameOver = false;
        score = 0f;

        // Bật và hiển thị Score: 000000 ban đầu
        if (scoreText != null) 
        {
            scoreText.gameObject.SetActive(true);
            scoreText.text = "Score: " + 0.ToString("D6");
        }

        // Bật và hiển thị High Score hiện tại (Mới mở game sẽ là HI: 000000)
        if (highScoreText != null) 
        {
            highScoreText.gameObject.SetActive(true);
            UpdateHighScoreUI();
        }

        if (gameOver != null) gameOver.SetActive(false); 
    }

    private void UpdateGameSpeed()
    {
        if (score < 100) gameSpeed = 5f;
        else if (score < 300) gameSpeed = Mathf.Lerp(5f, 8f, (score - 100) / 200f);
        else if (score < 600) gameSpeed = Mathf.Lerp(8f, 12f, (score - 300) / 300f);
        else gameSpeed = 15f;
    }

    private void UpdateScore()
    {
        score += Time.deltaTime * 10f;
        int currentScore = Mathf.FloorToInt(score);

        if (scoreText != null) scoreText.text = "Score: " + currentScore.ToString("D6");

        // Nếu điểm lượt chơi này cao hơn High Score -> Cập nhật High Score
        if (currentScore > sessionHighScore)
        {
            sessionHighScore = currentScore;
            UpdateHighScoreUI();
        }
    }

    private void UpdateHighScoreUI()
    {
        if (highScoreText != null)
        {
            highScoreText.text = "HI: " + sessionHighScore.ToString("D6");
        }
    }

    public float GetGameSpeed() => gameSpeed;
    public float GetScore() => score;

    public void GameOver() 
    {
        isGameOver = true;
        Time.timeScale = 0f;

        if (scoreText != null) scoreText.gameObject.SetActive(false);
        if (highScoreText != null) highScoreText.gameObject.SetActive(false);
        if (gameOver != null) gameOver.SetActive(true); 
    }

    // Khi chết bấm Restart -> Load lại Scene nhưng High Score VẪN GIỮ ĐƯỢC
    public void RestartGame()
    {
        Time.timeScale = 1f; 
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void ExitToMainMenu()
    {
        Time.timeScale = 1f; 
        SceneManager.LoadScene("MainMenu");
    }
}