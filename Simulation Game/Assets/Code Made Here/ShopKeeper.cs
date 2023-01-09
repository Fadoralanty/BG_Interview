using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopKeeper : MonoBehaviour
{
    [SerializeField] private Sprite characterPortrait;
    [SerializeField] private float interactRange = 2;
    [SerializeField] private PlayerController player;
    [SerializeField] private InventoryUI _inventoryUI;
    [SerializeField] private UIShop _shop;
    [SerializeField] private Dialogue Intro;
    [SerializeField] private Dialogue Warning;
    [SerializeField] private List<Item> items;
    private float _distanceToPlayer;
    private bool isShopOpen;
    private void Awake()
    {
        isShopOpen = false;
    }

    private void Start()
    {
        player.OnInteractPressed += OnInteractListener;
        _shop.OnNotEnoughMoney += OnNotEnoughMoneyListener;
    }

    private void OnNotEnoughMoneyListener()
    {
        Dialogue_Manager.instance.StartDialogue(Warning, characterPortrait);
    }

    private void Update()
    {
        Vector2 diff = player.transform.position - transform.position;
        _distanceToPlayer= diff.magnitude;
    }

    private void OnInteractListener()
    {
        if (_distanceToPlayer <= interactRange && !isShopOpen)
        {
            _shop.Items = items;
            _shop.OpenShopUI();
            player.OnOpenInventory?.Invoke();
            isShopOpen = true;
            Dialogue_Manager.instance.StartDialogue(Intro, characterPortrait);
        }
        else if(isShopOpen)
        {
            _shop.CloseShopUI();
            if(_inventoryUI.isInventoryOpen) player.OnOpenInventory?.Invoke();
            isShopOpen = false;
            Dialogue_Manager.instance.EndDialogue();
        }
    }
    private void OnDestroy()
    {
        player.OnInteractPressed -= OnInteractListener;
        _shop.OnNotEnoughMoney -= OnNotEnoughMoneyListener;

    }
    private void OnDrawGizmos()
    {
        Gizmos.color=Color.red;
        Gizmos.DrawWireSphere(transform.position,interactRange);
    }

}
