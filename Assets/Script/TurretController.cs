using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretController : MonoBehaviour
{
    public Transform turretTransform;
    public Camera MainCamera;


    void Update()
    {
        RotateTurretToMouse();
    }

    void RotateTurretToMouse()
    {
        // Get the mouse position in screen space
        Vector3 mouseScreenPosition = Input.mousePosition;

        // Convert the mouse position to a point in world space
        Ray ray = MainCamera.ScreenPointToRay(mouseScreenPosition);
        if (Physics.Raycast(ray, out RaycastHit hitInfo))
        {
            // Calculate the direction from the turret to the mouse hit point
            Vector3 direction = hitInfo.point - turretTransform.position;
            direction.y = 0; // Keep the turret rotation on the horizontal plane

            // Rotate the turret to face the direction, adding 180 degrees to compensate
            Quaternion targetRotation = Quaternion.LookRotation(direction) * Quaternion.Euler(0, 180, 0);
            turretTransform.rotation = Quaternion.Slerp(turretTransform.rotation, targetRotation, Time.deltaTime * 5f);
        }
    }
}
