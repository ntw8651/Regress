using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/*
 *  모든 플레이어 스테이터스 및 정보 저장 스크립트 
 * 
 *  공격 중인지, 앉아있는 중인지, Zoom 상태인지, 등.
 *  
 *  스크립트가 많아졌을 때, 현재 상태를 보다 쉽게 
 *  체크하고 관리하기 위한 스크립트.
 *  
 *  
 *  추후에 public이 아닌 private로 지정하고 getset을 통해 값 지정해주는 방식 고려
 *  << 정확히 뭔지 모르니 일단은 public으로 냅다 
 *  
 *  또한 매번 다른 스크립트에서 GetComponent<~>....형식으로 받아오는 게 자원 손실이 크다고 들은 바가 있기에
 *  대안 필요
 */
public class PlayerState : MonoBehaviour
{
    // Start is called before the first frame update

    public float Health = 100;

    public bool isZoom = false;

    public bool isOpenInventory = false;
    public void GetDamage(float damage, Vector3 pos = default(Vector3))
    {
        
    }
}
