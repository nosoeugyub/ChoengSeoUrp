using DM.Inven;
using UnityEngine;

public class ItemObject : MonoBehaviour
{
    public Item item;
    [TextArea]
    public string explain;
    public void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            FindObjectOfType<InventoryManager>().AddItem(item, 1);
            PlayerData.AddValueInDictionary((int)item.ItemType,0, PlayerData.ItemData);
            //PlayerData.ItemData[(int)item.ItemType].amounts[0]++; //0 = 아이템얻기
            Debug.Log((int)item.ItemType + ", "+PlayerData.ItemData[(int)item.ItemType].amounts[0]);
        }
    }
}
