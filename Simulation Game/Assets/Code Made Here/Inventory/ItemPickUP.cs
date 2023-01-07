using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemPickUP : MonoBehaviour
{
    [SerializeField] private Item _item;

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag("Player"))
        {
            col.GetComponent<PlayerController>().Inventory.AddItem(_item);
            Destroy(gameObject);
        }
    }
}
