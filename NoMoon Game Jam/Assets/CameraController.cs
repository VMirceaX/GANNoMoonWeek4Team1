using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public Camera mainCamera;

    void Start()
    {
        mainCamera = Camera.main;
    }

    public void StageUp()
    {
        mainCamera.transform.position += new Vector3(40, 0, 0);
        Debug.Log("StageUp");
    }
}
