using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class camera_move : MonoBehaviour
{
    /*
     * 초기 작성자 : 문서연
     * 
     * 카메라를 Z축을 실시간 조정하기 편하도록 코드 수정했습니다.
     * 
     */
    public Transform target;
    public Vector3 offset1;
    public Vector3 offset2;
    public Vector3 offset3;
    public Vector3 offset4;
    public Vector3 angle1;
    public Vector3 angle2;
    public Vector3 angle3;
    public Vector3 angle4;
    public float cameraSpeed = 10f;
    public float cameraz;
    int type = 1;

    private Vector3 targetPosition;
    private Quaternion targetRotation;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            type = 1;
        }
        else if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            type = 2;
        }
        else if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            type = 3;
        }
        else if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            type = 4;
        }

        if (type == 1)
        {
            targetPosition = target.position + offset1;
            targetRotation = Quaternion.Euler(angle1);
        }
        else if (type == 2)
        {
            targetPosition = target.position + offset2;
            targetRotation = Quaternion.Euler(angle2);
        }
        else if (type == 3)
        {
            targetPosition = target.position + offset3;
            targetRotation = Quaternion.Euler(angle3);
        }
        else if (type == 4)
        {
            targetPosition = target.position + offset4;
            targetRotation = Quaternion.Euler(angle4);
        }
        //마우스 휠로 화면 크기 조정~
        float wheelInput = Input.GetAxis("Mouse ScrollWheel");
        if (wheelInput > 0) //휠을 올렸을 때~
        {
            cameraz++;
        }
        if (wheelInput < 0) //휠을 내렸을 때~
        {
            cameraz--;
        }
        targetPosition += transform.forward * cameraz;
        transform.position = Vector3.Lerp(transform.position, targetPosition, cameraSpeed * Time.deltaTime);
        transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, cameraSpeed * Time.deltaTime);
    }
}
