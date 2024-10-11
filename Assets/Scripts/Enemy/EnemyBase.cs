using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
   적 오브젝트의 체력, 방어력 등 기본 값들을 저장하는 스크립트

    추후 수정사항
        - 스크립터블 오브젝트를 통해 개체값을 받아온다
        - 
*/

public class EnemyBase : MonoBehaviour
{
    public float health = 100;
    public float cooltime;
    public float cooltime_max;
    public float radius;
    public int charaLayer;
    public float speed;
    public float damage;
    Rigidbody rigidbody;

    void Start()
    {
        charaLayer = LayerMask.NameToLayer("Player");
        rigidbody = gameObject.GetComponent<Rigidbody>();
    }
    void Update()
    {
        int layerMask = (1 << charaLayer);
        CheckDeath();
        if (cooltime > 0.0f)
        {
            cooltime -= Time.deltaTime;
        }
        //캐릭터 확인하고 따라가기~
        Collider[] colls = Physics.OverlapSphere(this.transform.position, radius, layerMask);
        if (colls.Length > 0)
        {
            rigidbody.AddForce(Time.deltaTime * speed * (colls[0].transform.position - this.transform.position), ForceMode.Impulse);
            if (colls[0].gameObject != null)
            {
                attack(colls[0].transform.gameObject);
            }
        }
    }

   void CheckDeath()
    {
        if (health < 0)
        {
            Destroy(transform.gameObject);
        }
    }

    public void hit(float damege, Vector3 position = default(Vector3))
    {
        health -= damege;
        rigidbody.AddForce(position, ForceMode.Impulse);
    }

    void attack(GameObject gameObject)
    {
        cooltime = cooltime_max;
        //공격 범위 정해주고, 공격 범위 내에 존재할 때
        if (cooltime == 0)
        {
            //이거 태우가 적고 주석처리하라 한 코드~
            //GetComponent는 당연히 플레이어의 컴포넌트를 가져와야 합니다...
            //다음부터는 GetComponent 앞에 대상을 빼놓지 마세용...
            gameObject.GetComponent<PlayerState>().GetDamage(damage, this.transform.position);
        }
    }
}