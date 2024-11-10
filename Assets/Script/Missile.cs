using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Missile : MonoBehaviour
{
    public float damage = 50f; // Damage dealt by the missile
    public float explosionRadius = 5f; // Radius of the area effect

    // Method to set the damage value
    public void SetDamage(float newDamage)
    {
        damage = newDamage; // Update the damage
    }

    void OnCollisionEnter(Collision collision)
    {
       

        // Apply area damage
        ApplyAreaDamage();

        // Destroy the missile
        Destroy(gameObject);
    }

    void ApplyAreaDamage()
    {
        // Find all nearby colliders within the explosion radius
        Collider[] colliders = Physics.OverlapSphere(transform.position, explosionRadius);

        foreach (Collider nearbyObject in colliders)
        {

          

            // Check if the object hit has a TankHealth component
            TankHealth tankHealth = nearbyObject.GetComponent<TankHealth>();
            if (tankHealth != null)
            {
                tankHealth.TakeDamage(damage); // Apply damage to the tank
            }
            // Check if the nearby object has a health component
            EnemyTankHealth enemyHealth = nearbyObject.GetComponent<EnemyTankHealth>();
            if (enemyHealth != null)
            {
                enemyHealth.TakeDamage(damage); // Apply damage to the enemy
            }

           
        }
    }

    
}
