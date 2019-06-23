using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemActions : MonoBehaviour
{
    public PlayerController currentPlayer;
    public float ropeMaxRaycast;
    public LayerMask ropeRaycastMask;
    public bool ropeConnect, ropeState;
    private RaycastHit hit;
    public Vector3 face;
    public HingeJoint ropeJoint;
    public float coolDownTimer;
    public string itemfunction;
    public Rigidbody rb;
    public GroupInventory inv;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        currentPlayer = GetComponent<PlayerController>();
        ropeJoint = GetComponent<HingeJoint>();
    }


    void Update()
    {
        coolDownTimer -= Time.deltaTime;        

        if (currentPlayer.moveDir.x > 1)
        {
            face = transform.right;
        }

        else if (currentPlayer.moveDir.x < -1)
        {
            face = transform.right * -1;
        }

        else if (currentPlayer.moveDir.z > 1)
        {
            face = new Vector3(0, 0, 1);
        }

        else if (currentPlayer.moveDir.z < -1)
        {
            face = new Vector3(0, 0, -1);
        }

        if (!currentPlayer.keyboardOrGamepad && currentPlayer.heldItem != null)
        {
            if (currentPlayer.thisGamepad.buttonWest.isPressed)
            {
                itemfunction = currentPlayer.heldItem.itemName;
                if (itemfunction == "Rope")
                {
                    Rope();
                    inv.itemAmounts[Rope] = -1;
                }
            }

            if (ropeConnect && !ropeState)
            {
                ropeJoint.connectedBody = hit.rigidbody;
                ropeJoint.anchor = new Vector3(0, 4, 0);
                ropeJoint.axis = new Vector3(0, 1, 0);
                rb.constraints = RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationY;
                ropeState = true;
                GetComponent<CharacterController>().enabled = false;
            }

            else if (ropeConnect && ropeState)
            {
                if (currentPlayer.thisGamepad.buttonEast.isPressed)
                {
                    ropeJoint.connectedBody = null;
                    GetComponent<CharacterController>().enabled = true;
                    ropeConnect = false;
                    ropeState = false;
                }
            }
        }

        else if (currentPlayer.keyboardOrGamepad && currentPlayer.heldItem != null)
        {
            if (currentPlayer.thisKeyboard.fKey.isPressed && coolDownTimer <= 0)
            {
                itemfunction = currentPlayer.heldItem.itemName;
                if (itemfunction == "Rope")
                {
                    Rope();
                    coolDownTimer = 0.5f;
                }
            }

            if (ropeConnect && !ropeState)
            {
                ropeJoint.connectedBody = hit.rigidbody;
                ropeJoint.anchor = new Vector3 (0, 4, 0);
                ropeJoint.axis = new Vector3(0, 1, 0);
                rb.constraints = RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationY;
                ropeState = true;
                GetComponent<CharacterController>().enabled = false;
            }

            else if (ropeConnect && ropeState && coolDownTimer <= 0)
            {
                if (currentPlayer.thisKeyboard.eKey.isPressed)
                {
                    ropeJoint.connectedBody = null;
                    GetComponent<CharacterController>().enabled = true;
                    rb.constraints = RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationY | RigidbodyConstraints.FreezeRotationZ ;
                    transform.rotation = new Quaternion(0, 0, 0, 0);
                    ropeConnect = false;
                    ropeState = false;
                    coolDownTimer = 0.5f;
                }
            }
        }
        

        void Rope()
        {
            if (Physics.Raycast(transform.position, transform.up + face, out hit, ropeMaxRaycast, ropeRaycastMask))
            {
                ropeConnect = true;
            }
        }
    }
}
