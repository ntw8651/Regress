using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCollisionForce : MonoBehaviour
{
    /* Rigidbody와 같은 플레이어의 충돌을 구현하는 스크립트
     * ChacterController의 OnControllerColliderHit 함수를 사용하여 구현
     * 
     * 현재 닿은 오브젝트에게 현재 플레이어의 이동 방향 속도에 비례하는 힘을 가한다
     * 충돌 오브젝트에 힘을 가할 때, AddForceAtPosition을 사용하여 충돌 지점에 힘을 가한다
     */

    // 힘의 크기 조절 변수
    public float forceMultiplier = 10f;
    public GameObject player;

    void Start()
    {
        player = transform.gameObject;
    }

    void Update()
    {
        // 플레이어의 이동 로직이 여기에 들어갈 수 있다
    }

    void OnControllerColliderHit(ControllerColliderHit hit)
    {
        // 충돌한 오브젝트의 Rigidbody를 가져오기
        Rigidbody otherRigidbody = hit.collider.attachedRigidbody;
        

        // 충돌한 오브젝트가 Rigidbody를 가지고 있을 때만 힘을 가한다
        if (otherRigidbody != null)
        {
            //플레이어의 현재 가속도에 비례하게 하려 했으나, 충돌 시 플레이어 가속도가 0이 되는 점을 고려하여 변경
            //Vector3 playerVelocity = player.GetComponent<CharacterController>().velocity;

            // 밀어낼 방향 구하기
            Vector3 ForceDirection = hit.collider.transform.position - player.transform.position;

            // 충돌 지점
            Vector3 contactPoint = hit.point;

            //AddForceAtPosition는 오브젝트의 특정 지점을 지정해서 힘을 줄 수 있다 = 회전도 가능
            otherRigidbody.AddForceAtPosition(ForceDirection * forceMultiplier * Time.deltaTime, contactPoint, ForceMode.Impulse);
        }
    }
}
