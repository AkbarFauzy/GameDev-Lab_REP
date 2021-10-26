using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Items", menuName = "Items")]
public class Items : ScriptableObject
{
    public string itemNames;
    public string description;
    public Sprite itemIcon;
    public ItemType type;
}
