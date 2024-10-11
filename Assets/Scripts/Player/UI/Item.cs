using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Item")]

public class Item : ScriptableObject
{
    public string displayName;

    public Sprite displaySprite; // inventory show simply small image
    public GameObject prefab; // throwable having collision Object

    public int stack = 0;
    public bool isDropped = true;

}
