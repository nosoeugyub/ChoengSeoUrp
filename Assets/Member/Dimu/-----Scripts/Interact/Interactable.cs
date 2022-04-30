using UnityEngine;
using UnityEngine.UI;

public interface IInteractble
{
    //void Interact(); //상호작용 조작 시 호출될 메서드
    string CanInteract(); //상호작용 가능할 때 호출될 메서드
    void EndInteract();//상호작용 끝날 때 호출될 메서드. 삭제할까 생각중
    Transform ReturnTF(); //거리 계산을 위한 Transform
}
public interface ITalkable : IInteractble
{
    void Talk(Item handitem);
}
public interface IMineable :IInteractble
{
    bool Mine(Item handitem, Animator animator);
}
public interface IDropable : IInteractble
{
    void DropItems();
}
public interface IEatable : IInteractble
{
    void Eat();
}
public interface ICollectable : IInteractble
{
    void Collect();
}
public interface IEventable : IInteractble
{
    void EtcEvent(Item handItem);
}
public interface IBuildable : IInteractble
{
    void Demolish();
}
public interface ISpeechBubbleCollectable : IInteractble
{
    void InstantiateBubble();
    bool CheckBubble(Item handitem, Animator animator);
}

