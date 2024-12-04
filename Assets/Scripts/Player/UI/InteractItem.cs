using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractItem : MonoBehaviour, IInteraction
{
    /* "획득" 가능한 아이템으로써, 아이템의 정보를 가지고 있는 스크립트
     * 
     * IInteraction을 갖고 있어서 상호작용 한다.
     * 
     */
    public string variableName;
    public string Name { get; private set; }

    public void Interact(GameObject player)
    {
        //NEED ADD : 픽업 시 남은 공간이 부족하면 인벤토리에 추가하지 않는다. if문 추가 필요
        player.GetComponent<PlayerState>().UserVariableBools[variableName] = true;
        Destroy(transform.gameObject);
    }

    void Start()
    {
    }
}
