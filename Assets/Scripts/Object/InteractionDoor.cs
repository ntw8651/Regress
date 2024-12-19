using System.Collections;
using UnityEngine;

public class InteractionDoor : MonoBehaviour, IInteraction
{
    private bool isProcessing = false;
    public string Name { get; private set; } = "Door";

    
    void Interaction(GameObject player)
    {
        bool value = false;
        if (player.GetComponent<PlayerState>().UserVariableBools.TryGetValue("getDoorKey", out value))
        {
            if (value == false)
            {
                player.GetComponent<DialogueParseR>().InteractDialogue("닫힌문");
                return;
            }
        }
        else
        {
            player.GetComponent<DialogueParseR>().InteractDialogue("닫힌문");
            return;
        }
        player.GetComponent<DialogueParseR>().InteractDialogue("열린문");
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