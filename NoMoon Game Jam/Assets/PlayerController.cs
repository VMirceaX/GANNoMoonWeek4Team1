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
    public float keyboardHorizontal, keyboardVertical;


    void Start()
    {
        controller = gameObject.GetComponent<CharacterController>();
    }

    void FixedUpdate()
    {
        if (keyboardOrGamepad)
        {
            keyboardHorizontal = thisKeyboard.dKey.ReadValue() + (thisKeyboard.aKey.ReadValue() * -1);
            keyboardVertical = thisKeyboard.wKey.ReadValue() + (thisKeyboard.sKey.ReadValue() * -1);
        }
    }

    void Update()
    {
        moveDir.y -= gravity * Time.deltaTime;

        if (controller.isGrounded && !keyboardOrGamepad && thisGamepad != null)
        {
            moveDir = new Vector3(thisGamepad.leftStick.ReadValue().x * moveSpeed, 0 - gravity * Time.deltaTime, thisGamepad.leftStick.ReadValue().y * moveSpeed);
            moveDir = transform.TransformDirection(moveDir);
            moveDir *= moveSpeed;

            if (thisGamepad.buttonSouth.isPressed && controller.isGrounded)
            {
                moveDir.y = jumpForce;
            }
        }

        else if (!controller.isGrounded && !keyboardOrGamepad && thisGamepad != null)
        {
            moveDir = new Vector3(thisGamepad.leftStick.ReadValue().x * moveSpeed, moveDir.y, thisGamepad.leftStick.ReadValue().y * moveSpeed);
            moveDir = transform.TransformDirection(moveDir);

            moveDir.x *= moveSpeed;
            moveDir.z *= moveSpeed;
        }

            controller.Move(moveDir * Time.deltaTime);

        if (controller.isGrounded && keyboardOrGamepad && thisKeyboard != null)
        {
            moveDir = new Vector3(keyboardHorizontal * moveSpeed, 0 - gravity * Time.deltaTime, keyboardVertical * moveSpeed);
            moveDir = transform.TransformDirection(moveDir);
            moveDir *= moveSpeed;

            if (thisKeyboard.spaceKey.isPressed && controller.isGrounded)
            {
                moveDir.y = jumpForce;
            }
        }

        else if (!controller.isGrounded && keyboardOrGamepad && thisKeyboard != null)
        {
            moveDir = new Vector3(keyboardHorizontal * moveSpeed, moveDir.y, keyboardVertical * moveSpeed);
            moveDir = transform.TransformDirection(moveDir);

            moveDir.x *= moveSpeed;
            moveDir.z *= moveSpeed;
        }

        controller.Move(moveDir * Time.deltaTime);
    }
}
