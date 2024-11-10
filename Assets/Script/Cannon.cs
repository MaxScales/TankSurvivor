using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Cinemachine;

public class Cannon : Weapon
{
    public Transform firePoint; // The point from which the projectile will be fired
    public GameObject projectilePrefab; // The projectile prefab
    public float projectileSpeed = 20f; // Speed at which the projectile will be fired

    public float cooldownTime = 3f; // Cooldown time before the cannon can be fired again
    public Slider cooldownSlider; // Reference to the cooldown slider UI
    private CinemachineImpulseSource impulseSource; // Reference to the Cinemachine Impulse Source

    private bool isCooldown = false; // Tracks if the cannon is on cooldown
    private float cooldownTimer = 0f; // Timer for managing the cooldown

    void Start()
    {
        if (cooldownSlider != null)
        {
            cooldownSlider.maxValue = cooldownTime;
            cooldownSlider.value = cooldownTime; // Initialize the slider to show the cannon is ready
            impulseSource = GetComponent<CinemachineImpulseSource>();
        }
    }

    void Update()
    {
        if (isCooldown)
        {
            // Update the cooldown timer
            cooldownTimer -= Time.deltaTime;
            if (cooldownTimer <= 0f)
            {
                isCooldown = false;
                cooldownTimer = 0f; // Reset the timer
                if (cooldownSlider != null)
                {
                    cooldownSlider.value = cooldownTime; // Reset the slider
                }
            }
            else if (cooldownSlider != null)
            {
                // Update the slider to reflect the remaining cooldown time
                cooldownSlider.value = cooldownTime - cooldownTimer;
            }
        }
    }

    public override void Fire()
    {
        if (!isCooldown)
        {
            // Instantiate and fire the cannonball
            GameObject cannon = Instantiate(projectilePrefab, firePoint.position, firePoint.rotation);
            Rigidbody rb = cannon.GetComponent<Rigidbody>();
            if (rb != null)
            {
                rb.velocity = firePoint.forward * projectileSpeed;
            }

            // Start the cooldown
            isCooldown = true;
            cooldownTimer = cooldownTime; // Start the cooldown timer

            if (cooldownSlider != null)
            {
                cooldownSlider.value = 0; // Reset the slider to show the cooldown
            }

            // Trigger the camera shake
            if (impulseSource != null)
            {
                impulseSource.GenerateImpulse();
            }


        }
    }
}
