using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IInteration
{
    public void Interact(GameObject player);
    public string Name { get; }
}
