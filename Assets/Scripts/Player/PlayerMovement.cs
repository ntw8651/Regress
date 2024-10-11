using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class PlayerMovement : MonoBehaviour
{
    private GameObject player;
    private PlayerState playerState;


    public float playerSpeed = 5.0f;          // 플레이어 이동 속도
    public float _jumpForce = 3.0f;            // 점프 힘
    public float _gravity = -9.81f;            // 중력 가속도
    //중력과 점프 힘을 
    
    public Transform mCameraArm;              // 카메라 암 Transform
    
    public GameObject mPlayerObject;          // 플레이어 오브젝트
    
    private CharacterController _characterController; // 캐릭터 컨트롤러 컴포넌트
    
    private Vector3 _mMoveInput;              // 플레이어 이동 입력값
    private Vector3 _verticalVelocity;        // 수직 속도
    
    private void Start()
    {
        _characterController = GetComponent<CharacterController>(); // 캐릭터 컨트롤러 컴포넌트 가져오기
        player = transform.gameObject;
        playerState = player.GetComponent<PlayerState>();
    }
    private void Update()
    {
        _mMoveInput = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
        Move();
    }
    private void Move()
    {
        bool isMove = _mMoveInput.magnitude != 0; // 이동 입력이 있는지 확인

        if (isMove)
        {
            // 카메라 앞쪽 방향 벡터 계산
            Vector3 lookForward = new Vector3(mCameraArm.forward.x, 0f, mCameraArm.forward.z).normalized;
            // 카메라 오른쪽 방향 벡터 계산
            Vector3 lookRight = new Vector3(mCameraArm.right.x, 0f, mCameraArm.right.z).normalized;

            // 이동 방향 계산
            Vector3 moveDir = lookForward * _mMoveInput.y + lookRight * _mMoveInput.x;

            // 이동 방향(벡터합) 정규화
            moveDir = Vector3.ClampMagnitude(moveDir, 1f);

            // 플레이어 회전
            if (!playerState.isZoom)
            {
                Quaternion viewRot = Quaternion.LookRotation(moveDir.normalized);
                mPlayerObject.transform.rotation = Quaternion.Lerp(mPlayerObject.transform.rotation, viewRot, Time.deltaTime * 20f);
            }
            
            // 캐릭터 컨트롤러를 이용한 이동
            _characterController.Move(moveDir * (playerSpeed * Time.deltaTime));
        }

        if (_characterController.isGrounded)
        {
            // 점프 입력이 있을 때
            if (Input.GetButton("Jump")) //ntw : GetButtonDown에서 입력이 씹히는 현상이 발견되어 GetButton으로 교체
            {
                _verticalVelocity.y = _jumpForce;
            }
            //바닥에 닿아있으면 중력 가속도가 중첩되어선 안됨
            else
            {
                _verticalVelocity.y = 0;
            }
        }
        else
        {
            // 중력 적용
            _verticalVelocity.y += _gravity * Time.deltaTime;
        }
        
        _characterController.Move(_verticalVelocity * Time.deltaTime);
    }
}