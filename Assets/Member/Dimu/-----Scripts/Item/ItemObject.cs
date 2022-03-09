using DM.Inven;
using UnityEngine;

public class ItemObject : MonoBehaviour
{
    public Item item;
    private void OnEnable()
    {
        //transform.GetChild(0).GetComponent<SpriteRenderer>().sprite = item.ItemSprite;
        transform.GetChild(0).GetComponent<MeshRenderer>().material = item.ItemMaterial;

    }

    public void AddItem()//아이템 오브젝트에서 삭제할 메서드. 상호작용 시 바로 인벤 불러서 아템추가
    {
        FindObjectOfType<InventoryManager>().AddItem(item, 1);
        Debug.Log((int)item.InItemType + ", " + PlayerData.ItemData[(int)item.InItemType].amounts[0]);
    }

    //public void Interact()
    //{
    //    AddItem();
    //    Destroy(this.gameObject);
    //}
    //
    //public void CanInteract()
    //{
    //    NSY.Player.PlayerInput.OnPressFDown = Interact;
    //}
    public Transform ReturnTF()
    {
        return transform;
    }
}
