 using System;
 using System.Collections;
using System.Collections.Generic;
using UnityEngine;
 using UnityEngine.UI;

 public class UIShop : MonoBehaviour
{ 
    public Action OnShopOpen;
    public Action OnShopClose;
    public Action OnNotEnoughMoney;
    [SerializeField] private GameObject ShopItemTemplatePrefab;
    [SerializeField] private GameObject Container;
    [SerializeField] private Transform panel;
    [SerializeField] private List<Item> Items;
    [SerializeField] private PlayerController player;
    private bool _isShopUIOpen;

    private void Awake()
    {
        _isShopUIOpen = false;
    }

    private void Start()
    {
        foreach (var item in Items)
        {
            CreateItemButton(item);
        }
        CloseShopUI();
    }

    private void CreateItemButton(Item item)
    {
        GameObject newItem = Instantiate(ShopItemTemplatePrefab, panel);
        newItem.GetComponent<ToolTipTrigger>().content="<b>"+ item.Name +"</b>"+"\n" + "Sold for: "+"<color=yellow>" +item.BuyPrice + "$";
        newItem.transform.Find("Icon").GetComponent<Image>().sprite = item.Icon;
        Button button = newItem.transform.Find("Icon").GetComponent<Button>();
        button.onClick.AddListener(delegate { BuyItem(item, player); });
    }
    
    public void BuyItem(Item item, PlayerController player)
    {
        if (!Items.Contains(item)) Debug.Log("Item does not exist in item List");

        if (player.Wallet.CurrentMoney>=item.BuyPrice)
        {
            player.Wallet.LoseMoney(item.BuyPrice);
            player.Inventory.AddItem(item);
        }
        else
        {
            OnNotEnoughMoney?.Invoke();
        }
    }

    public void SellItem(Item item, PlayerController player)
    {
        if (!_isShopUIOpen) return;
        
        if (player.Inventory.ContainsItem(item))
        {
            player.Inventory.RemoveItem(item);
            player.Wallet.GetMoney(item.SellPrice);
        }
    }

    public void OpenShopUI()
    {
        _isShopUIOpen = true;
        Container.SetActive(_isShopUIOpen);
        OnShopOpen?.Invoke();
    }
    public void CloseShopUI()
    {
        _isShopUIOpen = false;
        Container.SetActive(_isShopUIOpen);
        OnShopClose?.Invoke();
    }
}
