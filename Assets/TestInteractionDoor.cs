using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TestInteractionDoor : MonoBehaviour, IInteration
{
    // Start is called before the first frame update
    public int type = 0;
    private bool isOpen = false;
    private bool isProcessing = false;
    public string Name { get; private set; } = "Door";
    
    void Interaction()
    {

    }

    public void Interact(GameObject player)
    {
        Debug.Log(transform.name + " is interacted");
        Interaction();
        
    }
    
    public Object GetObject()
    {
        return this;
    }
}
