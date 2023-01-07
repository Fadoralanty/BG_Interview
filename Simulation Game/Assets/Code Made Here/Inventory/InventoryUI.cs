using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryUI : MonoBehaviour
{
    [SerializeField] private int _maxItems = 10;
    private Inventory _inventory;
    private void Awake()
    {
        _inventory = new Inventory(_maxItems);
        _inventory.OnItemAdded += UpdateInventory;
        _inventory.OnItemRemoved += UpdateInventory;
    }

    private void UpdateInventory()
    {
        foreach (var item in _inventory.Items)
        {
            
        }
    }

    private void OnDestroy()
    {
        _inventory.OnItemAdded -= UpdateInventory;
        _inventory.OnItemRemoved -= UpdateInventory;
    }
}
