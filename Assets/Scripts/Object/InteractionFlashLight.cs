using System.Collections;
using UnityEngine;

public class InteractionFlashLight : MonoBehaviour, IInteraction
{
    public int type = 0;
    private bool isProcessing = false;
    public string Name { get; private set; } = "Item";

    public GameObject light;
    
    void Interaction(GameObject player)
    {
        light.GetComponent<Light>().enabled = true;
        player.GetComponent<DialogueParseR>().InteractDialogue("닫혀있다");
        Destroy(transform.gameObject);
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