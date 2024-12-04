using System.Collections;
using UnityEngine;

public class InteractionLadder : MonoBehaviour, IInteraction
{
    public int type = 0;
    private bool isProcessing = false;
    public string Name { get; private set; } = "Ladder";

    [SerializeField]
    private GameObject targetObject; // 이동할 목표 오브젝트

    [SerializeField]
    private GameObject cameraObject; // 카메라 오브젝트
    private CameraEffector cameraEffector; // CameraEffector 스크립트 참조
    
    void Start()
    {
        cameraEffector = cameraObject.GetComponent<CameraEffector>();
    }

    void Interaction(GameObject player)
    {
        if (isProcessing)
        {
            return;
        }
        isProcessing = true;

        StartCoroutine(HandleInteraction(player));

        
    }

    IEnumerator HandleInteraction(GameObject player)
    {
        // 화면 어둡게 하기
        yield return StartCoroutine(cameraEffector.FadeInCorutine(1f));

        // 플레이어 위치와 회전 변경
        Debug.Log(player);
        Debug.Log(player.transform.position);
        player.transform.position = targetObject.transform.position;
        player.transform.rotation = targetObject.transform.rotation;
        Debug.Log(player.transform.position);
        
        // 화면 밝게 하기
        yield return StartCoroutine(cameraEffector.FadeOutCorutine(1f));
        isProcessing = false;
    }

    public void Interact(GameObject player)
    {
        Interaction(player);
    }

    public Object GetObject()
    {
        return this;
    }
}