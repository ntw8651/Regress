using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlyerMovement : MonoBehaviour
{
    
    Rigidbody rb;
    //변수 선언 시 public으로 지정해주면 에디터에서 해당 값을 변경할 수 있음!!
    //추가로 다른 스크립트에서 GetComponent로 해당 스크립트를 가져와서 변수를 변경할 수 있음!!
    public float horizontalSpeed = 100.0f; 
    public float verticalSpeed = 100.0f; 
    public float rotationSmoothness = 2.0f; 
    public float flyerSpeed = 10.0f;
    public float flyerMaxSpeed = 10.0f;
    private Quaternion targetRotation; 

    void Start()
    {
        //RIgidbody의 경우는 처음 Start시에 가져와야됨!!
        rb = GetComponent<Rigidbody>();
        targetRotation = transform.rotation; 
    }


    void Update()
    {

        //비행기 가속, ForceMode.Impulse는 순간적인 힘을 가하는 것임!!
        rb.AddForce(transform.forward * Time.deltaTime * flyerSpeed, ForceMode.Impulse);

        //속도가 최대 속도를 넘지 않도록 제한함!! magnitude는 벡터의 크기를 반환함!! 스칼라값임!!
        if (rb.velocity.magnitude > flyerMaxSpeed)
        {
            rb.velocity = rb.velocity.normalized * flyerMaxSpeed;
        }

        // 마우스 입력을 받아올 수 있음
        //Axis라면 방향, 오른쪽과 상단은 + 왼쪽과 하단은 -임!!
        float mouseX = Input.GetAxis("Mouse X");
        float mouseY = Input.GetAxis("Mouse Y");

        // 마우스의 horizontal 움직임에 따라 z축 회전을 설정합니다.
        float zRotation = -mouseX * horizontalSpeed * Time.deltaTime;

        // 마우스의 vertical 움직임에 따라 x축 회전을 설정합니다.
        float xRotation = mouseY * verticalSpeed * Time.deltaTime;

        // 목표 회전을 누적합니다.
        targetRotation *= Quaternion.Euler(xRotation, 0, zRotation);

        // 현재 회전과 목표 회전 사이를 보간합니다.
        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * rotationSmoothness);

        //현재 보는 로컬 방향에 대해 왼쪽 오른쪽으로 기울어진 정도에 따라서 저절로 이동하고 싶은데(양력의 느낌으로)
    }
}
