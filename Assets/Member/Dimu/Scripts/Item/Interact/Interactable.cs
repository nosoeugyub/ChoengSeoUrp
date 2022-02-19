using UnityEngine;

public interface IInteractable
{
    void Interact();
    void CanInteract(GameObject player);
    void EndInteract();
    Transform ReturnTF();
}