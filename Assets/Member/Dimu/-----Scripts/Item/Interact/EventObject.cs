using DM.Inven;
using NSY.Iven;
using NSY.Manager;
using UnityEngine;

public class EventObject : ItemObject, IEventable
{
    Item handItem;
    [SerializeField] Item mailBoxMessage;
    public string CanInteract()
    {
        if (item.InItemType == InItemType.Trashcan)
            return "쓰레기 버리기";
        if (item.InItemType == InItemType.Mailbox)
            return "편지 받기";
        return "뭐지?";
    }

    public void EtcEvent(Item _handItem)
    {
        if (_handItem)
        {
            handItem = _handItem;
            if (item.InItemType == InItemType.Trashcan && handItem.InItemType == InItemType.Trash)
                TrashCut();

            Interact();

        }
        else
        {
            if (item.InItemType == InItemType.Mailbox)
                GetMessage();

            Interact();

        }
    }

    public void TrashCut()
    {
        //아이템 개수가 0이 되면 손을 비우는 처리 필요
        //FindObjectOfType<InventoryNSY>().DeleteItem(handItem, 1);
    }
    public void GetMessage()
    {
        SuperManager.Instance.inventoryManager.AddItem(mailBoxMessage);

    }
}