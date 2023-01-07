using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.Timeline.Actions;
using UnityEngine;

public class Inventory
{

    public Action OnItemAdded;
    public Action OnItemRemoved;
    public int MaxItems => maxItems;
    private int maxItems;
    public List<Item> Items => items;
    private List<Item> items;

    public Inventory()
    {
        maxItems = 10;
        items = new List<Item>();
    }
    public Inventory(int maxItems)
    {
        this.maxItems = maxItems;
        items = new List<Item>();
    }

    public void AddItem(Item item)
    {   
        if (items.Count + 1 > maxItems) return; 
        items.Add(item);
        OnItemAdded.Invoke();
    }    
    public void RemoveItem(Item item)
    {
        if (items.Contains(item))
        {
            items.Remove(item);
            OnItemRemoved.Invoke();
        }
    }

    public bool ContainsItem(Item item)
    {
        return items.Contains(item);
    }
    
}
