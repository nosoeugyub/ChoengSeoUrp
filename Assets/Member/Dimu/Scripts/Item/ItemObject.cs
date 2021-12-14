﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DM.Inven;

public class ItemObject : MonoBehaviour
{
    public Item item;
    [TextArea]
    public string explain;
    public void OnTriggerEnter(Collider other)
    {
        FindObjectOfType<InventoryManager>().AddItem(item, 1);
        PlayerData.ItemData[item.itemId].amounts[0]++;
        Debug.Log(PlayerData.ItemData[item.itemId].amounts[0]);
    }
}
