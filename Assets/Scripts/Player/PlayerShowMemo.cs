using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShowMemo : MonoBehaviour
{
    public GameObject memoObject;
    public GameObject memoTMP;
    private int isShow = 0;
    private bool isProcessing = false;
    // 다른 키 입력 막기
    void Start()
    {
        memoObject.SetActive(false);
        
        
    }

    public void Update()
    {
        if (isShow > 1&& Input.GetKeyDown(KeyCode.F))
        {
            isShow = 0;
            memoObject.SetActive(false);
        }
        else if(Input.GetKeyDown(KeyCode.F))
        {
            isShow += 1;
        }
    }
    
    public void ViewMemo(string content)
    {
        if (isProcessing)
        {
            return;
        }
        isProcessing = true;
        isShow = 1;
        memoObject.SetActive(true);
        memoTMP.GetComponent<TMPro.TextMeshProUGUI>().text = content.Replace("\\n", "\n");
        isProcessing = false;
    }
    
}
