using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.UIElements;
using Image = UnityEngine.UI.Image;

public class ItemCell : MonoBehaviour
    , IPointerEnterHandler
    , IPointerExitHandler
{
    public GameObject image;
    public GameObject text;
    public GameObject background;
    public int id; //클릭 식별용



    public void OnPointerEnter(PointerEventData eventData)
    {
        background.GetComponent<RawImage>().color = new Color32(255, 0, 0, 255);
    }
    public void OnPointerExit(PointerEventData eventData)
    {
        background.GetComponent<RawImage>().color = new Color32(185, 185, 185, 255);
    }


}
