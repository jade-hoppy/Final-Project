﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//automatically add a component to the inspector area of the
//FPSPlayerObject
[RequireComponent(typeof(CharacterController))]

public class CameraScript : MonoBehaviour
{
    //allows you to customize the movements of the player
    public float walkingSpeed = 7.5f;
    public float runningSpeed = 11.5f;
    public float jumpSpeed = 8.0f;
    public float gravity = 20.0f;
    public Camera playerCamera;
    public float lookSpeed = 2.0f;
    public float lookXLimit = 45.0f;

    CharacterController characterController;
    Vector3 moveDirection = Vector3.zero;
    float rotationX = 0;


    //adds a public variable but you can't see or modify it
    //in Unity inspector
    [HideInInspector]
    public bool canMove = true;

    void Start()//runs once at the start of the game
    {
        //get access to the controller so we can manipulate it.
        characterController = GetComponent<CharacterController>();

        // Lock cursor in gameview window so you won't lose control
        //of your player if the cursor leaves the bounds of the gameview
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        //press "Escape" when playing the game to access the cursor again

    }

    void Update()
    {
        // We are grounded, so recalculate move direction based on axes

        //this makes it so when you press up/down to move forward/backward, the camera
        //and the player movements will all be oriented correctly
        Vector3 forward = transform.TransformDirection(Vector3.forward);
        Vector3 right = transform.TransformDirection(Vector3.right);
        // Press Left Shift to run
        bool isRunning = Input.GetKey(KeyCode.LeftShift);
        float curSpeedX = canMove ? (isRunning ? runningSpeed : walkingSpeed) * Input.GetAxis("Vertical") : 0;
        float curSpeedY = canMove ? (isRunning ? runningSpeed : walkingSpeed) * Input.GetAxis("Horizontal") : 0;
        float movementDirectionY = moveDirection.y;
        moveDirection = (forward * curSpeedX) + (right * curSpeedY);

        //allow the player to jump
        if (Input.GetButton("Jump") && canMove && characterController.isGrounded)
        {
            moveDirection.y = jumpSpeed;//jump in the air with "Jump" button
        }
        else
        {
            //fall back to ground when not on the ground
            moveDirection.y = movementDirectionY;
        }

        // Apply gravity. Gravity is multiplied by deltaTime twice (once here, and once below
        // when the moveDirection is multiplied by deltaTime). This is because gravity should be applied
        // as an acceleration (ms^-2)
        if (!characterController.isGrounded)
        {
            moveDirection.y -= gravity * Time.deltaTime;
        }

        // Move the controller
        characterController.Move(moveDirection * Time.deltaTime);

        // Player and Camera rotation so you can always see where you are going
        if (canMove)
        {
            rotationX += -Input.GetAxis("Mouse Y") * lookSpeed;
            rotationX = Mathf.Clamp(rotationX, -lookXLimit, lookXLimit);
            playerCamera.transform.localRotation = Quaternion.Euler(rotationX, 0, 0);
            transform.rotation *= Quaternion.Euler(0, Input.GetAxis("Mouse X") * lookSpeed, 0);
        }
    }
}