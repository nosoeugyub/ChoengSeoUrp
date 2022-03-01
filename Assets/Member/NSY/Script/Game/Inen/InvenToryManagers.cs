using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DM.Inven;
using NSY.Player;

namespace NSY.Iven
{
    public class InvenToryManagers : MonoBehaviour
    {
        //케릭터 속성
        PlayerController playercontroller;

        [SerializeField] InventoryNSY iventorynsy;
        [SerializeField] EquipPanel equipPanel;
        [SerializeField] Image draggableitem;


        private ItemSlot draggedSlot;
        private void Awake()
        {
            //인벤토리 클레스 이벤트
            iventorynsy.OnRightClickEvent += Equip;
            equipPanel.OnRightClickEvent += Unequip;
            //드래그 시작
            iventorynsy.OnBeginDragEvent += BeginDrag;
            equipPanel.OnBeginDragEvent += BeginDrag;
            //드래그 끝
            iventorynsy.OnEndDragEvent += EndDrag;
            equipPanel.OnEndDragEvent += EndDrag;
            //드래그
            iventorynsy.OnDragEvent += Drag;
            equipPanel.OnDragEvent += Drag;
            //드롭
            iventorynsy.OnDropEvent += Drop;
            equipPanel.OnDropEvent += Drop;
        }
                 
        private void Equip(ItemSlot itemslot)
        {
            Item item = itemslot.item as Item;
            if (item != null)
            {
                Equip(item);
            }
        }
        private void Unequip(ItemSlot itemslot)
        {
            Item item = itemslot.item as Item;
            if (item != null)
            {
                Unequip(item);
            }
        }
        private void BeginDrag(ItemSlot itemslot)
        {
            if (itemslot.item != null)
            {
                draggedSlot = itemslot;
                draggableitem.sprite = itemslot.item.ItemSprite;
                draggableitem.transform.position = Input.mousePosition;
                draggableitem.enabled = true;
            }
        }
        private void EndDrag(ItemSlot itemslot)
        {
            draggedSlot = null;
            draggableitem.enabled = false;
        }
        private void Drag(ItemSlot itemslot)
        {
            if (draggableitem.enabled)
            {
                draggableitem.transform.position = Input.mousePosition;
            }
            
        }
        private void Drop(ItemSlot dropitemslot)
        {
            if (dropitemslot.CanReceiveItem(draggedSlot.item) && draggedSlot.CanReceiveItem(dropitemslot.item))
            {
                Item dragItem = draggedSlot.item as Item;
                Item dropItem = dropitemslot.item as Item;

              
                Item draggeditem = draggedSlot.item;
                draggedSlot.item = dropitemslot.item;
                dropitemslot.item = draggeditem;
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

