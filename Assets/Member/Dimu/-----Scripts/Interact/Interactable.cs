using UnityEngine;
using UnityEngine.UI;

public interface IInteractable
{
    //void Interact(); //상호작용 조작 시 호출될 메서드
    string CanInteract(); //상호작용 가능할 때 호출될 메서드
    void EndInteract();//상호작용 끝날 때 호출될 메서드. 삭제할까 생각중
    Transform ReturnTF(); //거리 계산을 위한 Transform
}
public interface ITalkable : IInteractable
{
    void Talk(Item handitem);
}
public interface IMineable :IInteractable
{
    bool Mine(Item handitem, Animator animator);
}
public interface IDropable : IInteractable
{
    void DropItems();
}
public interface IEatable : IInteractable
{
    void Eat();
}
public interface ICollectable : IInteractable
{
    void Collect(Animator animator);
}
public interface IEventable : IInteractable
{
    void EtcEvent(Item handItem);
}
public interface IBuildable : IInteractable
{
    void Demolish();
}
public interface ISpeechBubbleCollectable : IInteractable
{
    void InstantiateBubble();
    bool CheckBubble(Item handitem, Animator animator);
}

