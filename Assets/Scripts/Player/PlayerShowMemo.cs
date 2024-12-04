using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShowMemo : MonoBehaviour
{
    public GameObject memoObject;
    public GameObject memoTMP;
    private bool isShow = false;
    private bool isProcessing = false;
    // 다른 키 입력 막기
    void Start()
    {
        memoObject.SetActive(false);
        
        
    }

    public void Update()
    {
        if (isShow && Input.GetKeyDown(KeyCode.F))
        {
            isShow = false;
            memoObject.SetActive(isShow);
        }
    }
    
    public void ViewMemo(string content)
    {
        if (isProcessing)
        {
            return;
        }
        isProcessing = true;
        isShow = !isShow;
        memoObject.SetActive(isShow);
        memoTMP.GetComponent<TMPro.TextMeshProUGUI>().text = content.Replace("\\n", "\n");
        isProcessing = false;
    }
    
}
