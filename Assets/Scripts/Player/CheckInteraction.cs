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
            int _flag = 0;
            float _minDistance = float.MaxValue;
            GameObject _targetObject = null;
            foreach (var col in _colliders)
            {
                // ReSharper disable once Unity.PerformanceCriticalCodeInvocation
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
        IInteration interactable = targetObject.GetComponent<IInteration>();

        if (interactable.Name == "Door")
        {
            interactText.text = "Press F to open/close the door";
        }
        else if (interactable.Name == "Item")
        {
            interactText.text = "Press F to pick up the item";
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
}