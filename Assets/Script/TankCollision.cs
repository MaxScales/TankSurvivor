using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TankCollision : MonoBehaviour
{
    public string groundTag = "Ground";

    void OnCollisionEnter(Collision collision)
    {
        // Check if the object the tank collided with is NOT the ground
        if (!collision.gameObject.CompareTag(groundTag) && collision.gameObject.CompareTag("Obstacle"))
        {
            // If it's not the ground, apply damage to the tank
            TankHealth tankHealth = GetComponent<TankHealth>();
            if (tankHealth != null)
            {
                tankHealth.TakeDamage(10f); // Call the method to apply a hit
            }
        }
    }
}
