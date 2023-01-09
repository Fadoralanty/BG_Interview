using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class InventoryUI : MonoBehaviour
{
    public bool isInventoryOpen;
    [SerializeField] private UIShop UIShop;
    [SerializeField] private GameObject InventoryCanvas;
    [SerializeField] private int _maxItems = 10;
    [SerializeField] private PlayerController player;
    [SerializeField] private GameObject ItemSlotPrefab;
    [SerializeField] private GameObject ItemSlotContainer;
    private Inventory _inventory;
    private List<GameObject> UI_items;

    private void Awake()
    {
        UI_items = new List<GameObject>();
        isInventoryOpen = false;
        InventoryCanvas.SetActive(isInventoryOpen);

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
        isInventoryOpen = !isInventoryOpen;
        InventoryCanvas.SetActive(isInventoryOpen);
        //UpdateInventory();
    }
    public void OnItemAddedListener(Item item) 
    {
        GameObject itemSlot = Instantiate(ItemSlotPrefab, ItemSlotContainer.transform);
        itemSlot.GetComponent<Image>().sprite = item.Icon;
        itemSlot.GetComponent<ToolTipTrigger>().content ="<b>"+ item.Name +"</b>"+"\n" + "Sold for: "+ "<b>" + "<color=yellow>" +item.SellPrice + "$";
        itemSlot.GetComponent<ItemSlot>().Item = item;
        Button button = itemSlot.GetComponent<Button>();
        button.onClick.AddListener(delegate { UIShop.SellItem(item, player); });
        button.onClick.AddListener(delegate { player.EquipItem(item); });
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
        player.OnOpenInventory -= OnOpenInventoryListener;
    }
}
