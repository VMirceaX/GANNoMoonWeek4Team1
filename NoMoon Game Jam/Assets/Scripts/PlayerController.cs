using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed = 10f;
    public float jumpForce = 8f;
    public float gravity = 30f;
    public Vector3 moveDir = Vector3.zero;
    private CharacterController controller;
    public Gamepad thisGamepad;
    public Keyboard thisKeyboard;
    public bool keyboardOrGamepad; //true == keyboard, false == gamepad
    public float keyboardHorizontal, keyboardVertical;
    public float itemDistanceCheck;
    public PickupScript[] sceneItems;
    public GroupInventory inventory;
    public Equipment heldItem;
    public GameObject rope, gold, body;
    public float ropeMoveForce;
    public Equipment[] itemArray = new Equipment[3];
    public int i;
    public float coolDownTimer, coolDownTimer2;
    public bool coolDownActive, coolDownActive2;

    void Start()
    {
        SceneItemReCheck();

        controller = gameObject.GetComponent<CharacterController>();
        heldItem = itemArray[i];
    }

    void FixedUpdate()
    {
        if (keyboardOrGamepad)
        {
            keyboardHorizontal = thisKeyboard.dKey.ReadValue() + (thisKeyboard.aKey.ReadValue() * -1);
            keyboardVertical = thisKeyboard.wKey.ReadValue() + (thisKeyboard.sKey.ReadValue() * -1);
        }
    }

    void SwapItem()
    {
        if (coolDownActive)
        {
            coolDownTimer -= Time.deltaTime;

            if (coolDownTimer <= 0)
            {
                coolDownActive = false;
            }
        }

        if (thisGamepad != null)
        {
            if (thisGamepad.leftShoulder.isPressed && !coolDownActive)
            {
                if (i >= 0)
                {
                    i--;
                    heldItem = itemArray[i];
                }
                else if (i < 0)
                {
                    i = itemArray.Length + 1;
                    heldItem = itemArray[i];
                }

                coolDownActive = true;
                coolDownTimer = 0.2f;
            }

            else if (thisGamepad.rightShoulder.isPressed && !coolDownActive)
            {
                if (i <= itemArray.Length)
                {
                    i++;
                    heldItem = itemArray[i];
                }
                else if (i > itemArray.Length)
                {
                    i = 0;
                    heldItem = itemArray[i];
                }

                coolDownActive = true;
                coolDownTimer = 0.2f;
            }
        }

        else if (thisKeyboard != null)
        {
            if (thisKeyboard.digit1Key.isPressed && !coolDownActive)
            {
                if (i >= 0)
                {
                    i--;
                    heldItem = itemArray[i];
                }
                else if (i <= 0)
                {
                    i = itemArray.Length + 1;
                    heldItem = itemArray[i];
                }

                coolDownActive = true;
                coolDownTimer = 0.2f;
            }

            else if (thisKeyboard.digit3Key.isPressed && !coolDownActive)
            {
                if (i <= itemArray.Length)
                {
                    i++;
                    heldItem = itemArray[i];
                }
                else if (i > itemArray.Length)
                {
                    i = 0;
                    heldItem = itemArray[i];
                }

                coolDownActive = true;
                coolDownTimer = 0.2f;
            }
        }
    }

    void DropItem()
    {
        if (coolDownActive2)
        {
            coolDownTimer2 -= Time.deltaTime;

            if (coolDownTimer2 <= 0)
            {
                coolDownActive2 = false;
            }
        }

        if (thisGamepad != null)
        {
            if (thisGamepad.buttonNorth.isPressed && !coolDownActive2)
            {
                Debug.Log(inventory.itemAmounts[heldItem.itemName]);
                if (heldItem.itemName == "Rope" && !(inventory.itemAmounts["Rope"] <= 0))
                {
                    Instantiate(rope, transform.position + new Vector3 (-1.5f, 0, 0), new Quaternion(0, 0, 0, 0), null);
                    inventory.itemAmounts[heldItem.itemName] = inventory.itemAmounts[heldItem.itemName] - 1;
                }

                else if (heldItem.itemName == "Gold" && !(inventory.itemAmounts["Gold"] <= 0))
                {
                    Instantiate(gold, transform.position + new Vector3(-1.5f, 0, 0), new Quaternion(0, 0, 0, 0), null);
                    inventory.itemAmounts[heldItem.itemName] = inventory.itemAmounts[heldItem.itemName] - 1;
                }

                else if (heldItem.itemName == "Body" && !(inventory.itemAmounts["Body"] <= 0))
                {
                    Instantiate(body, transform.position + new Vector3(-1.5f, 0, 0), new Quaternion(0, 0, 0, 0), null);
                    inventory.itemAmounts[heldItem.itemName] = inventory.itemAmounts[heldItem.itemName] - 1;
                }

                inventory.ItemCollected();

                coolDownActive2 = true;
                coolDownTimer2 = 0.2f;
            }
        }

        else if (thisKeyboard != null)
        {
            if (thisKeyboard.qKey.isPressed && !coolDownActive2)
            {
                inventory.itemAmounts[heldItem.itemName] = inventory.itemAmounts[heldItem.itemName] - 1;
                Debug.Log(inventory.itemAmounts[heldItem.itemName]);
                if (heldItem.itemName == "Rope" && !(inventory.itemAmounts["Rope"] <= 0))
                {
                    Instantiate(rope, transform.position + new Vector3(-1.5f, 0, 0), new Quaternion(0, 0, 0, 0), null);
                    inventory.itemAmounts[heldItem.itemName] = inventory.itemAmounts[heldItem.itemName] - 1;
                }

                else if (heldItem.itemName == "Gold" && !(inventory.itemAmounts["Gold"] <= 0))
                {
                    Instantiate(gold, transform.position + new Vector3(-1.5f, 0, 0), new Quaternion(0, 0, 0, 0), null);
                    inventory.itemAmounts[heldItem.itemName] = inventory.itemAmounts[heldItem.itemName] - 1;
                }

                else if (heldItem.itemName == "Body" && !(inventory.itemAmounts["Body"] <= 0))
                {
                    Instantiate(body, transform.position + new Vector3(-1.5f, 0, 0), new Quaternion(0, 0, 0, 0), null);
                    inventory.itemAmounts[heldItem.itemName] = inventory.itemAmounts[heldItem.itemName] - 1;
                }

                inventory.ItemCollected();

                coolDownActive2 = true;
                coolDownTimer2 = 0.2f;
            }
        }
    }

    void ItemPickup()
    {
        SceneItemReCheck();

        foreach (PickupScript item in sceneItems)
        {
            if (Vector3.Distance(item.transform.position, transform.position) <= itemDistanceCheck && thisGamepad != null && item.enabled)
            {


                if (thisGamepad.buttonEast.isPressed)
                {
                    inventory.itemAmounts[item.item.itemName] = inventory.itemAmounts[item.item.itemName] + 1;
                    Debug.Log(inventory.itemAmounts[item.item.itemName]);
                    Destroy(item.gameObject);
                    inventory.ItemCollected();
                }
            }

            else if (Vector3.Distance(item.transform.position, transform.position) <= itemDistanceCheck && thisKeyboard != null && item.enabled)
            {


                if (thisKeyboard.eKey.isPressed)
                {
                    inventory.itemAmounts[item.item.itemName] = inventory.itemAmounts[item.item.itemName] + 1;
                    Debug.Log(inventory.itemAmounts[item.item.itemName]);
                    Destroy(item.gameObject);
                    inventory.ItemCollected();
                }
            }
        }
    }

    void Update()
    {
        if (controller.enabled)
        {
            DropItem();
            PlayerMovement();
            ItemPickup();
            SwapItem();
            moveDir.y -= gravity * Time.deltaTime;
        }

        else if (!controller.enabled)
        {
            if (keyboardOrGamepad)
            {
                if (keyboardHorizontal == 1)
                {
                    Rigidbody ropeMovement = GetComponent<Rigidbody>();
                    ropeMovement.AddForce(ropeMoveForce, 0, 0);
                }

                else if (keyboardHorizontal == -1)
                {
                    Rigidbody ropeMovement = GetComponent<Rigidbody>();
                    ropeMovement.AddForce(ropeMoveForce * -1, 0, 0);
                }
            }

            else if (!keyboardOrGamepad)
            {
                Rigidbody ropeMovement = GetComponent<Rigidbody>();
                ropeMovement.AddForce(thisGamepad.leftStick.ReadValue().x * ropeMoveForce, 0, 0);
            }
        }
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
        sceneItems = new PickupScript[FindObjectsOfType<PickupScript>().Length];
        sceneItems = FindObjectsOfType<PickupScript>();
    }

}
