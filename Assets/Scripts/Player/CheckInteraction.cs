using System;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.PlayerLoop;
using UnityEngine.Serialization;

public class CheckInteraction : MonoBehaviour
{
    public TMP_Text interactText;
    private void Update()
    {
        Collider[] _colliders;
        _colliders = Physics.OverlapSphere(transform.position, 3f);
        if (_colliders.Length > 0)
        {
            int _flag = 0;
            foreach (var col in _colliders)
            {
                // ReSharper disable once Unity.PerformanceCriticalCodeInvocation
                IInteration interactable = col.GetComponent<IInteration>();
                if (interactable != null)
                {
                    _flag += 1;
                    // Debug.Log(col.name);
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
                    //NEED FIX : 
                    break;
                }
            }
            if(_flag == 0)
            {
                interactText.text = "";
            }
        }
    }

}