using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class PlayerControl : MonoBehaviour
{
    // Start is called before the first frame update
    /*
     *          플레이어 조작 관련 스크립트
     * 근접 & 원거리 공격 & 줌
     * 이동을 제외한 키보드 & 마우스 인풋 처리
     * 
     * 
     * 
     */


    public GameObject testObj;
    public GameObject testBullet;



    private GameObject player; // 이미 플레이어가 달려 있으나, 보다 명확하게 처리하기 위해 player 변수를 따로 지정함
    private PlayerState playerState;

    public float rangedAttackSpeed = 1f;
    private float rangedAttackFullTime = 10f;
    private float rangedAttackTime = 0f;

    public float TestRangeAttackSpeed = 100f;

    void Start()
    {
        player = transform.gameObject;
        playerState = player.GetComponent<PlayerState>();
    }

    // Update is called once per frame
    void Update()
    {
        /*
         * 여기 좀 바꿀 필요가 있음. 깔끔하게
         */
        if (Input.GetMouseButtonDown(1))
        {
            playerState.isZoom = true;
        }
        if (Input.GetMouseButtonUp(1))
        {
            playerState.isZoom = false;
        }


        

        if (playerState.isZoom)
        {
            Zoom();
            if (Input.GetMouseButton(0))
            {
                RangedAttack();
            }
        }
        else
        {
            if (Input.GetMouseButton(0))
            {
                MeleeAttack();
            }
        }

        
    }
    /// <summary>
    /// 대충 시간마다 때리게 바꾸기
    /// 구현 내용
    /// - 마우스 방향 바라보기
    /// - 바라보는 방향 부채꼴로 때리기
    /// - 부채꼴 공격하는 모양 띄우기(대충 원기둥 휘두르기)
    /// </summary>
    void MeleeAttack()
    {
        // 시간 체크
        

        // 바라보기


        // 공격


        // 
    }


    void Zoom()
    {
        /*
         * 카메라를 기준으로 레이케스팅해서 지면 좌표를 받아오고
         * 해당 정보를 바탕으로 공격한다
         * 
         * 개선 방안 : 레이케스팅을 실제 지면 기반이 아닌, 전용 레이어를 만들고 그라운드에 씌운다.
         *          그리고 해당 그라운드를 플레이어에게 귀속시킨다. 이리 하면 플레이어 중심으로 좌표 설정이 가능하다
         */


        Vector3 _cursorPosition = Input.mousePosition;
        //_cursorPosition.z = Camera.main.farClipPlane;

        Ray _targetRay = Camera.main.ScreenPointToRay(_cursorPosition); // 레이 보낼 위치 정하기
        RaycastHit _hit; // Raycasting한 결과가 담길 변수
        if (Physics.Raycast(_targetRay, out _hit))
        {
            // NEED FIX : 해당 point 좌표 y만 플레이어 값으로 바꾸기
            Vector3 _targetPosition = _hit.point;
            _targetPosition.y = player.transform.position.y;
            testObj.transform.position = _targetPosition;


            // NEED ADD : 줌 상태일 때, 커서 쪽으로 카메라와 플레이어 절반 위치로 camera_move target 위치 바꾸기 
            // 일단은 방향만 바라보도록
            Quaternion viewRot = Quaternion.LookRotation(_targetPosition - player.transform.position);
            player.transform.GetComponent<PlayerMovement>().mPlayerObject.transform.rotation = Quaternion.Lerp(player.transform.GetComponent<PlayerMovement>().mPlayerObject.transform.rotation, viewRot, Time.deltaTime * 20f);
            // NEED FIX : 플레이어 모델 링크도 PlayerState에 귀속하거나, Player 자체를 회전하는 방식으로 변경
        }


    }   

    /// <summary>
    /// 줌 상태일 때 레이케스팅 약간 반경 줘서 쏘고
    /// 
    /// 적 맞으면 데미지
    /// </summary>
    void RangedAttack()
    {
        if(rangedAttackTime > 0)
        {
            rangedAttackTime -= Time.deltaTime * rangedAttackSpeed;
            return;
        }
        rangedAttackTime = rangedAttackFullTime; 

        //raycast 후 공격
        Vector3 _direction = player.transform.GetComponent<PlayerMovement>().mPlayerObject.transform.rotation * Vector3.forward;
        //플레이어가 바라보는 방향으로 레이 케스팅
        GameObject _bullet = Instantiate(testBullet, player.transform.position+_direction*1f, Quaternion.identity);
        _bullet.GetComponent<Rigidbody>().AddForce(_direction * TestRangeAttackSpeed, ForceMode.Impulse);
        Destroy(_bullet, 2f);
        /*
        RaycastHit _hit;
        if (Physics.Raycast(transform.position, _direction, out _hit, 100f))
        {
            Debug.Log(_hit.collider.name);
            _hit.point
        }
        */

    }
}
