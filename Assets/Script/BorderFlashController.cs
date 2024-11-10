using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BorderFlashController : MonoBehaviour
{
    public Transform player; // Reference to the player
    public Transform gasCloud; // Reference to the gas cloud
    public float warningDistance = 20f; // Distance at which the border starts flashing
    public float flashSpeed = 5f; // Speed at which the border flashes

    public Image topBorder;
    public Image bottomBorder;
    public Image leftBorder;
    public Image rightBorder;

    private bool isFlashing = false;

    void Update()
    {
        // Calculate the distance between the player and the gas cloud
        float distance = Vector3.Distance(player.position, gasCloud.position);

        // Check if the cloud is within the warning distance
        if (distance <= warningDistance)
        {
            if (!isFlashing)
            {
                isFlashing = true;
                StartCoroutine(FlashBorders());
            }
        }
        else
        {
            if (isFlashing)
            {
                isFlashing = false;
                StopAllCoroutines();
                SetBorderAlpha(0f); // Reset alpha for all borders
            }
        }
    }

    System.Collections.IEnumerator FlashBorders()
    {
        while (isFlashing)
        {
            // Determine which borders to flash based on the direction of the gas cloud
            Vector3 direction = (gasCloud.position - player.position).normalized;

            // Reset all border alphas
            SetBorderAlpha(0f);

            // Flash specific borders based on the direction
            float alpha = Mathf.PingPong(Time.time * flashSpeed, 1f);

            // Inverted logic to match your case
            if (direction.z < 0) // Gas cloud is coming from the front
            {
                topBorder.color = new Color(topBorder.color.r, topBorder.color.g, topBorder.color.b, alpha);
            }
            else if (direction.z > 0) // Gas cloud is coming from the back
            {
                bottomBorder.color = new Color(bottomBorder.color.r, bottomBorder.color.g, bottomBorder.color.b, alpha);
            }

            if (direction.x < 0) // Gas cloud is coming from the right
            {
                rightBorder.color = new Color(rightBorder.color.r, rightBorder.color.g, rightBorder.color.b, alpha);
            }
            else if (direction.x > 0) // Gas cloud is coming from the left
            {
                leftBorder.color = new Color(leftBorder.color.r, leftBorder.color.g, leftBorder.color.b, alpha);
            }

            yield return null;
        }
    }

    void SetBorderAlpha(float alpha)
    {
        // Reset all border alphas
        Color color = topBorder.color;
        color.a = alpha;
        topBorder.color = color;
        bottomBorder.color = color;
        leftBorder.color = color;
        rightBorder.color = color;
    }
}
