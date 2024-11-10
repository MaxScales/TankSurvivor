using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TankHealth : MonoBehaviour
{
    public float maxHealth = 100f; // Maximum health of the player tank
    private float currentHealth;
    public Slider healthBar; // Reference to the UI Slider for the health bar
    public float lowHealthThreshold = 30f; // Threshold for low health
    public float pulseSpeed = 2f; // Speed of the pulsing effect


    private bool isPulsing = false;


    void Start()
    {
        currentHealth = maxHealth; // Initialize health
        healthBar.maxValue = maxHealth;
        healthBar.value = currentHealth;
    }

    void Update()
    {
        // Check if the current health is below the low health threshold
        if (currentHealth <= lowHealthThreshold && !isPulsing)
        {
            StartCoroutine(PulseHealthBar());
        }
    }

    // Method to apply damage to the player tank
    public void TakeDamage(float damageAmount)
    {
        currentHealth -= damageAmount;
        healthBar.value = currentHealth; // Update the health bar

        if (currentHealth <= 0f)
        {
            Die();
        }
    }

    // Coroutine to pulse the health bar when health is low
    IEnumerator PulseHealthBar()
    {
        isPulsing = true;
        RectTransform healthBarRect = healthBar.GetComponent<RectTransform>();
        Image healthBarFill = healthBar.fillRect.GetComponent<Image>();

        while (currentHealth <= lowHealthThreshold)
        {
            // Make the health bar pulse by scaling it
            float scale = 1f + Mathf.Sin(Time.time * pulseSpeed) * 0.1f;
            healthBarRect.localScale = new Vector3(scale, scale, 1f);

            // Change the color to a pulsing red effect
            float alpha = Mathf.Abs(Mathf.Sin(Time.time * pulseSpeed));
            healthBarFill.color = new Color(1f, 0f, 0f, alpha);

            yield return null; // Wait for the next frame
        }

        // Reset the scale when pulsing stops
        healthBarRect.localScale = Vector3.one;
        isPulsing = false;
    }

    IEnumerator DieCoroutine()
    {
        // Handle the player's death (e.g., game over, respawn, etc.)
        ScoreManager.instance.GameOver();
        yield return new WaitForSeconds(2f);
        Destroy(gameObject);
    }
    

    // Method to handle the player tank's death
    void Die()
    {
        StartCoroutine(DieCoroutine());
    }

}
