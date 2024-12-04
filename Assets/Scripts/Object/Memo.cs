using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Memo : MonoBehaviour, IInteraction
{
    public string memo;
    public string Name { get; private set; } = "Memo";
    
    public void Interact(GameObject player)
    {
        player.GetComponent<PlayerShowMemo>().ViewMemo(memo);
    }
    
    
}
