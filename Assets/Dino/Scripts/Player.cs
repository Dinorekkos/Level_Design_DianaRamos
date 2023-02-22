using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
     public float speed = 5.0f; // adjust this value to change the player's movement speed
    public float jumpForce = 10.0f; // adjust this value to change the player's jump force
    public float mouseSensitivity = 100.0f; // adjust this value to change the mouse sensitivity
    public float minY = -60.0f; // the minimum y value of the camera
    public float maxY = 60.0f; // the maximum y value of the camera
    public float maxStepHeight = 0.5f; // adjust this value to change the maximum step height the player can climb
    public float slopeLimit = 45f; // adjust this value to change the maximum slope angle the player can climb
    public Transform groundCheck; // a reference to the object that checks if the player is on the ground
    public float groundDistance = 0.4f; // the distance to check if the player is on the ground
    
    private float rotationY = 0.0f;
    private CharacterController controller;

    void Start()
    {
        controller = GetComponent<CharacterController>();

        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }

    void Update()
    {
        // Check if the player is on the ground
        bool isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, LayerMask.GetMask("Ground"));

        // Move the player in the direction of the arrow keys
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");
        Vector3 moveDirection = transform.right * horizontalInput + transform.forward * verticalInput;
        controller.Move(moveDirection * speed * Time.deltaTime);

        // Check if the player can step up
        float stepHeight = Mathf.Infinity;
        RaycastHit hit;
        if (Physics.Raycast(transform.position, Vector3.down, out hit))
        {
            stepHeight = transform.position.y - hit.point.y;
        }

        // Check if the player can climb the slope
        float slopeAngle = Vector3.Angle(hit.normal, Vector3.up);
        bool isOnSteepSlope = slopeAngle > slopeLimit;

        // Jump if the player is on the ground and the jump key is pressed, or if they are on a slope they can climb
        if ((isGrounded || stepHeight <= maxStepHeight || isOnSteepSlope) && Input.GetKeyDown(KeyCode.Space))
        {
            controller.Move(Vector3.up * jumpForce * Time.deltaTime);
        }

        // Rotate the camera based on the mouse movement
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;
        rotationY -= mouseY;
        rotationY = Mathf.Clamp(rotationY, minY, maxY);
        Camera.main.transform.localRotation = Quaternion.Euler(rotationY, 0, 0);
        transform.Rotate(Vector3.up * mouseX);
    }

    void FixedUpdate()
    {
        // Check if the player is on a slope
        RaycastHit slopeHit;
        if (Physics.Raycast(transform.position, Vector3.down, out slopeHit, groundDistance))
        {
            float slopeAngle = Vector3.Angle(slopeHit.normal, Vector3.up);
            if (slopeAngle > slopeLimit && slopeHit.distance <= maxStepHeight)
            {
                Vector3 slopeDirection = Vector3.Cross(slopeHit.normal, Vector3.down);
                controller.Move(slopeDirection * speed * Time.deltaTime);
            }
        }
    }
}
