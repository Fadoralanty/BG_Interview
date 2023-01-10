 using System;
 using System.Collections;
using System.Collections.Generic;
 using TMPro;
 using UnityEngine;
 using UnityEngine.UI;

 public class UIShop : MonoBehaviour
{ 
    public Action OnShopOpen;
    public Action OnShopClose;
    public Action OnNotEnoughMoney;
    public List<Item> Items;
    [SerializeField] private GameObject ShopItemTemplatePrefab;
    [SerializeField] private TextMeshProUGUI ShopName;
    [SerializeField] private GameObject Container;
    [SerializeField] private Transform panel;
    [SerializeField] private PlayerController player;
    private bool _isShopUIOpen;
    private List<GameObject> _UIitems;
    private void Awake()
    {
        _isShopUIOpen = false;
        _UIitems = new List<GameObject>();
    }

    private void Start()
    {
        //FillUIItems();
        CloseShopUI();
    }

    private void CreateItemButton(Item item)
    {
        GameObject newItem = Instantiate(ShopItemTemplatePrefab, panel);
        _UIitems.Add(newItem);
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

    public void ClearUIItems()
    {
        foreach (var gameObject in _UIitems)
        {
            GameObject obj = gameObject;
            //_UIitems.Remove(gameObject);
            Destroy(obj);
        }
    }

    public void FillUIItems()
    {
        foreach (var item in Items)
        {
            CreateItemButton(item);
        }
    }
    public void OpenShopUI()
    {
        _isShopUIOpen = true;
        Container.SetActive(_isShopUIOpen);
        FillUIItems();
        OnShopOpen?.Invoke();
    }
    public void CloseShopUI()
    {
        _isShopUIOpen = false;
        ClearUIItems();
        Container.SetActive(_isShopUIOpen);
        OnShopClose?.Invoke();
    }
}
