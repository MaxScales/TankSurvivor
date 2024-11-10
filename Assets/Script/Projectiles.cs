using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectiles : MonoBehaviour
{
    
    public float lifeTime = 5f; // Time in seconds before the projectile is destroyed
    public float gravityScale = 0.5f; // Scale of gravity effect (1.0 is normal gravity)
    public float damage = 20;

    private Rigidbody rb;


    void Start()
    {
        rb = GetComponent<Rigidbody>();
        Destroy(gameObject, lifeTime);
    }

    void FixedUpdate()
    {
        // Apply custom gravity to the bullet
        Vector3 gravity = Physics.gravity * gravityScale;
        rb.AddForce(gravity, ForceMode.Acceleration);
    }

    void OnCollisionEnter(Collision collision)
    {

        // Check if the object hit has a TankHealth component
        TankHealth tankHealth = collision.gameObject.GetComponent<TankHealth>();
        if (tankHealth != null)
        {
            tankHealth.TakeDamage(damage); // Apply damage to the tank
        }

        // Check if the object hit has an EnemyTankHealth component
        EnemyTankHealth enemyHealth = collision.gameObject.GetComponent<EnemyTankHealth>();
        if (enemyHealth != null)
        {
            enemyHealth.TakeDamage(damage); // Apply a hit to the enemy tank
        }
        // Destroy the projectile on collision 
        Destroy(gameObject);
    }

    // Method to set the damage value
    public void SetDamage(float newDamage)
    {
        damage = newDamage; // Update the damage
    }
}
