using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Cinemachine;


public class MissileLauncher : Weapon

{
    public Transform firePoint; // Where the missile is spawned
    public GameObject missilePrefab; // The missile prefab
    public int numberOfMissiles = 4;
    public float cooldown = 5f;
    public float missileSpeed = 20f; // Speed of the missile
    public float damage = 50;
    public float explosionRadius = 5f;
    

    public Slider cooldownSlider;

    private bool isCooldown = false; // Tracks if the launcher is on cooldown
    private float cooldownTimer = 0f; // Time when the launcher can fire again
    private CinemachineImpulseSource impulseSource; // Reference to the Cinemachine Impulse Source


    void Start()
    {
        if (cooldownSlider != null)
        {
            cooldownSlider.maxValue = cooldown;
            cooldownSlider.value = cooldown; // Initialize slider to full
        }

        impulseSource = GetComponent<CinemachineImpulseSource>();
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
                    cooldownSlider.value = cooldown; // Reset the slider
                }
            }
            else if (cooldownSlider != null)
            {
                // Update the slider to reflect the remaining cooldown time
                cooldownSlider.value = cooldown - cooldownTimer;
            }
        }
    }


    // Implementation of the Fire method
    public override void Fire()
    {
        if (!isCooldown)
        {
            StartCoroutine(FireMissiles());
            isCooldown = true;
            cooldownTimer = cooldown; // Start the cooldown timer

            if (cooldownSlider != null)
            {
                cooldownSlider.value = 0; // Reset the slider to show the cooldown
            }

            
        }
    }



    private IEnumerator FireMissiles()
    {
        for (int i = 0; i < numberOfMissiles; i++)
        {
            // Instantiate and fire the missile
            GameObject missile = Instantiate(missilePrefab, firePoint.position, firePoint.rotation);
            Rigidbody rb = missile.GetComponent<Rigidbody>();
            if (rb != null)
            {
                rb.velocity = firePoint.forward * missileSpeed;
            }

            // Trigger the camera shake
            if (impulseSource != null)
            {
                impulseSource.GenerateImpulse();
            }

            yield return new WaitForSeconds(fireRate); // Wait before firing the next missile
        }
    }
}
