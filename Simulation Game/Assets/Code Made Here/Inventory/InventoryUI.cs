using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class InventoryUI : MonoBehaviour
{
    [SerializeField] private GameObject InventoryCanvas;
    [SerializeField] private int _maxItems = 10;
    [SerializeField] private PlayerController player;
    private Inventory _inventory;
    private List<Item> UI_items;
    private bool _isInventoryOpen;
    [SerializeField] private GameObject ItemSlotPrefab;
    [SerializeField] private GameObject ItemSlotContainer;
    
    private void Awake()
    {
        UI_items = new List<Item>();
        _isInventoryOpen = false;
        InventoryCanvas.SetActive(_isInventoryOpen);

    }

    private void Start()
    {
        _inventory = player.Inventory;
        _maxItems = player.Inventory.MaxItems;
        _inventory.OnItemAdded += UpdateInventory;
        _inventory.OnItemRemoved += UpdateInventory;
        player.OnOpenInventory += OnOpenInventoryListener;
    }

    private void OnOpenInventoryListener()
    {
        _isInventoryOpen = !_isInventoryOpen;
        InventoryCanvas.SetActive(_isInventoryOpen);
        UpdateInventory();
    }
    private void UpdateInventory() 
    {
        foreach (var item in _inventory.Items)
        {
            if (!UI_items.Contains(item))
            {
                UI_items.Add(item);
                GameObject itemSlot = Instantiate(ItemSlotPrefab, ItemSlotContainer.transform);
                itemSlot.GetComponent<Image>().sprite = item.Icon;
                itemSlot.GetComponent<ItemSlot>().item = item;
                itemSlot.gameObject.SetActive(true);
            }
        }
    }

    private void OnDestroy()
    {
        _inventory.OnItemAdded -= UpdateInventory;
        _inventory.OnItemRemoved -= UpdateInventory;
    }
}
