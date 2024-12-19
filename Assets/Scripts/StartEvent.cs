using Cinemachine;
using System.Collections;
using UnityEngine;

public class StartEvent : MonoBehaviour
{
    [SerializeField]
    private GameObject player;

    public GameObject cameraObject; // 카메라 오브젝트
    
    
    
    void Start()
    {
        StartCoroutine(ExecuteFunctions());
    }

    IEnumerator ExecuteFunctions()
    {
        // Wait for 1 second
        yield return new WaitForSeconds(1f);
        FirstFunction();

        // Wait for another 1 second
        yield return new WaitForSeconds(3f);
        SecondFunction();
        
        yield return new WaitForSeconds(3f);
        ThirdFunction();
    }

    void FirstFunction()
    {
        player.GetComponent<DialogueParseR>().InteractDialogue("낯선천장");
        
    }

    void SecondFunction()
    {
        player.GetComponent<DialogueParseR>().InteractDialogue("여긴어디");
        cameraObject.GetComponent<CinemachineDollyCart>().enabled = true;   
        
    }
    
    void ThirdFunction()
    {
        cameraObject.GetComponent<CinemachineDollyCart>().enabled = false;
        cameraObject.GetComponent<PlayerCameraFirst>().enabled = true;
        player.GetComponent<PlayerMovement>().mPlayerObject.SetActive(true);
        player.GetComponent<PlayerMovement>().enabled = true;
    }
}