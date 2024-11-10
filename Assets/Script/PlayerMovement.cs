using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed = 5.0f;
    public float rotationSpeed = 120f;
    

    private float moveInput;
    private float rotationInput;
   
   
    private Rigidbody rb;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.freezeRotation = true;
    }

    // Update is called once per frame
    void Update()
    {
        moveInput = Input.GetAxis("Vertical");
        rotationInput = Input.GetAxis("Horizontal");



    }

    void FixedUpdate()
    {
        MoveTank();
        RotateTank();
       
    }

    void MoveTank()
    {
        // Only move if there is significant input to prevent drifting
        if (Mathf.Abs(moveInput) > 0.01f)
        {
            Vector3 direction = -transform.forward * moveInput * moveSpeed * Time.fixedDeltaTime;
            rb.MovePosition(rb.position + direction);
        }
    }

    void RotateTank()
    {
        // Only rotate if there is significant input to prevent unintended rotation
        if (Mathf.Abs(rotationInput) > 0.01f)
        {
            float rotation = rotationInput * rotationSpeed * Time.fixedDeltaTime;
            Quaternion turnRotation = Quaternion.Euler(0f, rotation, 0f);
            rb.MoveRotation(rb.rotation * turnRotation);
        }
    }



}
