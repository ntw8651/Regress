using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuUI : MonoBehaviour
{
    /*
     * 
     * 특정 UI를 클릭하면 Main Menu Scene에서 MainScene으로 이동하는 스크립트
     * 
     * 
     * 
     */
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space))
        {
            SceneManager.LoadScene("MainScene");
        }
    }
}
