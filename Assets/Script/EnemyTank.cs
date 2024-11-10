using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyTank : MonoBehaviour, IDamageBoostable
{
    public Transform player; // Reference to the player's transform
    public Transform turret; // Reference to the turret for aiming
    public Transform firePoint; // The point from which the projectile is fired

    public GameObject projectilePrefab; // The projectile prefab
    public float fireRate = 3f; // Time between shots in seconds
    public float projectileSpeed = 20f; // Speed of the projectile
    public float detectionRange = 50f; // Range within which the enemy can detect and fire at the player
    

    private float nextFireTime = 0f;
    private float damageBoost = 0f; //initial damage boost


    void Update()
    {
        if (player == null) return; // Ensure there is a player to target

        // Check if the player is within range
        float distanceToPlayer = Vector3.Distance(transform.position, player.position);
        if (distanceToPlayer <= detectionRange)
        {
            // Aim at the player
            Vector3 directionToPlayer = player.position - turret.position;
            directionToPlayer.y = 0; // Keep the turret rotation on the horizontal plane
            Quaternion targetRotation = Quaternion.LookRotation(directionToPlayer);

            //to fix truuet being off by 180
            targetRotation *= Quaternion.Euler(0, 180, 0); // Rotate 180 degrees around the Y-axis

            turret.rotation = Quaternion.Slerp(turret.rotation, targetRotation, Time.deltaTime * 5f);

            // Fire at the player intermittently
            if (Time.time >= nextFireTime)
            {
                nextFireTime = Time.time + fireRate;
                Fire();
            }
        }
    }

    void Fire()
    {
        // Instantiate and fire the projectile
        GameObject projectile = Instantiate(projectilePrefab, firePoint.position, firePoint.rotation);
        Rigidbody rb = projectile.GetComponent<Rigidbody>();
        if (rb != null)
        {
            rb.velocity = firePoint.forward * projectileSpeed;
        }

        // Set the damage of the projectile, taking into account any damage boost
        Projectiles projectileScript = projectile.GetComponent<Projectiles>();
        if (projectileScript != null)
        {
            float totalDamage = projectileScript.damage + damageBoost; // Calculate total damage
            projectileScript.SetDamage(totalDamage); // Apply the damage to the projectile
        }

    }

    public void ApplyDamageBoost(float boostAmount)
    {
        damageBoost = boostAmount;
    }

}
