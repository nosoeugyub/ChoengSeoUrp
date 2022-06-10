using UnityEngine;
public enum AreaType { Sprimmer, Sumall, Fallter, WinRing, Port, Q_1 }
public class AreaInteract : Interactable
{
    [SerializeField] protected AreaType areaType;
    protected NPCManager npcManager;

    public override int CanInteract()
    {
        return (int)CursorType.Normal;
    }

    private void Awake()
    {
        npcManager = FindObjectOfType<NPCManager>();
    }
    public virtual void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            PlayerData.AddValue((int)areaType, (int)LocationBehaviorEnum.Interact, PlayerData.locationData, (int)LocationBehaviorEnum.length);
        }
    }


}
