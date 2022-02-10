﻿using DM.Inven;
using UnityEngine;

public class ItemObject : MonoBehaviour
{
    public Item item;
    [TextArea]
    public string explain;
    public void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            FindObjectOfType<InventoryManager>().AddItem(item, 1);
            PlayerData.AddValue((int)item.ItemType,0, PlayerData.ItemData);
            Debug.Log((int)item.ItemType + ", "+PlayerData.ItemData[(int)item.ItemType].amounts[0]);
        }
    }
}
