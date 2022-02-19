using DM.Inven;
using UnityEngine;

public class ItemObject : MonoBehaviour
{
    public Item item;
    [TextArea]
    public string explain;
    //public void OnTriggerEnter(Collider other)
    //{
    //    if (other.CompareTag("Player"))
    //    {
    //        AddItem();
    //    }
    //}

    public void AddItem()//아이템 오브젝트에서 삭제할 메서드. 상호작용 시 바로 인벤 불러서 아템추가
    {
        FindObjectOfType<InventoryManager>().AddItem(item, 1);
        Debug.Log((int)item.ItemType + ", " + PlayerData.ItemData[(int)item.ItemType].amounts[0]);
    }
}
