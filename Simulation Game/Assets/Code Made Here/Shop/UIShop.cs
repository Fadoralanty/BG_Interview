 using System;
 using System.Collections;
using System.Collections.Generic;
using UnityEngine;
 using UnityEngine.UI;

 public class UIShop : MonoBehaviour
{ 
    [SerializeField] private GameObject ShopItemTemplatePrefab;
    [SerializeField] private Transform panel;
    [SerializeField] private List<Item> Items;
    private void Start()
    {
        foreach (var item in Items)
        {
            CreateItemButton(item);
        }
    }

    private void CreateItemButton(Item item)
    {
        GameObject newItem = Instantiate(ShopItemTemplatePrefab, panel);
        newItem.GetComponent<ToolTipTrigger>().content="<b>"+ item.Name +"</b>"+"\n" + "Sold for: "+"<color=yellow>" +item.BuyPrice + "$";
        newItem.transform.Find("Icon").GetComponent<Image>().sprite = item.Icon;
    }
    
    public void BuyItem(Item item, PlayerController player)
    {
        if (!Items.Contains(item)) Debug.Log("Item does not exist in item List");

        if (player.Wallet.CurrentMoney>=item.BuyPrice)
        {
            player.Wallet.LoseMoney(item.BuyPrice);
            player.Inventory.AddItem(item);
        }
    }

    public void SellItem(Item item, PlayerController player)
    {
        if (player.Inventory.ContainsItem(item))
        {
            player.Inventory.RemoveItem(item);
            player.Wallet.GetMoney(item.SellPrice);
        }
    }
}
