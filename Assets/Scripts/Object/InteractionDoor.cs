using System.Collections;
using UnityEngine;

public class InteractionDoor : MonoBehaviour, IInteraction
{
    private bool isProcessing = false;
    public string Name { get; private set; } = "Door";

    public GameObject light;
    
    void Interaction(GameObject player)
    {
        player.GetComponent<DialogueParseR>().InteractDialogue("닫혀있다");
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