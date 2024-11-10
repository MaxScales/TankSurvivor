using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MortarEnemy : MonoBehaviour, IDamageBoostable
{
    public Transform firePoint; // The point from which the mortar projectile is launched
    public GameObject projectilePrefab; // The mortar projectile prefab
    public float fireInterval = 5f; // Time between shots
    public float launchForce = 10f; // Initial force applied to the projectile
    public Transform player; // Reference to the player to target
    public float launchAngle = 45f; // Launch angle in degrees
    
    
    private float fireTimer = 0f; // Timer to track when to fire next
    private float damageBoost = 0f; //initial damage boost
    

    void Update()
    {
        fireTimer += Time.deltaTime;

        if (fireTimer >= fireInterval)
        {
            FireMortar();
            fireTimer = 0f; // Reset the timer
        }

       
    }

    void FireMortar()
    {
        if (player == null) return; // Ensure we have a target

        // Calculate the direction and distance to the player
        Vector3 targetPosition = player.position;
        Vector3 direction = targetPosition - firePoint.position;
        float distance = direction.magnitude;

        // Set a fixed launch angle (in degrees)
        float launchAngle = 45f;
        float gravity = Mathf.Abs(Physics.gravity.y); // Get the magnitude of gravity

        // Calculate the launch velocity based on the distance and angle
        float launchVelocity = Mathf.Sqrt(distance * gravity / Mathf.Sin(2 * launchAngle * Mathf.Deg2Rad));

        // Set up the launch direction with the fixed angle
        Vector3 launchDirection = direction.normalized;
        launchDirection.y = Mathf.Tan(launchAngle * Mathf.Deg2Rad);

        // Adjust launch direction with calculated velocity
        launchDirection = launchDirection.normalized * launchVelocity;

        // Instantiate and launch the projectile
        GameObject projectile = Instantiate(projectilePrefab, firePoint.position, Quaternion.identity);
        Rigidbody rb = projectile.GetComponent<Rigidbody>();
        if (rb != null)
        {
            rb.velocity = launchDirection; // Set the velocity for a parabolic trajectory
            
            // Ignore collision between the projectile and the object that fired it
            Collider projectileCollider = projectile.GetComponent<Collider>();
            Collider firePointCollider = firePoint.GetComponent<Collider>();

            if (projectileCollider != null && firePointCollider != null)
            {
                Physics.IgnoreCollision(projectileCollider, firePointCollider);
            }
        }

        // Set the damage of the projectile, taking into account any damage boost
        Missile MissileScript = projectile.GetComponent<Missile>();
        if (MissileScript != null)
        {
            float totalDamage = MissileScript.damage + damageBoost; // Calculate total damage
            MissileScript.SetDamage(totalDamage); // Apply the damage to the projectile
        }
    }

   



    public void ApplyDamageBoost(float boostAmount)
    {
        damageBoost = boostAmount;
    }

}
