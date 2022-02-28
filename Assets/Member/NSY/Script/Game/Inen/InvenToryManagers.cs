using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DM.Inven;

namespace NSY.Iven
{
    public class InvenToryManagers : MonoBehaviour
    {
        [SerializeField] InventoryNSY iventorynsy;
        [SerializeField] EquipPanel equipPanel;

        private void Awake()
        {
            iventorynsy.OnItemRightClickEvent += EquipFromInventory;
        }
                                                                                        
        private void EquipFromInventory(Item item)
        {
            if (item is Item)
            {
                Equip((Item)item);
            }
        }

        //아이템 장착 제거
        public void Equip(Item item)
        {
           
            if (iventorynsy.RemoveItem(item))
            {
                Item previousitem;
                if (equipPanel.AddItem(item, out previousitem))
                {
                    if (previousitem != null)
                    {
                        iventorynsy.AddItem(previousitem);
                    }
                }
                else
                {
                    iventorynsy.AddItem(item);
                }
            }
        }

        public void Unequip(Item item)
        {
            if (!iventorynsy.isFull() && equipPanel.RemoveItem(item))
            {
                iventorynsy.AddItem(item);
            }
        }


    }

}

