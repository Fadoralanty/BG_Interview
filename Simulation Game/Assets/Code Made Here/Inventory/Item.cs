using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum itemType
{
    Consumable,
    Clothes
}
public class Item
{
    private string Name;
    //private int Amount;
    private float SellPrice;
    public Sprite Icon;
    private itemType Type;
    public Item(string name, float sellPrice, Sprite icon, itemType type)
    {
        Name = name;
        SellPrice = sellPrice;
        Icon = icon;
        Type = type;
        //Amount = 1;
    }
}
