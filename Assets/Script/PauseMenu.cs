using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class PauseMenu : MonoBehaviour
{
    public static bool isPaused = false; // Boolean to check if the game is paused
    public GameObject pauseMenuUI; // Reference to the Pause Menu UI

    void Update()
    {
        // Check for the pause key (Escape or "P" for example)
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (isPaused)
            {
                Resume();
            }
            else
            {
                Pause();
            }
        }
    }

    public void Resume()
    {
        pauseMenuUI.SetActive(false); // Hide the pause menu
        Time.timeScale = 1f; // Resume the game
        isPaused = false;
    }

    public void Pause()
    {
        pauseMenuUI.SetActive(true); // Show the pause menu
        Time.timeScale = 0f; // Pause the game
        isPaused = true;
    }

    public void RestartGame()
    {
        Time.timeScale = 1f; // Resume the game before reloading the scene
        SceneManager.LoadScene(SceneManager.GetActiveScene().name); // Reload the current scene
    }

    public void QuitGame()
    {
        Time.timeScale = 1f; // Resume the game before quitting
        Application.Quit(); // Quit the game
       
    }
}
