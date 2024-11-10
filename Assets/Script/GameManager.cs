using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    void Start()
    {
        // Retrieve the initial difficulty level set in the main menu
        string initialDifficulty = PlayerPrefs.GetString("InitialDifficulty", "Easy"); // Default to "Easy" if not set

        // Apply the difficulty settings
        SetDifficulty(initialDifficulty);
    }

    void SetDifficulty(string difficulty)
    {
        switch (difficulty)
        {
            case "Easy":
                // Set easy difficulty parameters
                Debug.Log("Easy difficulty applied");
                break;
            case "Medium":
                // Set medium difficulty parameters
                Debug.Log("Medium difficulty applied");
                break;
            case "Hard":
                // Set hard difficulty parameters
                Debug.Log("Hard difficulty applied");
                break;
            default:
                Debug.Log("Unknown difficulty level");
                break;
        }
    }
}
