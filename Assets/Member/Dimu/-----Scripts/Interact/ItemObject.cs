using NSY.Iven;
using UnityEngine;

public class ItemObject : MonoBehaviour//, IInteractable
{
    [SerializeField]protected Item item;
    protected InventoryNSY inventoryNSY;
    [SerializeField] protected MeshRenderer quad;

    public void Awake()
    {
    }
    private void OnEnable()
    {
        inventoryNSY=FindObjectOfType<InventoryNSY>();
        quad = transform.GetChild(0).GetComponent<MeshRenderer>();
        if(item.ItemMaterial)
            quad.material = item.ItemMaterial;
    }
    public Transform ReturnTF()
    {
        return transform;
    }

    public string CanInteract()
    {
        return "상호작용하기";
    }
    public void Interact()
    {
        //상호작용 인덱스 체크
        //print(string.Format( "AddData : {0}", item.ItemName));
        PlayerData.AddValue((int)item.InItemType, (int)ItemBehaviorEnum.InteractItem, PlayerData.ItemData, (int)ItemBehaviorEnum.length);
    }
}
