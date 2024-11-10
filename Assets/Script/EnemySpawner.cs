using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum DifficultyLevel
{ Easy, Medium, Hard }

public class EnemySpawner : MonoBehaviour
{
    public GameObject tankTrapPrefab; // Prefab for the single tank trap
    public GameObject mortarPrefab; // Prefab for mortars
    public GameObject enemyTankPrefab; // Prefab for enemy tanks
    public GameObject towerPrefab; // Prefab for towers

    public Transform centerPoint; // Central point for strategic enemy placement
    public Transform player; // Reference to the player in the scene
    public float spawnAreaSize = 50f; // Half the length and width of the square spawn area for tank traps

    // Difficulty settings
    public enum Difficulty { Easy, Medium, Hard }
    public DifficultyLevel currentDifficulty = DifficultyLevel.Easy;

    public List<Transform> towerPositions = new List<Transform>(); // List to store tower positions

    private int numberOfMortars;
    private int numberOfEnemyTanks;
    private int numberOfTowers;
    private float initialSpawnInterval;
    private float minimumSpawnInterval;
    private float difficultyIncreaseRate;

    void Start()
    {
        SetDifficultyParameters();
        SpawnTankTrapClusters();
        SpawnTowers();
        StartCoroutine(EnemyRedeploymentRoutine());
        
    }

    void SetDifficultyParameters()
    {
        // Set parameters based on the selected difficulty
        switch (currentDifficulty)
        {
            case DifficultyLevel.Easy:
                numberOfMortars = 2;
                numberOfEnemyTanks = 2;
                numberOfTowers = 1;
                initialSpawnInterval = 5f;
                minimumSpawnInterval = 2f;
                difficultyIncreaseRate = 0.05f;
                break;

            case DifficultyLevel.Medium:
                numberOfMortars = 3;
                numberOfEnemyTanks = 3;
                numberOfTowers = 2;
                initialSpawnInterval = 4f;
                minimumSpawnInterval = 1.5f;
                difficultyIncreaseRate = 0.1f;
                break;

            case DifficultyLevel.Hard:
                numberOfMortars = 4;
                numberOfEnemyTanks = 4;
                numberOfTowers = 3;
                initialSpawnInterval = 3f;
                minimumSpawnInterval = 1f;
                difficultyIncreaseRate = 0.15f;
                break;
        }
    }

   

    void SpawnTowers()
    {
        for (int i = 0; i < numberOfTowers; i++)
        {
            // Generate a random position within the spawn area for the tower
            float randomX = Random.Range(-spawnAreaSize, spawnAreaSize);
            float randomZ = Random.Range(-spawnAreaSize, spawnAreaSize);

            // Set the tower's spawn position
            Vector3 spawnPosition = new Vector3(randomX, 0f, randomZ);

            // Instantiate the tower prefab at the spawn position
            GameObject tower =Instantiate(towerPrefab, spawnPosition, Quaternion.identity);
            towerPositions.Add(tower.transform);

            // Add a component or script to the tower that calls RemoveTower when destroyed
            TowerHealth towerHealth = tower.GetComponent<TowerHealth>();
            if (towerHealth != null)
            {
                towerHealth.onTowerDestroyed += RemoveTower;
            }
        }
    }

    // Method to remove a tower from the list when it's destroyed
    public void RemoveTower(Transform tower)
    {
        if (towerPositions.Contains(tower))
        {
            towerPositions.Remove(tower);
        }
    }

    void SpawnMortarsAndTanks()
    {
        for (int i = 0; i < numberOfEnemyTanks; i++)
        {
            Vector3 spawnPosition = GetRandomPositionNearTowerOrFallback();
            GameObject enemyTank = Instantiate(enemyTankPrefab, spawnPosition, Quaternion.identity);

            EnemyTank enemyTankScript = enemyTank.GetComponent<EnemyTank>();
            if (enemyTankScript != null)
            {
                enemyTankScript.player = player;
            }
        }

        for (int i = 0; i < numberOfMortars; i++)
        {
            Vector3 spawnPosition = GetRandomPositionNearTowerOrFallback();
            GameObject mortar = Instantiate(mortarPrefab, spawnPosition, Quaternion.identity);

            MortarEnemy enemyMortarScript = mortar.GetComponent<MortarEnemy>();
            if (enemyMortarScript != null)
            {
                enemyMortarScript.player = player;
            }
        }
    }

    void SpawnTankTrapClusters()
    {
        int numberOfClusters = 5; // Number of clusters you want to spawn
        int trapsPerCluster = 7;  // Number of tank traps per cluster
        float clusterRadius = 15f; // Radius around the cluster's center where traps will spawn

        for (int i = 0; i < numberOfClusters; i++)
        {
            // Generate a central point for the cluster within the spawn area
            float clusterX = Random.Range(-spawnAreaSize, spawnAreaSize);
            float clusterZ = Random.Range(-spawnAreaSize, spawnAreaSize);
            Vector3 clusterCenter = new Vector3(clusterX, 0f, clusterZ);

            // Spawn tank traps around the central point
            for (int j = 0; j < trapsPerCluster; j++)
            {
                // Generate random positions within the cluster radius
                float angle = Random.Range(0, 360) * Mathf.Deg2Rad;
                float distance = Random.Range(0, clusterRadius);
                float trapX = clusterCenter.x + Mathf.Cos(angle) * distance;
                float trapZ = clusterCenter.z + Mathf.Sin(angle) * distance;
                Vector3 trapPosition = new Vector3(trapX, 0f, trapZ);

                // Instantiate the tank trap prefab at the calculated position
                Instantiate(tankTrapPrefab, trapPosition, Quaternion.identity);
            }
        }
    }

    Vector3 GetRandomPositionAroundCenter()
    {
        float radius = 30f; // Distance from the center point
        float angle = Random.Range(0, 360) * Mathf.Deg2Rad;
        float x = centerPoint.position.x + Mathf.Cos(angle) * radius;
        float z = centerPoint.position.z + Mathf.Sin(angle) * radius;
        return new Vector3(x, 0f, z);
    }

    Vector3 GetRandomPositionNearTowerOrFallback()
    {
        // Clean up null references in the towerPositions list
        towerPositions.RemoveAll(tower => tower == null);

        if (towerPositions.Count > 0)
        {
            Transform randomTower = towerPositions[Random.Range(0, towerPositions.Count)];
            float radius = 10f;
            float angle = Random.Range(0, 360) * Mathf.Deg2Rad;
            float offsetX = Mathf.Cos(angle) * radius;
            float offsetZ = Mathf.Sin(angle) * radius;
            return new Vector3(randomTower.position.x + offsetX, 0f, randomTower.position.z + offsetZ);
        }
        else
        {
            // Fallback: Generate a random position within the spawn area
            float randomX = Random.Range(-spawnAreaSize, spawnAreaSize);
            float randomZ = Random.Range(-spawnAreaSize, spawnAreaSize);
            return new Vector3(randomX, 0f, randomZ);
        }
    }

    public void OnDifficultyChanged(DifficultyLevel newDifficulty)
    {
        currentDifficulty = newDifficulty;
        // Update your spawn settings based on the new difficulty
        
        SetDifficultyParameters();
    }

    IEnumerator EnemyRedeploymentRoutine()
    {
        while (true)
        {
            yield return new WaitForSeconds(20f);

            // Spawn more mortars and tanks
            SpawnMortarsAndTanks();

            // Spawn additional towers
            SpawnTowers();

            // Spawn additional tank traps in clusters
            SpawnTankTrapClusters();
        }
    }
}
