using UnityEngine;

public class TeleportObject : Interactable
{
    [SerializeField] AreaType areaType;
    public override int CanInteract()
    {
        return (int)CursorType.Mag;
    }

    public void Teleport()
    {
        FindObjectOfType<NPCManager>().OpentelePickUI();
    }
}
