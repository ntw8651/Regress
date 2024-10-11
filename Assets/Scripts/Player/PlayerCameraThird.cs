using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCameraThird : MonoBehaviour
{
    public Transform player; // 플레이어의 Transform
    public Vector3 offset; // 카메라와 플레이어 사이의 거리
    public float rotationSpeed = 5.0f; // 카메라 회전 속도

    private float currentRotationX = 0.0f;
    private float currentRotationY = 0.0f;

    void Start()
    {
        // 초기 오프셋 설정
        offset = new Vector3(0, 0, 6);
    }

    void LateUpdate()
    {
        // 마우스 입력에 따라 회전 값 업데이트
        currentRotationX += Input.GetAxis("Mouse X") * rotationSpeed;
        currentRotationY += Input.GetAxis("Mouse Y") * rotationSpeed;
        currentRotationY = Mathf.Clamp(currentRotationY, -89, 89); // 상하 회전 각도 제한

        // 회전 값에 따라 카메라 위치 계산
        Quaternion rotation = Quaternion.Euler(currentRotationY, currentRotationX, 0);
        Vector3 position = player.position + rotation * offset;

        // 카메라 위치와 회전 적용
        transform.position = position;
        transform.LookAt(player.position);
    }
}
