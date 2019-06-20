using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed = 10f;
    public float jumpForce = 8f;
    public float gravity = 30f;
    private Vector3 moveDir = Vector3.zero;
    private CharacterController controller;
    public Gamepad thisGamepad;
    public Keyboard thisKeyboard;
    public bool keyboardOrGamepad; //true == keyboard, false == gamepad


    void Start()
    {
        controller = gameObject.GetComponent<CharacterController>();
    }

    void FixedUpdate()
    {
        
    }

    void Update()
    {
        moveDir.y -= gravity * Time.deltaTime;

        if (controller.isGrounded && !keyboardOrGamepad)
        {
            moveDir = new Vector3(thisGamepad.leftStick.ReadValue().x * moveSpeed, 0 - gravity * Time.deltaTime, thisGamepad.leftStick.ReadValue().y * moveSpeed);
            moveDir = transform.TransformDirection(moveDir);
            moveDir *= moveSpeed;

            if (Input.GetButtonDown("Jump") && controller.isGrounded)
            {
                moveDir.y = jumpForce;
            }
        }

        else if (!controller.isGrounded && !keyboardOrGamepad)
        {
            moveDir = new Vector3(thisGamepad.leftStick.ReadValue().x * moveSpeed, moveDir.y, thisGamepad.leftStick.ReadValue().y * moveSpeed);
            moveDir = transform.TransformDirection(moveDir);

            moveDir.x *= moveSpeed;
            moveDir.z *= moveSpeed;
        }

            controller.Move(moveDir * Time.deltaTime);
    }
}
