using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class ThirdPersonCam : MonoBehaviour
{
    public Transform playerHull; // The tank's hull (main body)
    public CinemachineVirtualCamera cinemachineCam; // Reference to the Cinemachine virtual camera

    public float followDistance = 10f; // Distance behind the tank
    public float followHeight = 5f; // Height above the tank
    public float cameraSmoothSpeed = 5f; // Speed of camera smoothing

    private Vector3 cameraOffset;

    private void Start()
    {
        // Calculate the initial camera offset based on desired distance and height
        cameraOffset = new Vector3(0, followHeight, followDistance);

        // Configure the Cinemachine virtual camera to follow and look at the tank's hull
        cinemachineCam.Follow = playerHull;
        cinemachineCam.LookAt = playerHull;
    }

    private void LateUpdate()
    {
        // Smoothly update the camera position to follow the tank from behind
        Vector3 desiredPosition = playerHull.position + playerHull.TransformDirection(cameraOffset);
        transform.position = Vector3.Lerp(transform.position, desiredPosition, cameraSmoothSpeed * Time.deltaTime);

        // Ensure the camera is always looking at the tank
        transform.LookAt(playerHull);
    }
}
