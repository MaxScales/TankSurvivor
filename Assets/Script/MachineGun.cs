using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MachineGun : Weapon

{
   
    public Transform firePoint; // Where the bullets are spawned
    public GameObject bulletPrefab; // The bullet prefab
    public float bulletSpeed = 50f; // Speed of the bullets
    public int damage = 2;
    public float RPS = 10f;
    public float heatPerShot = 10f; // Heat generated per shot
    public float maxHeat = 100f; // Maximum heat before overheating
    public float cooldownRate = 5f; // How much heat is reduced per second when not firing
    public Slider heatBar; // Reference to the heat bar slider (optional for UI feedback)

    private float heatLevel = 0f; // Current heat level
    private bool isOverheated = false; // Whether the gun is overheated

    void Update()
    {
        // Cool down the gun over time
        if (heatLevel > 0f)
        {
            heatLevel -= cooldownRate * Time.deltaTime;
            heatLevel = Mathf.Max(heatLevel, 0f); // Ensure heat level does not go below 0
        }

        // Check if the gun is overheated
        if (heatLevel >= maxHeat)
        {
            isOverheated = true;
        }

        // If the gun has fully cooled down, it can be used again
        if (isOverheated && heatLevel <= 0f)
        {
            isOverheated = false;
        }

        // Update the heat bar UI 
        if (heatBar != null)
        {
            heatBar.value = heatLevel / maxHeat; // Update the heat bar
        }
    }

    public override void Fire()
    {
        // Check if it's time to fire
        if (!isOverheated && Time.time >= nextFireTime)
        {
            nextFireTime = Time.time + 1f / RPS;

            // Instantiate and fire a bullet
            GameObject bullet = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
            Rigidbody rb = bullet.GetComponent<Rigidbody>();
            if (rb != null)
            {
                rb.velocity = firePoint.forward * bulletSpeed;
            }
            heatLevel += heatPerShot;

            // Check if the gun has overheated as a result of this shot
            if (heatLevel >= maxHeat)
            {
                isOverheated = true;
            }
        }

    }

}