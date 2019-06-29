using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrapCollider : MonoBehaviour
{
    public InputManager inputManager;

    void Start()
    {
        inputManager = FindObjectOfType<InputManager>();
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && other.GetComponent<PlayerController>() == inputManager.player1)
        {
            inputManager.Dead("player1");
            other.transform.position = transform.position;
            if (GetComponent<SphereCollider>())
            {
                Destroy(gameObject);
            }
        }

        else if(other.CompareTag("Player") && other.GetComponent<PlayerController>() == inputManager.player2)
        {
            inputManager.Dead("player2");
            other.transform.position = transform.position;
            if (GetComponent<SphereCollider>())
            {
                Destroy(gameObject);
            }
        }

        else if (other.CompareTag("Player") && other.GetComponent<PlayerController>() == inputManager.player3)
        {
            inputManager.Dead("player3");
            other.transform.position = transform.position;
            if (GetComponent<SphereCollider>())
            {
                Destroy(gameObject);
            }
        }

        else if (other.CompareTag("Player") && other.GetComponent<PlayerController>() == inputManager.player4)
        {
            inputManager.Dead("player4");
            other.transform.position = transform.position;
            if (GetComponent<SphereCollider>())
            {
                Destroy(gameObject);
            }
        }
    }
}
