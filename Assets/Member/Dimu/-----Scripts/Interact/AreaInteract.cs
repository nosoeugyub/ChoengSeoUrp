using UnityEngine;
public enum AreaType { Sprimmer, Sumall, Fallter, WinRing, Port, Q_1 }
public class AreaInteract : MonoBehaviour
{
    [SerializeField] AreaType areaType;
    NPCManager npcManager;
    private void Awake()
    {
        npcManager = FindObjectOfType<NPCManager>();
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            PlayerData.AddValue((int)areaType, (int)LocationBehaviorEnum.Interact, PlayerData.locationData, (int)LocationBehaviorEnum.length);
            npcManager.ButtonInteractable((int)areaType, true);
            //Debug.Log(areaType.ToString());
        }
    }


}
