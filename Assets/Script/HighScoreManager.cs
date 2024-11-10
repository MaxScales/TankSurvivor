using System.IO;
using System.Collections.Generic;
using UnityEngine;

public class HighScoreManager : MonoBehaviour
{
    public static HighScoreManager instance; // Singleton instance
    private string filePath;
    public HighScoreList highScoreList = new HighScoreList();

    void Awake()
    {
        // Implement singleton pattern
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject); // Persist across scenes
        }
        else
        {
            Destroy(gameObject); // Destroy duplicate instances
            return;
        }

        filePath = Path.Combine(Application.persistentDataPath, "highscore.json");
        LoadHighScores();
    }

    // Save a new high score
    public void SaveHighScore(int score)
    {
        HighScoreEntry newEntry = new HighScoreEntry { score = score, date = System.DateTime.Now.ToString() };
        highScoreList.highScores.Add(newEntry);

        // Save the updated high score list to a file
        string json = JsonUtility.ToJson(highScoreList);
        File.WriteAllText(filePath, json);

        
    }

    // Load high scores from the file
    public void LoadHighScores()
    {
        if (File.Exists(filePath))
        {
            string json = File.ReadAllText(filePath);
            highScoreList = JsonUtility.FromJson<HighScoreList>(json) ?? new HighScoreList();
        }
        else
        {
            highScoreList = new HighScoreList();
        }
    }

    // Analyze high scores to determine initial difficulty
    public string DetermineInitialDifficulty()
    {
        if (highScoreList.highScores.Count == 0)
            return "Easy"; // Default difficulty if no scores are available

        // Calculate average score
        int totalScore = 0;
        foreach (var entry in highScoreList.highScores)
        {
            totalScore += entry.score;
        }
        float averageScore = totalScore / highScoreList.highScores.Count;

        // Check for score consistency
        float scoreVariance = 0f;
        foreach (var entry in highScoreList.highScores)
        {
            scoreVariance += Mathf.Pow(entry.score - averageScore, 2);
        }
        scoreVariance /= highScoreList.highScores.Count;

        // Determine difficulty based on average score and variance
        if (averageScore > 80 && scoreVariance < 100) // Example thresholds
        {
            return "Hard";
        }
        else if (averageScore > 50)
        {
            return "Medium";
        }
        else
        {
            return "Easy";
        }
    }

    public int GetHighestScore()
    {
        if (highScoreList.highScores.Count == 0)
            return 0; // Return 0 if there are no scores

        int highestScore = 0;
        foreach (var entry in highScoreList.highScores)
        {
            if (entry.score > highestScore)
                highestScore = entry.score;
        }

        return highestScore;
    }

    [System.Serializable]
    private class HighScoreData
    {
        public int highScore;
        public string date;
    }

    [System.Serializable]
    public class HighScoreList
    {
        public List<HighScoreEntry> highScores = new List<HighScoreEntry>();
    }

}
