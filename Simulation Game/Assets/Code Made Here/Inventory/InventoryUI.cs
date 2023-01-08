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
    private List<GameObject> UI_items;
    private bool _isInventoryOpen;
    [SerializeField] private GameObject ItemSlotPrefab;
    [SerializeField] private GameObject ItemSlotContainer;
    
    private void Awake()
    {
        UI_items = new List<GameObject>();
        _isInventoryOpen = false;
        InventoryCanvas.SetActive(_isInventoryOpen);

    }

    private void Start()
    {
        _inventory = player.Inventory;
        _maxItems = player.Inventory.MaxItems;
        _inventory.OnItemAdded += OnItemAddedListener;
        _inventory.OnItemRemoved += OnItemRemovedListener;
        player.OnOpenInventory += OnOpenInventoryListener;
    }

    public void OnOpenInventoryListener()
    {
        _isInventoryOpen = !_isInventoryOpen;
        InventoryCanvas.SetActive(_isInventoryOpen);
        //UpdateInventory();
    }
    public void OnItemAddedListener(Item item) 
    {
        GameObject itemSlot = Instantiate(ItemSlotPrefab, ItemSlotContainer.transform);
        itemSlot.GetComponent<Image>().sprite = item.Icon;
        itemSlot.GetComponent<ToolTipTrigger>().content ="<b>"+ item.Name +"</b>"+"\n" + "Sold for: "+"<color=yellow>" +item.SellPrice + "$";
        itemSlot.GetComponent<ItemSlot>().Item = item;
        itemSlot.gameObject.SetActive(true);
        UI_items.Add(itemSlot);
    }
    public void OnItemRemovedListener(Item item) //look for 1 instance of the item and delete it 
    {
        foreach (var itemSlot in UI_items)
        {
            if (itemSlot.GetComponent<ItemSlot>().Item == item)
            {
                GameObject obj = itemSlot;
                UI_items.Remove(itemSlot);
                Destroy(obj);
                break;
            }
        }
    }
    private void OnDestroy()
    {
        _inventory.OnItemAdded -= OnItemAddedListener;
        _inventory.OnItemRemoved -= OnItemAddedListener;
    }
}
