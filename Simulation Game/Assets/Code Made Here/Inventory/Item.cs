using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "New Item",menuName = "Item/Create New Item")]
public class Item :ScriptableObject
{
    public enum ItemType
    {
        Consumable,
        Clothes
    }
    public int Id;
    public string Name;
    public int BuyPrice;
    public int SellPrice;
    public Sprite Icon;
    public ItemType Type;
}
