using NSY.Iven;
using UnityEngine;

public class ItemObject : MonoBehaviour//, IInteractable
{
    [SerializeField]protected Item item;
    private void Awake()
    {
    }
    private void OnEnable()
    {
        transform.GetChild(0).GetComponent<MeshRenderer>().material = item.ItemMaterial;
    }
    //public void AddItem()//아이템 오브젝트에서 삭제할 메서드. 상호작용 시 바로 인벤 불러서 아템추가
    //{
    //    FindObjectOfType<InventoryManager>().AddItem(item, 1);
    //    Debug.Log((int)item.InItemType + ", " + PlayerData.ItemData[(int)item.InItemType].amounts[0]);
    //}
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
        print(string.Format( "AddData : {0}", item.ItemName));
        PlayerData.AddValue((int)item.InItemType, (int)ItemBehaviorEnum.InteractItem, PlayerData.ItemData, (int)ItemBehaviorEnum.length);
    }
}
