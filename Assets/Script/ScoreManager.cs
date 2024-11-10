using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class ScoreManager : MonoBehaviour
{
    public static ScoreManager instance; // Singleton instance

    public float score = 0f; // Player's score
    public float pointsPerSecond = 1f; // Points awarded per second for survival
    public Text scoreText; // UI Text element to display the current games score 
    public Text highScoreText; // UI Text element to display the high score
    public Text timerText;
    public HighScoreManager highScoreManager; // Reference to HighScoreManager

    public GameObject gameOverCanvas; // Reference to the Game Over Canvas
    public Text gameOverScoreText; // Text element to display the score on the game over screen
    public Text gameOverHighScoreText; // Text element to display the high score on the game over screen

    public int finalScore; // Track the player's final score
    public DifficultyManager difficultyManager; // Reference to the DifficultyManager

    private int highScore = 0;
    private float timeElapsed = 0f; // Time elapsed since the round started
    private bool timerIsRunning = true;

    void Awake()
    {
        // Implement singleton pattern to ensure only one ScoreManager exists
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {
        // Load the high score from HighScoreManager
        if (highScoreManager != null)
        {
            highScore = highScoreManager.GetHighestScore();
            if (highScoreText != null)
            {
                highScoreText.text = "High Score: " + highScore;
            }
        }
    }

    void Update()
    {
        // Ensure that the timer is running and Time.timeScale is not zero
        if (timerIsRunning && Time.timeScale > 0f)
        {
            // Award points for the duration the player survives
            score += pointsPerSecond * Time.deltaTime;
            timeElapsed += Time.deltaTime;
            UpdateTimerText();
            
            // Update the score display
            if (scoreText != null)
            {
                scoreText.text = "Score: " + Mathf.FloorToInt(score).ToString();
            }

            // Check if the current score is higher than the high score
            if (score > highScore)
            {
                highScore = Mathf.FloorToInt(score);
                if (highScoreText != null)
                {
                    highScoreText.text = "High Score: " + highScore;
                }
            }
        }
    }

    // Method to add points when an enemy is killed
    public void AddPoints(float points)
    {
        score += points;
        if (scoreText != null)
        {
            scoreText.text = "Score: " + Mathf.FloorToInt(score).ToString();
        }

        // Check if the current score is higher than the high score
        if (score > highScore)
        {
            highScore = Mathf.FloorToInt(score);
            if (highScoreText != null)
            {
                highScoreText.text = "High Score: " + highScore;
            }
        }
    }

    // Save the high score using HighScoreManager
    public void SaveHighScore()
    {
        if (highScoreManager != null)
        {
            highScoreManager.SaveHighScore(highScore); // Use HighScoreManager to save
        }
    }

    public void GameOver()
    {
        // Show the Game Over Canvas
        gameOverCanvas.SetActive(true);

        // Display the scores on the game over screen
        gameOverScoreText.text = "Score: " + Mathf.FloorToInt(score);
        gameOverHighScoreText.text = "High Score: " + highScore;

        // Save the high score using HighScoreManager
        if (highScoreManager != null)
        {
            highScoreManager.SaveHighScore(highScore);
        }

        if (difficultyManager != null)
        {
            difficultyManager.EvaluatePerformance(finalScore);
        }

        // Stop the game
        Time.timeScale = 0f; // This pauses the game
    }

    public void RestartGame()
    {
        // Restart the game by reloading the scene
        Time.timeScale = 1f; // Resume the game
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void QuitGame()
    {
        // Quit the game
        
        Application.Quit();
    }

    // Method to update the timer text in the UI
    void UpdateTimerText()
    {
        int minutes = Mathf.FloorToInt(timeElapsed / 60); // Calculate minutes
        int seconds = Mathf.FloorToInt(timeElapsed % 60); // Calculate seconds
        timerText.text = string.Format("{0:00}:{1:00}", minutes, seconds); // Format as MM:SS
    }

    // Method to start or reset the timer
    public void StartTimer()
    {
        timeElapsed = 0f; // Reset the elapsed time
        timerIsRunning = true;
    }

    // Method to pause the timer
    public void PauseTimer()
    {
        timerIsRunning = false;
    }

    // Method to get the elapsed time
    public float GetElapsedTime()
    {
        return timeElapsed;
    }
}
