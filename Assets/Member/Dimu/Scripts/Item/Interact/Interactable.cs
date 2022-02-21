using UnityEngine;

public interface IInteractable
{
    void Interact(); //상호작용 조작 시 호출될 메서드
    void CanInteract(GameObject player); //상호작용 가능할 때 호출될 메서드
    void EndInteract();//상호작용 끝날 때 호출될 메서드. 삭제할까 생각중
    Transform ReturnTF(); //거리 계산을 위한 Transform
}