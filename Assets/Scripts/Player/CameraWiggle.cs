using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraWiggle : MonoBehaviour
{
    public float wiggleSpeed = 0.5f; // 흔들림 속도
    public float wiggleAmount = 1f; // 흔들림 정도
    public float mouseTiltAmount = 0.1f; // 마우스 기울기 정도

    private Quaternion initialRotation;

    // Start is called before the first frame update
    void Start()
    {
        initialRotation = transform.localRotation;
    }

    // Update is called once per frame
    void Update()
    {
        // Perlin noise for breathing effect
        float wiggleX = (Mathf.PerlinNoise(Time.time * wiggleSpeed, 0f) - 0.5f) * wiggleAmount;
        float wiggleY = (Mathf.PerlinNoise(0f, Time.time * wiggleSpeed) - 0.5f) * wiggleAmount;

        // Mouse input for tilt effect
        float mouseX = (Input.mousePosition.x / Screen.width - 0.5f) * mouseTiltAmount;
        float mouseY = -(Input.mousePosition.y / Screen.height - 0.5f) * mouseTiltAmount;

        // Combine both effects
        transform.localRotation = initialRotation * Quaternion.Euler(wiggleX + mouseY, wiggleY + mouseX, 0f);
    }
}