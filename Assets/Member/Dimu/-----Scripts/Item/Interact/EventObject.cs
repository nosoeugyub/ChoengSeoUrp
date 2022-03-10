using DM.Inven;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventObject : ItemObject, IEventable
{
    //[SerializeField] EventItemType eventItemType;
    Item handItem;
    public string CanInteract()
    {
        if (item.InItemType == InItemType.Trashcan)
            return "쓰레기 버리기";
        else
            return "뭐지?";
    }

    public void EtcEvent(Item _handItem)
    {
        handItem = _handItem;
        if (item.InItemType == InItemType.Trashcan && handItem.InItemType == InItemType.Trash)
            TrashCut();
    }

    public void TrashCut()
    {
        FindObjectOfType<InventoryManager>().DeleteItem(handItem, 1);

    }
}

//public enum EventItemType
//{ Trashcan }