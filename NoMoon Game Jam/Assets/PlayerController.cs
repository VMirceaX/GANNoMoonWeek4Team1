using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed = 10f;
    public float jumpForce = 8f;
    public float gravity = 30f;
    private Vector3 moveDir = Vector3.zero;
    private CharacterController controller;

    void Start()
    {
        controller = gameObject.GetComponent<CharacterController>();
    }

    void Update()
    {
        moveDir.y -= gravity * Time.deltaTime;

        if (controller.isGrounded)
        {
            moveDir = new Vector3(Input.GetAxisRaw("Horizontal") * moveSpeed, 0 - gravity * Time.deltaTime, Input.GetAxisRaw("Vertical") * moveSpeed);
            moveDir = transform.TransformDirection(moveDir);
            moveDir *= moveSpeed;

            if (Input.GetButtonDown("Jump") && controller.isGrounded)
            {
                moveDir.y = jumpForce;
            }
        }

        else if (!controller.isGrounded)
        {
            moveDir = new Vector3(Input.GetAxisRaw("Horizontal") * moveSpeed, moveDir.y, Input.GetAxisRaw("Vertical") * moveSpeed);
            moveDir = transform.TransformDirection(moveDir);

            moveDir.x *= moveSpeed;
            moveDir.z *= moveSpeed;
        }

            controller.Move(moveDir * Time.deltaTime);
    }
}
