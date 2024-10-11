using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class InventoryUI : MonoBehaviour
{
    /*
     * 플레이어가 아이템과 상호작용하는 스크립트
     * 플레이어가 UI와 상호작용해서 보낸 요청은 실질적으로 
     * PlayerInventory.cs에서 처리된다
     * 
     * 
     */
    private List<Item> itemsOrigin = new List<Item>();
    public GameObject player;
    public GameObject itemCellPrefab;

    private List<Item> items;
    [SerializeField]
    private Canvas canvas;
    private GraphicRaycaster gRaycaster;


    public GameObject background;
    private PlayerState playerState;

    [Header("Only Check")]
    public GameObject itemCells;

    private PlayerInventory playerInventory;


    private enum InventoryState
    {
        WeaponWindow,
        etc,
    }

    void Start()
    {
        playerState = player.GetComponent<PlayerState>();
        gRaycaster = canvas.GetComponent<GraphicRaycaster>();
        playerInventory = player.GetComponent<PlayerInventory>();
    }

    // Update is called once per frame
    void Update()
    {

        //Inventory OpenCheck
        if (Input.GetKeyDown(KeyCode.I))
        {
            if (playerState.isOpenInventory)
            {
                playerState.isOpenInventory = false;
                CloseInventory();
            }
            else
            {
                playerState.isOpenInventory = true;
                OpenInventory();
            }
        }

        //Opened Inventory... 나중에 마우스 raycast UI부모 스크립트에 다 통합시키기
        if (playerState.isOpenInventory)
        {

            //NEED FIX : 마우스 위치 아이템 가져오기 함수화
            if (Input.GetMouseButtonDown(0))
            {
                var ped = new PointerEventData(null);
                ped.position = Input.mousePosition;
                List<RaycastResult> results = new List<RaycastResult>();
                gRaycaster.Raycast(ped, results);
                if (results.Count <= 0)
                {
                    return;
                }
                if (results[0].gameObject.transform.tag == "ItemCell")
                {
                    int itemId = results[0].gameObject.transform.GetComponent<ItemCell>().id;
                    SelectItem(items[itemId]);
                }

            }
            if (Input.GetKeyDown(KeyCode.Q))
            {
                //drop item
                var ped = new PointerEventData(null);
                ped.position = Input.mousePosition;
                List<RaycastResult> results = new List<RaycastResult>();
                gRaycaster.Raycast(ped, results);
                
                if (results.Count <= 0)
                {
                    return;
                }

                if (results[0].gameObject.transform.tag == "ItemCell")
                {
                    int itemId = results[0].gameObject.transform.GetComponent<ItemCell>().id;
                    //Need Fix : PlayerObject를... 분리하기
                    playerInventory.DropItem(itemId, direction:player.GetComponent<PlayerMovement>().mPlayerObject.transform.forward);
                    RefreshInventoryUI();
                }
            }
        }
    }

    void SelectItem(Item item)
    {

    }

    public void RefreshInventoryUI()
    {
        // Need Fix -> Clear All ItemCell Function으로 바꾸기
        foreach (Transform child in itemCells.transform)
        {
            Destroy(child.gameObject);
        }


        GetInventoryData();
        SortItems();
        for (int i = 0; i < items.Count; i++)
        {
            CreateItemCell(items[i], i);
        }

    }
    void GetInventoryData()
    {
        itemsOrigin = player.GetComponent<PlayerInventory>().items;

    }
    void CreateItemCell(Item item, int count)
    {
        GameObject itemCell = Instantiate(itemCellPrefab, itemCells.transform);
        itemCell.GetComponent<ItemCell>().text.GetComponent<TextMeshProUGUI>().SetText(item.displayName);
        itemCell.GetComponent<ItemCell>().image.GetComponent<Image>().sprite = item.displaySprite;

        float _cellHeight = itemCell.GetComponent<ItemCell>().background.GetComponent<RectTransform>().rect.height;

        itemCell.GetComponent<RectTransform>().localPosition = new Vector3(0, _cellHeight*4 - count * _cellHeight * 1.2f, 0);
        itemCell.GetComponent<ItemCell>().id = count;
    }
    void SortItems()
    {
        items = itemsOrigin;
    }
    public void OpenInventory()
    {
        //시간 정지시키면서 막 그러는 기능은 인벤토리에 넣을까 아니면 키인펏컨트롤러 그런걸 만들어서 거기에서 처리할까 ㅇ므 고민되네 일단은 여따가 박아두고서 나중에 옮기자
        //라고 생각했는데 생각해보니까 Enable안된상태로 보관할거잖아
        background.SetActive(true);
        RefreshInventoryUI();

    }
    public void CloseInventory()
    {
        foreach (Transform child in itemCells.transform)
        {
            Destroy(child.gameObject);
        }
        background.SetActive(false);
    }
}
