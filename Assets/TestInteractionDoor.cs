using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TestInteractionDoor : MonoBehaviour, IInteraction
{
    // Start is called before the first frame update
    public int type = 0;
    private bool isOpen = false;
    private bool isProcessing = false;
    public string Name { get; private set; } = "Door";
    
    void Interaction()
    {
        isProcessing = true;
        if (isOpen)
        {
            
            for(int i = 0; i < 10; i++)
            {
                transform.Rotate(new Vector3(0, 0, i * 10));
            }
            isOpen = false;
        }
        else
        {
            for (int i = 0; i < 10; i++)
            {
                transform.Rotate(new Vector3(0, 0, -i * 10));
            }
            isOpen = true;
        }
        isProcessing = false;
    }

    public void Interact(GameObject player)
    {
        Debug.Log(transform.rotation);
        Debug.Log(transform.localRotation);
        if (isProcessing == false)
        {
            Interaction();
        }
    }
    
    public Object GetObject()
    {
        return this;
    }
}
