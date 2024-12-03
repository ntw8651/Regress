using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    public Button[] interactButtons;
    
    public void SetActiveButton(bool isActive)
    {
        foreach (var button in interactButtons)
        {
            button.interactable = isActive;
        }
    }
}
