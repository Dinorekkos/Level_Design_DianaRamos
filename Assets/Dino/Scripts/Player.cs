using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
   
    public float moveSpeed = 5f;
    public float jumpForce = 10f;
    public float gravity = -20f;
    public float lookSensitivity = 3f;
    public float maxLookUpAngle = 60f;
    public float maxLookDownAngle = -60f;

    private CharacterController controller;
    private Camera playerCamera;
    private Vector3 moveDirection;
    private float lookAngleX;
    private float lookAngleY;

    void Start()
    {
        controller = GetComponent<CharacterController>();
        playerCamera = GetComponentInChildren<Camera>();
        Cursor.lockState = CursorLockMode.Locked;
    }

    void Update()
    {
        // Handle player movement
        float moveX = Input.GetAxis("Horizontal");
        float moveZ = Input.GetAxis("Vertical");
        Vector3 moveDirection = new Vector3(moveX, 0f, moveZ);
        moveDirection = playerCamera.transform.TransformDirection(moveDirection);
        moveDirection.y = 0f;
        moveDirection.Normalize();
        moveDirection *= moveSpeed;

        if (controller.isGrounded)
        {
            moveDirection.y = 0f;
            if (Input.GetKeyDown(KeyCode.Space))
            {
                moveDirection.y = jumpForce;
            }
        }

        moveDirection.y += gravity * Time.deltaTime;
        controller.Move(moveDirection * Time.deltaTime);

        // Handle camera movement
        float lookX = Input.GetAxis("Mouse X") * lookSensitivity;
        float lookY = Input.GetAxis("Mouse Y") * lookSensitivity;
        lookAngleX += lookX;
        lookAngleY -= lookY;
        lookAngleY = Mathf.Clamp(lookAngleY, maxLookDownAngle, maxLookUpAngle);
        transform.rotation = Quaternion.Euler(0f, lookAngleX, 0f);
        playerCamera.transform.localRotation = Quaternion.Euler(lookAngleY, 0f, 0f);

        // Rotate the player to match the direction of the camera
        Vector3 playerForward = playerCamera.transform.forward;
        playerForward.y = 0f;
        if (playerForward != Vector3.zero)
        {
            transform.rotation = Quaternion.LookRotation(playerForward);
        }
    }
}
