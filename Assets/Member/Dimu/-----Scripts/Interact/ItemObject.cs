using NSY.Iven;
using UnityEngine;

public class ItemObject : MonoBehaviour//, IInteractable
{
    [SerializeField]protected Item item;
    protected InventoryNSY inventoryNSY;
    [SerializeField] protected MeshRenderer quad;

    public void Awake()
    {
        inventoryNSY=FindObjectOfType<InventoryNSY>();
        if(!quad)
        quad = transform.Find("Quad").GetComponent<MeshRenderer>();
    }
    private void OnEnable()
    {
        if(item.ItemMaterial)
            quad.material = item.ItemMaterial;
    }
    public Transform ReturnTF()
    {
        return transform;
    }
    public OutItemType GetOutItemType()
    {
        return item.OutItemType;
    }
    public string CanInteract()
    {
        return "상호작용하기";
    }
    public void Interact()
    {
        //상호작용 인덱스 체크
        print(string.Format( "AddData : {0}", item.ItemName));
        PlayerData.AddValue((int)item.InItemType, (int)ItemBehaviorEnum.InteractItem, PlayerData.ItemData, (int)ItemBehaviorEnum.length);
    }
}
