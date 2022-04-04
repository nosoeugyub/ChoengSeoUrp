public class TrashCan : ItemObject, IEventable
{
    bool isInvenOn = false;
    public new string CanInteract()
    {
        return "쓰레기통 인벤열기";
    }
    public void EtcEvent(Item _handItem) //상호작용 시 실행
    {
        TrashInventoryOn();
        Interact();
    }
    public void TrashInventoryOn()
    {
        if (isInvenOn)
        {
            print("인벤닫기");
            isInvenOn = false;
        }
        else
        {
            print("인벤열기");
            isInvenOn = true;
        }
    }

    //메서드 자유 추가
}
