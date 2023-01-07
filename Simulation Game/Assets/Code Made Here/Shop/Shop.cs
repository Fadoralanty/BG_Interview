using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shop : MonoBehaviour
{
    public List<Item> Items;

    public void BuyItem(Item item, PlayerController player)
    {
        if (!Items.Contains(item)) return;

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
