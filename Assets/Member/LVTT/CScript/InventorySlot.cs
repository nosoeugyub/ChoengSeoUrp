using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TT_INTERACT
{
    public class InventorySlot : MonoBehaviour
    {
        private Inventory inventory;
        public int i;
        private void Start()
        {
            inventory = FindObjectOfType<Inventory>();
        }

        private void Update()
        {
            if (transform.childCount <= 0)
            {
                inventory.InventorySlotList[i].isFull = false;
            }
        }
        //public void DropItem()
        //{
        //    foreach (Transform child in transform)
        //    {
        //        inventory.InventorySlotList[i].curItemNum = 0;
        //        inventory.InventorySlotList[i].SlotStackTxt.text = inventory.InventorySlotList[i].curItemNum.ToString();
        //        GameObject.Destroy(child.gameObject);
        //    }
        //}
    }
}

