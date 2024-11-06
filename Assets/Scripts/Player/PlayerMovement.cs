using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.Serialization;

public class PlayerMovement : MonoBehaviour
{
    /* Player 이동 관련 스크립트
     * 기능 : x-z 평면 이동, 점프, 중력 적용
     * 사용 컴포넌트 : CharacterController
     *
     * 주목사항 : 점프 관련 함수 최적화(CharacterController.isGrounded -> BoxCast로 변경)
     * 
     * 추후 보완 사항 : 함수 및 변수명 일관화 필요, 점프 쿨타임 고려
     */
    private GameObject player;
    private PlayerState playerState;


    public float playerSpeed = 5.0f;          // 플레이어 이동 속도
    public float _jumpForce = 3.0f;            // 점프 힘
    public float _gravity = -9.81f;            // 중력 가속도
    //중력과 점프 힘을 적절히 조절할 것
    
    public Transform mCameraArm;              // 카메라 암 Transform
    
    public GameObject mPlayerObject;          // 플레이어 오브젝트
    
    private CharacterController _characterController; // 캐릭터 컨트롤러 컴포넌트


    //Ground Checking
    [SerializeField]
    private bool isGround;
    private int playerLayerMask;//~Player임(그대로 사용 시, 플레이어만 무시함)
    [SerializeField]
    private float playerFootSize;//몸통에서 아래쪽으로 케스팅 시작할 거리(클수록 아래)
    
    private Vector3 _mMoveInput;              // 플레이어 이동 입력값
    private Vector3 _verticalVelocity;        // 수직 속도
    
    private void Start()
    {
        _characterController = GetComponent<CharacterController>(); // 캐릭터 컨트롤러 컴포넌트 가져오기
        player = transform.gameObject;
        playerState = player.GetComponent<PlayerState>();
        playerLayerMask = (-1) - (1 << LayerMask.NameToLayer("Player"));
    }
    private void Update()
    {
        _mMoveInput = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
        Move();
        GroundCheck();
    }
    private void OnDrawGizmos()
    {
        // Unity Editor상에서 캐릭터의 바닥에 캐스팅되는 박스를 그려줌, 디버그용
        // NEED DELETE : 이 코드는 점프 코드가 안정화 될 때까지 유지시키고, 안정화 상태라면 삭제해주시길 바랍니다
        Gizmos.color = Color.cyan;
        Gizmos.DrawCube(transform.position + Vector3.down * playerFootSize, new Vector3(0.4f, 0.2f, 0.4f));
    }
    private void GroundCheck()
    {
        //boxcast를 이용하여 땅에 닿았는지 확인
        RaycastHit hit;
        if (Physics.BoxCast(transform.position, new Vector3(0.4f, 0.2f, 0.4f), Vector3.down, out hit, Quaternion.identity, playerFootSize, playerLayerMask))
        {
            isGround = true;
        }
        else
        {
            isGround = false;
        }
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
        //GroundChecker.GetComponent<CheckGround>().isGrounded
        if (isGround)
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