using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerEnemy : MonoBehaviour
{
    public float boostRadius = 10f; // radius within which enemies are boosted
    public float damageBoostAmount = 10f; //The amount of damage boost applied to enemies
    public float boostInterval = 1f; //How often to check and apply the boost 
    

    
    void Start()
    {
        // epeating method to apply the boost at regular intervals
        InvokeRepeating(nameof(ApplyDamageBoost), 0f, boostInterval);
    }

    void ApplyDamageBoost()
    {
        // Find all colliders within the boost radius
        Collider[] colliders = Physics.OverlapSphere(transform.position, boostRadius);

       

        foreach (Collider nearbyObject in colliders)
        {
            //check if they implement the DamageBoostable interface
            IDamageBoostable damageBoostable = nearbyObject.GetComponent<IDamageBoostable>();
            if (damageBoostable != null)
            {
                damageBoostable.ApplyDamageBoost(damageBoostAmount); // Apply the damage boost
            }
        }
    }

    void OnDrawGizmosSelected()
    {
        // Draw a sphere in the editor to visualize the boost radius
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, boostRadius);
    }
}
