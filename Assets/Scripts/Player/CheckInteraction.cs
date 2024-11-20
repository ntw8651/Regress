using System;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.PlayerLoop;
using UnityEngine.Serialization;

public class CheckInteraction : MonoBehaviour
{
    public TMP_Text interactText;
    public GameObject targetObject;
  
    private void Update()
    {
        Collider[] _colliders;
        _colliders = Physics.OverlapSphere(transform.position, 3f);
        if (_colliders.Length > 0)
        {
            float _minDistance = float.MaxValue;
            GameObject _targetObject = null;
            foreach (var col in _colliders)
            {
                // ReSharper disable once Unity.PerformanceCriticalCodeInvocation
                IInteraction interactable = col.GetComponent<IInteraction>();
                
                if (interactable != null)
                {
                    if (_minDistance > Vector3.Distance(transform.position, col.transform.position))
                    {
                        _minDistance = Vector3.Distance(transform.position, col.transform.position);
                        _targetObject = col.gameObject;
                    }
                }

            }

            targetObject = _targetObject;
        }
        Interact();
    }

    public void Interact()
    {
        if(targetObject == null)
        {
            interactText.text = "";
            return;
        }
        IInteraction interactable = targetObject.GetComponent<IInteraction>();

        if (interactable.Name == "Door")
        {
            interactText.text = "Press F to open/close the door";
        }
        else if (interactable.Name == "Item")
        {
            interactText.text = "Press F to pick up the item";
        }
        else if(interactable.Name == "ITR")
        {
            interactText.text = "Press F to talk to the NPC.";
        }
        else if (interactable.Name == "III")
        {
            interactText.text = "상호작용";
        }
        else
        {
            interactText.text = "Press F to pick up " + interactable.Name;
        }

        if (Input.GetKeyDown(KeyCode.F))
        {
            // ReSharper disable once Unity.PerformanceCriticalCodeInvocation
            interactable.Interact(transform.gameObject);

        }
    }
    
    public void DebugDialogue(TalkData[] talkDatas)
    {
        for (int i = 0; i < talkDatas.Length; i++)
        {
            // 캐릭터 이름 출력
            Debug.Log(talkDatas[i].name);
            // 대사들 출력
            foreach (string context in talkDatas[i].contexts) 
                Debug.Log(context);
        }
    }

}