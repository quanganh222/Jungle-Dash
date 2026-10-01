using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement; // Bắt buộc phải có dòng này để load Scene

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    [SerializeField] private TextMeshProUGUI scoreText; 
    [SerializeField] private GameObject scoreTextObject;
    [SerializeField] private GameObject gameOver;        // Panel GameOver chứa 2 nút Restart & Exit

    [Header("Game Settings")]
    private float gameSpeed = 5f; 
    private float score = 0;      
    private bool isGameOver = false;

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
        score = 0;

        if (scoreTextObject != null) scoreTextObject.SetActive(true);
        if (gameOver != null) gameOver.SetActive(false); // Mới vào game thì ẩn GameOver panel
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
        score += Time.deltaTime * 10;
        if (scoreText != null) scoreText.text = "Score: " + Mathf.FloorToInt(score);
    }

    public float GetGameSpeed() => gameSpeed;
    public float GetScore() => score;

    // Gọi khi nhân vật chết
    public void GameOver() 
    {
        isGameOver = true;
        Time.timeScale = 0;

        if (scoreTextObject != null) scoreTextObject.SetActive(false);
        if (gameOver != null) gameOver.SetActive(true); 
    }

    // Gọi khi bấm nút Restart 
    public void RestartGame()
    {
        Time.timeScale = 1f; 
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    // Gọi khi bấm nút Exit
    public void ExitToMainMenu()
    {
        Time.timeScale = 1f; 
        SceneManager.LoadScene("MainMenu");
    }
}