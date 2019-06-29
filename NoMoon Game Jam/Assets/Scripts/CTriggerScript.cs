using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CTriggerScript : MonoBehaviour
{
    public CameraController controller;

    void Start()
    {
        controller = FindObjectOfType<CameraController>();
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            controller.StageUp();
            Destroy(gameObject);
        }
    }
}
