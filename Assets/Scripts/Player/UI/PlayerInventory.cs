using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class PlayerInventory : MonoBehaviour
{
    /*
     * 
     * 플레이어가 소지한 아이템을 리스트로 관리하는 스크립트
     * 
     * 
     * 
     */
    public List<Item> items = new List<Item>();
    //FOR DEBUG
    public List<Item> debugAddItems = new List<Item>();

    public GameObject player;
    public GameObject inventoryUI;

    public long weight = 0;

    void Start()
    {
        //FOR DEBUG
        foreach (var i in debugAddItems)
        {
            AddItem(i);
        }


    }

    public void AddItem(Item item)
    {
        // NEED FIX : 아이템 추가를 할 때, 인수로 개수를, 받고 스택 가능의 품목일 경우 합치기
        // Manack 코드 가져와야지
        item = Instantiate(item);

        item.isDropped = false;

        items.Add(item);
        if (transform.GetComponent<PlayerState>().isOpenInventory)
        {
            inventoryUI.GetComponent<InventoryUI>().RefreshInventoryUI();
        }
    }

    public void DropItem(int itemId, int count = 1, Vector3 direction = default(Vector3))
    {
        /*
         * 프리팹 생성해서 떨어뜨리는거는 아이템 스크립트에서 해야할듯
         * 아 그냥 여기서 구현하고 거기서 콜하지 뭐
         * 
         * NEED FIX : 버릴 때, 별도의 Item Stack 검사가 필요한 지 체크 필요
         * NEED ADD : 중요 아이템은 못버리게 설정
         */
        if(direction == null)
        {
            direction = Vector3.zero;
        }
        

        GameObject _dropItem = Instantiate(items[itemId].prefab);
        RemoveItem(itemId, count);
        _dropItem.transform.position = player.transform.position;
        _dropItem.GetComponent<Rigidbody>().AddForce(direction * 20f, ForceMode.Impulse);



    }
    
    /// <summary>
    /// 아이템 개수를 줄이거나, 아예 지워버립니다!
    /// </summary>
    public void RemoveItem(int itemId, int count = 1)
    {
        if (items[itemId].stack > count)
        {
            items[itemId].stack -= count;
        }
        else
        {
            items.Remove(items[itemId]);
        }
    }

    public void SortItem()
    {
        items.Sort();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {

    }


}
