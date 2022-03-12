using DM.Inven;
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
        handItem = _handItem;
        if (item.InItemType == InItemType.Trashcan && handItem.InItemType == InItemType.Trash)
            TrashCut();
        else if (item.InItemType == InItemType.Mailbox && handItem.InItemType == InItemType.Mailbox)
            GetMessage();
    }

    public void TrashCut()
    {
        //아이템 개수가 0이 되면 손을 비우는 처리 필요
        FindObjectOfType<InventoryManager>().DeleteItem(handItem, 1);
    }
    public void GetMessage()
    {
        FindObjectOfType<InventoryManager>().AddItem(mailBoxMessage, 1);

    }
}