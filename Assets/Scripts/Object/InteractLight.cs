using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class InteractLight : MonoBehaviour, IInteraction
{
    // Start is called before the first frame update
    public int type = 0;
    private bool isTrunOn = false;
    private bool isProcessing = false;
    public string Name { get; private set; } = "Light";

    [SerializeField]
    private GameObject lightObject;

    void Start()
    {
        isTrunOn = lightObject.GetComponent<Light>().enabled;
    }
    void Interaction()
    {
        if (isProcessing)
        {
            return;
        }
        isProcessing = true;
        isTrunOn = !isTrunOn;
        lightObject.GetComponent<Light>().enabled = isTrunOn;
        
        isProcessing = false;
    }

    public void Interact(GameObject player)
    {
        Interaction();
    }
    
    public Object GetObject()
    {
        return this;
    }
}
