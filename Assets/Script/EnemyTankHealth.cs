using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyTankHealth : MonoBehaviour
{
    public float maxHealth = 100f; // Maximum health of the enemy tank
    private float currentHealth;
    public int PointsPerKill = 10;

    public Slider healthBar; // Reference to the UI Slider for the health bar

    void Start()
    {
        currentHealth = maxHealth; // Initialize health
        healthBar.maxValue = maxHealth;
        healthBar.value = currentHealth;
    }

    // Method to apply damage to the enemy tank
    public void TakeDamage(float damageAmount)
    {
        currentHealth -= damageAmount;
        healthBar.value = currentHealth; // Update the health bar

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    // Method to handle the enemy tank's death
    void Die()
    {

        // Award points for killing the enemy
        if (ScoreManager.instance != null)
        {
            ScoreManager.instance.AddPoints(PointsPerKill);
        }
        // Destroy the enemy tank or trigger a death animation
        Destroy(gameObject);
    }
}
