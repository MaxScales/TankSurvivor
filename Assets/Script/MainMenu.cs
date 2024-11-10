
using UnityEngine;
using UnityEngine.SceneManagement; // Required for loading scenes
using UnityEngine.UI;


public class MainMenu : MonoBehaviour
{

    public HighScoreManager highScoreManager; // Reference to your HighScoreManager script
    public string initialDifficulty; // To store the initial difficulty level
    public Text highScoreText;

    void Start()
    {
        // Load high scores and determine the initial difficulty level
        if (highScoreManager != null)
        {
            int highScore = highScoreManager.GetHighestScore();
            Debug.Log("Loaded High Score: " + highScore); // Check if this prints the correct score
            highScoreText.text = "High Score: " + highScore;
            initialDifficulty = highScoreManager.DetermineInitialDifficulty();
            
        }
    }
    // Play button is clicked
    public void PlayGame()
    {
        // Pass the initial difficulty setting to the game scene
        PlayerPrefs.SetString("InitialDifficulty", initialDifficulty);
        SceneManager.LoadScene("Assignment3"); 
    }

  

    // Quit button is clicked
    public void QuitGame()
    {
        Debug.Log("Game is quitting..."); // This will show in the console during testing
        Application.Quit(); // Quits the game (only works in a built version)
    }
}
