using NSY.Iven;
using UnityEngine;


public class MailBox : ItemObject, IEventable
{
    [SerializeField] Post MailPost; //우편함에 추가될 우편 메일 스크립터블오브젝트
    [SerializeField] Item mailBoxMessage;
    public new int CanInteract()
    {
        return (int)CursorType.Normal;
    }

    public void EndInteract()
    {
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
        //NSY 추가코드 
       // FindObjectOfType<PostPanel>().AddPost(MailPost);
    }

    //메서드 자유 추가
}
