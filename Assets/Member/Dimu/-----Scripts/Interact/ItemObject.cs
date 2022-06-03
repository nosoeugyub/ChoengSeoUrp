using NSY.Iven;
using UnityEngine;

public class ItemObject : Interactable//, IInteractable
{
    public Item item;
    protected InventoryNSY inventoryNSY;

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
        base.OnEnable();
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
    public void Interact()
    {
        //상호작용 인덱스 체크
        //print(string.Format("AddData : {0}", item.ItemName));
        PlayerData.AddValue((int)item.InItemType, (int)ItemBehaviorEnum.InteractItem, PlayerData.ItemData, (int)ItemBehaviorEnum.length);
    }

    public override int CanInteract()
    {
        return (int)CursorType.Normal;
    }
}
