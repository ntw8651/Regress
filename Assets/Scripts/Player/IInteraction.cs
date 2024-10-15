using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IInteraction
{
    public void Interact(GameObject player);
    public string Name { get; }
}
