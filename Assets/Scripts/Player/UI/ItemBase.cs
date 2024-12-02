using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemBase : MonoBehaviour, IInteraction
{
    /* "획득" 가능한 아이템으로써, 아이템의 정보를 가지고 있는 스크립트
     * 
     * IInteraction을 갖고 있어서 상호작용 한다.
     * 
     */
    public Item itemOrigin;
    public Item item;

    public string Name => item.displayName; // =>는 대략 포인터라고 보면 된다. Name을 호출하면 item.displayName를 호출하게 된다.
    

    public void Interact(GameObject player)
    {
        //NEED ADD : 픽업 시 남은 공간이 부족하면 인벤토리에 추가하지 않는다. if문 추가 필요
        player.GetComponent<PlayerInventory>().AddItem(item);
        Destroy(transform.gameObject);

    }

    void Start()
    {
        if (item == null)
        {
            // Origin Item 정보를 받아와서 자신의 것으로 복사한다. (Initialize)
            item = Instantiate(itemOrigin);
        }
    }
}
