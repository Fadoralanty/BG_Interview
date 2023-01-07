using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class InventoryUI : MonoBehaviour
{
    [SerializeField] private GameObject InventoryCanvas;
    [SerializeField] private int _maxItems = 10;
    [SerializeField] private PlayerController player;
    private Inventory _inventory;
    private bool _isInventoryOpen;
    
    private void Awake()
    {
        _inventory = new Inventory(_maxItems);
        _inventory.OnItemAdded += UpdateInventory;
        _inventory.OnItemRemoved += UpdateInventory;
        _isInventoryOpen = false;
        InventoryCanvas.SetActive(_isInventoryOpen);

    }

    private void Start()
    {
        player.OnOpenInventory += OnOpenInventoryListener;
    }

    private void OnOpenInventoryListener()
    {
        _isInventoryOpen = !_isInventoryOpen;
        InventoryCanvas.SetActive(_isInventoryOpen);
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
