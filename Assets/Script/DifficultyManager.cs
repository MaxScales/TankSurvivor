using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DifficultyManager : MonoBehaviour
{
    public static DifficultyLevel currentDifficulty = DifficultyLevel.Easy; // Default difficulty
    public int scoreThresholdForIncrease = 500; // Example threshold for increasing difficulty
    public int scoreThresholdForDecrease = 200;  // Example threshold for decreasing difficulty

    private EnemySpawner enemySpawner;

    void Start()
    {
        enemySpawner = FindObjectOfType<EnemySpawner>(); // Get the EnemySpawner instance
    }


    public void EvaluatePerformance(int finalScore)
    {
        bool difficultyChanged = false;

        // Determine if difficulty should be increased or decreased
        if (finalScore >= scoreThresholdForIncrease)
        {
            IncreaseDifficulty();
            difficultyChanged = true;
        }
        else if (finalScore < scoreThresholdForDecrease)
        {
            DecreaseDifficulty();
            difficultyChanged = true;
        }

        // Notify the spawner if the difficulty has changed
        if (difficultyChanged && enemySpawner != null)
        {
            enemySpawner.OnDifficultyChanged(currentDifficulty);
        }
    }


    private void IncreaseDifficulty()
    {
        if (currentDifficulty == DifficultyLevel.Easy)
        {
            currentDifficulty = DifficultyLevel.Medium;
            Debug.Log("Difficulty increased to Medium.");
        }
        else if (currentDifficulty == DifficultyLevel.Medium)
        {
            currentDifficulty = DifficultyLevel.Hard;
            Debug.Log("Difficulty increased to Hard.");
        }
    }

    private void DecreaseDifficulty()
    {
        if (currentDifficulty == DifficultyLevel.Hard)
        {
            currentDifficulty = DifficultyLevel.Medium;
            Debug.Log("Difficulty decreased to Medium.");
        }
        else if (currentDifficulty == DifficultyLevel.Medium)
        {
            currentDifficulty = DifficultyLevel.Easy;
            Debug.Log("Difficulty decreased to Easy.");
        }
    }
}
