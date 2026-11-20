
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public enum GameState
    {
        Playing,
        Paused,
        GameOver
    }

    [Header("UI References")]
    [SerializeField] private TMP_Text ScoreUI;
    [SerializeField] private TMP_Text CoinUI;
    [SerializeField] private GameObject gameOverPanel;

    [Header("Game Over UI")]
    [SerializeField] private TMP_Text finalScoreText;
    [SerializeField] private TMP_Text coinsCollectedText;
    [SerializeField] private TMP_Text bestScoreText;

    [Header("Score Settings")]
    [SerializeField] private float scoreIncreaseRate = 10f;

    private float elapsedTime;
    private int coins;

    private const string BEST_SCORE_KEY = "RoyalRun_BestScore";

    public GameState CurrentState { get; private set; } = GameState.Playing;

    public bool IsGameRunning => CurrentState == GameState.Playing;

    public int CurrentScore => Mathf.FloorToInt(elapsedTime * scoreIncreaseRate);

    public int Coins => coins;

    private void Start()
    {
        Time.timeScale = 1f;

        if (gameOverPanel != null)
            gameOverPanel.SetActive(false);

        UpdateScoreUI();
        UpdateCoinUI();
    }

    private void Update()
    {
        if (!IsGameRunning)
            return;

        elapsedTime += Time.deltaTime;
        UpdateScoreUI();
    }

    private void UpdateScoreUI()
    {
        if (ScoreUI != null)
            ScoreUI.text = "Score: " + CurrentScore;
    }

    private void UpdateCoinUI()
    {
        if (CoinUI != null)
            CoinUI.text = "Coin: " + coins;
    }

    public void UpdateCoin(int amount)
    {
        if (!IsGameRunning)
            return;

        coins += amount;
        UpdateCoinUI();
    }

    public void PauseGame()
    {
        if (CurrentState != GameState.Playing)
            return;

        CurrentState = GameState.Paused;
        Time.timeScale = 0f;
    }

    public void ResumeGame()
    {
        if (CurrentState != GameState.Paused)
            return;

        CurrentState = GameState.Playing;
        Time.timeScale = 1f;
    }

    public void EndGame()
    {
        if (CurrentState == GameState.GameOver)
            return;

        CurrentState = GameState.GameOver;

        // Play Game Over sound
        if (AudioManager.Instance != null)
        {
            AudioManager.Instance.PlayGameOver();
        }

        int finalScore = CurrentScore;
        int bestScore = PlayerPrefs.GetInt(BEST_SCORE_KEY, 0);

        if (finalScore > bestScore)
        {
            bestScore = finalScore;
            PlayerPrefs.SetInt(BEST_SCORE_KEY, bestScore);
            PlayerPrefs.Save();
        }

        if (finalScoreText != null)
            finalScoreText.text = "Final Score: " + finalScore;

        if (coinsCollectedText != null)
            coinsCollectedText.text = "Coins Collected: " + coins;

        if (bestScoreText != null)
            bestScoreText.text = "Best Score: " + bestScore;

        if (gameOverPanel != null)
            gameOverPanel.SetActive(true);

        Time.timeScale = 0f;

        Debug.Log("GAME OVER! Final Score: " + finalScore);
    }

    public void RestartGame()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("GamePlay");
    }

    public void GoToMainMenu()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("MainMenu");
    }
}
