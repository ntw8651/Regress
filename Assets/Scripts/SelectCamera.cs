using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectCamera : MonoBehaviour
{
    public Camera mainCamera;
    public Camera bedCamera;

    private void Start()
    {
        mainCamera.enabled = true;
        bedCamera.enabled = false;
    }

    public void ShowMainCamera()
    {
        mainCamera.enabled = true;
        bedCamera.enabled = false;
    }
    
    public void ShowBedCamera()
    {
        mainCamera.enabled = false;
        bedCamera.enabled = true;
    }
}
