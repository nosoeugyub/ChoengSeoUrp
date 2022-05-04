using NSY.Iven;
using UnityEngine;

public class ItemObject : MonoBehaviour//, IInteractable
{
    [SerializeField] protected Item item;
    protected InventoryNSY inventoryNSY;
    [SerializeField] protected MeshRenderer quad;

    public void Awake()
    {
        inventoryNSY = FindObjectOfType<InventoryNSY>();
        if (!quad)
        {
            if (transform.Find("Quad"))
                quad = transform.Find("Quad").GetComponent<MeshRenderer>();
            else if (transform.GetChild(0).Find("Quad"))
                quad = transform.GetChild(0).Find("Quad").GetComponent<MeshRenderer>();
        }
    }
    private void OnEnable()
    {
        if (item.ItemMaterial)
            quad.material = item.ItemMaterial;
    }
    public Transform ReturnTF()
    {
        return transform;
    }
    public Item GetItem()
    {
        return item;
    }
    public void SetItem(Item item_)
    {
        item = item_;
    }
    public InItemType GetInItemType()
    {
        return item.InItemType;
    }
    public string CanInteract()
    {
        return "상호작용하기";
    }
    public void Interact()
    {
        //상호작용 인덱스 체크
        //print(string.Format("AddData : {0}", item.ItemName));
        PlayerData.AddValue((int)item.InItemType, (int)ItemBehaviorEnum.InteractItem, PlayerData.ItemData, (int)ItemBehaviorEnum.length);
    }
}
