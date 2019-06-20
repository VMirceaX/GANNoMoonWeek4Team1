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
    public float itemDistanceCheck;
    public ItemActionScript[] sceneItems;
    public GroupInventory inventory;
    public SpriteRenderer interactButton;

    void Start()
    {
        SceneItemReCheck();

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

    void ItemPickup()
    {
        SceneItemReCheck();

        foreach (ItemActionScript item in sceneItems)
        {
            if (Vector3.Distance(item.transform.position, transform.position) <= itemDistanceCheck && thisGamepad != null)
            {
                interactButton.enabled = true;
                if (thisGamepad.buttonWest.isPressed)
                {
                    inventory.itemAmounts[item.item.itemName] = inventory.itemAmounts[item.item.itemName] + 1;
                    Debug.Log(inventory.itemAmounts[item.item.itemName]);
                    Destroy(item.gameObject);
                    inventory.itemPickedUp = true;
                    interactButton.enabled = false;
                }
            }

            if (Vector3.Distance(item.transform.position, transform.position) <= itemDistanceCheck && thisKeyboard != null)
            {
                interactButton.enabled = true;
                if (thisKeyboard.eKey.isPressed)
                {
                    inventory.itemAmounts[item.item.itemName] = inventory.itemAmounts[item.item.itemName] + 1;
                    Debug.Log(inventory.itemAmounts[item.item.itemName]);
                    Destroy(item.gameObject);
                    inventory.itemPickedUp = true;
                    interactButton.enabled = false;
                }
            }

            else
            {

            }
        }
    }

    void Update()
    {
        PlayerMovement();
        ItemPickup();


        moveDir.y -= gravity * Time.deltaTime;
    }

    void PlayerMovement()
    {
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

    void SceneItemReCheck()
    {
        sceneItems = new ItemActionScript[FindObjectsOfType<ItemActionScript>().Length];
        sceneItems = FindObjectsOfType<ItemActionScript>();
    }

}
