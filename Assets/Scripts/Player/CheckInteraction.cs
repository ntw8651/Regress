using System;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.PlayerLoop;
using UnityEngine.Serialization;

public class CheckInteraction : MonoBehaviour
{
    /* 플레이어 상호작용 조작 스크립트
     * 주변에 상호작용 가능 물품이 있을 시, 인터페이스로 상호작용 함
     * 
     */
    public TMP_Text interactText;
    public GameObject targetObject; // 상호작용 대상 오브젝트, 추후 다른 스크립트에서 참조할 수 있도록 public으로 선언
    
    [SerializeField]
    private float maxDistance = 3f;
    private void Update()
    {
        Collider[] _colliders;
        _colliders = Physics.OverlapSphere(transform.position, 3f);
        // 상호작용 가능 개체 탐색
        // NEED FIX : 함수화 하여 Update를 깔끔히 할 것
        
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, 3f) && hit.collider.GetComponent<IInteration>() != null)
        {
            // 여기에 layermask 거꾸로 해서 날리고, 만약 없으면
            // OK, 근데 만약 layermask거꾸로 해서 뭔가 걸리면 사이에 오브젝트가 있으므로
            // 레이케스팅 실패

            if (targetObject != hit.collider.gameObject)
            {
                //Debug.DrawRay(ray.origin, ray.direction * 20, Color.red, 10f);
                if (targetObject != null)
                {
                    Destroy(targetObject.GetComponent<Outline>());
                }

                targetObject = hit.collider.gameObject;
                targetObject.AddComponent<Outline>();
            }
        }
        else
        {
            if (_colliders.Length > 0)
            {
                float _minDistance = float.MaxValue;
                GameObject _targetObject = null;
                foreach (var col in _colliders)
                {
                    IInteration interactable = col.GetComponent<IInteration>();
                
                    if (interactable != null)
                    {
                        if (_minDistance > Vector3.Distance(transform.position, col.transform.position))
                        {
                            _minDistance = Vector3.Distance(transform.position, col.transform.position);
                            _targetObject = col.gameObject;
                        }
                    }
                }


                if (_targetObject != null)
                {
                    if (targetObject != _targetObject)
                    {
                        if (targetObject != null)
                        {
                            Destroy(targetObject.GetComponent<Outline>());
                        }

                        targetObject = _targetObject;
                        targetObject.AddComponent<Outline>();
                    }
                }
                else
                {
                    if (targetObject != null)
                    {
                        Destroy(targetObject.GetComponent<Outline>());
                    }
                    targetObject = null;
                }
            }
            else if (targetObject != null)
            {
                Destroy(targetObject.GetComponent<Outline>());
                targetObject = null;
            }
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
        IInteration interactable = targetObject.GetComponent<IInteration>();

        if (interactable.Name == "Door")
        {
            interactText.text = "Press F to open/close the door";
        }
        else if (interactable.Name == "Item")
        {
            interactText.text = "Press F to pick up the item";
        }
        else if(interactable.Name == "NPC")
        {
            interactText.text = "Press F to talk to the NPC.";
        }
        else
        {
            // 해당 부분은 미정 상태
            interactText.text = "Press F to pick up " + interactable.Name;
        }

        if (Input.GetKeyDown(KeyCode.F))
        {
            // ReSharper disable once Unity.PerformanceCriticalCodeInvocation
            interactable.Interact(transform.gameObject);

        }
    }

}