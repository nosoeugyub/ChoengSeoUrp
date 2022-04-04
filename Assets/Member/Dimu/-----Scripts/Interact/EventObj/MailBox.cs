using NSY.Iven;
using UnityEngine;

public class MailBox : ItemObject, IEventable
{
    [SerializeField] Item mailBoxMessage;
    public new string CanInteract()
    {
        return "편지 받기";
    }
    public void EtcEvent(Item handItem)
    {
        GetMessage();
        Interact();
    }
    public void GetMessage()
    {
        //편지함 열리기 라던가 어떠한 상호작용 필요
        FindObjectOfType<InventoryNSY>().AddItem(mailBoxMessage);
    }

    //메서드 자유 추가
}
