using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChasingGasCloud : MonoBehaviour
{
    public Transform player; // Reference to the player
    public float initialSpeed = 5f; // Initial speed of the gas cloud
    public float speedIncreaseRate = 0.5f; // How much the speed increases per second
    public float initialSize = 10f; // Initial size (scale) of the gas cloud
    public float sizeIncreaseRate = 0.2f; // How much the size increases per second
    public float maxSpeed = 20f; // Maximum speed the gas cloud can reach
    public float maxSize = 50f; // Maximum size the gas cloud can reach
    public float damagePerSecond = 10f; // Damage dealt per second

   
    public float playerSpeedCheckInterval = 2f; // How often to check the player's speed (in seconds)


    private float currentSpeed;
    private float currentSize;
    private Rigidbody playerRigidbody;
    private float lastPlayerSpeed = 0f;
    private float checkTimer = 0f;

    void Start()
    {
        // Initialize speed and size
        currentSpeed = initialSpeed;
        currentSize = initialSize;
        playerRigidbody = player.GetComponent<Rigidbody>();
        UpdateSize();
    }

    void Update()
    {
        // Chase the player
        ChasePlayer();

        // Periodically check the player's speed and adjust the cloud's speed
        checkTimer += Time.deltaTime;
        if (checkTimer >= playerSpeedCheckInterval)
        {
            AdjustSpeedBasedOnPlayer();
            checkTimer = 0f; // Reset the timer
        }

        // Increase speed and size over time
        currentSpeed = Mathf.Min(maxSpeed, currentSpeed + speedIncreaseRate * Time.deltaTime);
        currentSize = Mathf.Min(maxSize, currentSize + sizeIncreaseRate * Time.deltaTime);

        // Update the size of the gas cloud
        UpdateSize();
    }

    void ChasePlayer()
    {
        if (player == null) return;

        // Move towards the player
        Vector3 direction = (player.position - transform.position).normalized;
        transform.position += direction * currentSpeed * Time.deltaTime;
    }

    void UpdateSize()
    {
        // Update the scale of the gas cloud
        transform.localScale = new Vector3(currentSize, currentSize, currentSize);
    }

    void AdjustSpeedBasedOnPlayer()
    {
        if (playerRigidbody == null) return;

        // Get the player's current speed
        float playerSpeed = playerRigidbody.velocity.magnitude;

        // If the player is moving slower than before, increase the cloud's speed
        if (playerSpeed < lastPlayerSpeed)
        {
            currentSpeed += speedIncreaseRate;
            currentSpeed = Mathf.Min(currentSpeed, maxSpeed); // Clamp to max speed
        }
        else
        {
            // If the player is maintaining or increasing speed, reset the cloud's speed to the initial speed
            currentSpeed = initialSpeed;
        }

        // Update the last recorded player speed
        lastPlayerSpeed = playerSpeed;
    }

    void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player"))
        {
           
            // Apply damage to the player
            TankHealth playerHealth = other.GetComponent<TankHealth>();
            if (playerHealth != null)
            {
                playerHealth.TakeDamage(damagePerSecond * Time.deltaTime);
            }
        }
    }
}
