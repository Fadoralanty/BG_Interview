using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.Timeline.Actions;
using UnityEngine;

public class Inventory
{

    private int MaxItems;
    public List<Item> Items;
    public Action OnItemAdded;
    public Action OnItemRemoved;

    public Inventory()
    {
        MaxItems = 10;
        Items = new List<Item>();
    }
    public Inventory(int maxItems)
    {
        MaxItems = maxItems;
        Items = new List<Item>();
    }

    private void AddItem(Item item)
    {   
        if (Items.Count + 1 > MaxItems) return; 
        Items.Add(item);
        OnItemAdded.Invoke();
    }    
    private void RemoveItem(Item item)
    {
        if (Items.Contains(item))
        {
            Items.Remove(item);
            OnItemRemoved.Invoke();
        }
    }
    
}
