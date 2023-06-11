using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    // Speed
    public float speed = 5;

    // CharacterController component
    CharacterController cc;

    // Magnitude of gravitational acceleration
    public float gravity = -20;
    // Vertical speed
    float yVelocity = 0;

    // Jump size
    public float jumpPower = 5;

    // Rotation speed for player and camera
    public float rotationSpeed = 5;

    void Start()
    {
        cc = GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {
        // Get user input for movement
        float hMovement = ARAVRInput.GetAxis("Horizontal");
        float vMovement = ARAVRInput.GetAxis("Vertical");


        // Create movement direction
        Vector3 moveDirection = new Vector3(hMovement, 0, vMovement);
        // Change the input value in the direction the user is looking
        moveDirection = Camera.main.transform.TransformDirection(moveDirection);
        // Add vertical direction with gravity applied (v = v0 + at)
        yVelocity += gravity * Time.deltaTime;
        // When on the floor, zero velocity to account for normal drag
        if (cc.isGrounded)
        {
            yVelocity = 0;
        }
        // Assign a jump size when the user presses the jump button
        if (ARAVRInput.GetDown(ARAVRInput.Button.Two, ARAVRInput.Controller.RTouch))
        {
            yVelocity = jumpPower;
        }

        moveDirection.y = yVelocity;

        // Move the player
        cc.Move(moveDirection * speed * Time.deltaTime);

    }
}
