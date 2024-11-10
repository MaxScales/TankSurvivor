using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TowerHealth : MonoBehaviour
{
    public float maxHealth = 100f; // Maximum health of the tower
    private float currentHealth;
    public delegate void TowerDestroyed(Transform tower);
    public event TowerDestroyed onTowerDestroyed;

    public Slider healthBar; // Reference to a UI Slider for the health bar (optional)

    void Start()
    {
        currentHealth = maxHealth; // Initialize health
        if (healthBar != null)
        {
            healthBar.maxValue = maxHealth;
            healthBar.value = currentHealth;
        }
    }

    // Method to apply damage to the tower
    public void TakeDamage(float damageAmount)
    {
        currentHealth -= damageAmount;
        if (healthBar != null)
        {
            healthBar.value = currentHealth; // Update the health bar
        }

        if (currentHealth <= 0f)
        {
            Die();
        }
    }

    // Method to handle the tower's destruction
    void Die()
    {
        // Notify listeners (EnemySpawner) that the tower is destroyed
        if (onTowerDestroyed != null)
        {
            onTowerDestroyed(transform);
        }
        // Handle the tower's death (e.g., play an explosion effect, remove from game, etc.)
        Destroy(gameObject);
    }
}
