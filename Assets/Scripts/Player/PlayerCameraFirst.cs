using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCameraFirst : MonoBehaviour
{
    public Transform player; // Reference to the player's transform
    public float mouseSensitivity = 100f; // Mouse sensitivity for looking around

    [SerializeField]
    private Vector3 offset; // Offset from the player's position
    
    private float xRotation = 0f; // Rotation around the x-axis
    private float yRotation = 0f; // Rotation around the y-axis

    void Start()
    {
        // Lock the cursor to the center of the screen and make it invisible
        Cursor.lockState = CursorLockMode.Locked;
    }

    void Update()
    {
        // Get mouse input
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;

        // Adjust the x and y rotation based on the mouse input
        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -90f, 90f); // Clamp the rotation to prevent flipping
        yRotation += mouseX;

        // Rotate the camera around the x-axis and y-axis
        transform.localRotation = Quaternion.Euler(xRotation, yRotation, 0f);
        transform.position = player.transform.position + offset;
        // Rotate the player around the y-axis
        player.Rotate(Vector3.up * mouseX);
    }
}