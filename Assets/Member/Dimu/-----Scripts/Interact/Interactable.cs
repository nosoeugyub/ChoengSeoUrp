﻿using UnityEngine;

public interface IInteractable
{
    //void Interact(); //상호작용 조작 시 호출될 메서드
    int CanInteract(); //상호작용 가능할 때 호출될 메서드
    void EndInteract();//상호작용 끝날 때 호출될 메서드. 삭제할까 생각중
    Transform ReturnTF(); //거리 계산을 위한 Transform
}

public abstract class Interactable : MonoBehaviour
{
    [SerializeField]  Shader defaultshader;
    [SerializeField] protected MeshRenderer quad;

    protected void OnEnable()
    {
        if (quad)
            defaultshader = quad.material.shader;
    }
    public abstract int CanInteract();
    public virtual void EndInteract()
    {
        if (quad)
            quad.material.shader = defaultshader;
    }
}




































//public interface ITalkable : IInteractable
//{
//    void Talk(Item handitem);
//}
//public interface IMineable :IInteractable
//{
//    bool Mine(Item handitem, Animator animator);
//}
//public interface ICollectable : IInteractable
//{
//    void Collect(Animator animator);
//}
//public interface IBuildable : IInteractable
//{
//    void Demolish();
//}
//public interface ISpeechBubbleCollectable : IInteractable
//{
//    void InstantiateBubble();
//    bool CheckBubble(Item handitem, Animator animator);
//}
//public interface IEatable : IInteractable
//{
//    void Eat();
//}
//public interface IEventable : IInteractable
//{
//    void EtcEvent(Item handItem);
//}
//public interface IDropable : IInteractable
//{
//    void DropItems();
//}