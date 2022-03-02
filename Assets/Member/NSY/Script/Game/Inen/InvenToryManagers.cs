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


        private ItemSlot dragitemSlot;

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
                dragitemSlot = itemslot;
                draggableitem.sprite = itemslot.item.ItemSprite;
                draggableitem.transform.position = Input.mousePosition;
                draggableitem.gameObject.SetActive(true);
            }
        }
        private void Drag(ItemSlot itemslot)
        {
            if (draggableitem.enabled)
            {
                draggableitem.transform.position = Input.mousePosition;
            }

        }
        private void EndDrag(ItemSlot itemslot)
        {
          
            dragitemSlot = null;
            draggableitem.gameObject.SetActive(false);
            this.enabled = false;
        }
       
        private void Drop(ItemSlot dropitemslot)
        {
            //  if (dragitemSlot == null) return;

           
           // if (dropitemslot.CanReceiveItem(dragitemSlot.item) && dragitemSlot.CanReceiveItem(dropitemslot.item))
           // {
                Debug.Log("드롭이벤트 발생");
                Swapitems(dropitemslot);
           // }
           
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

        private void Swapitems(ItemSlot dropitemslot)
        {
            Item dragItem = dragitemSlot.item as Item;
            Item dropitem = dropitemslot.item as Item;

            Item draggeditem = dragitemSlot.item;
            dragitemSlot.item = dropitemslot.item;
            dropitemslot.item = draggeditem;
              // if(dropitemslot is EquipmentSlot)
              // {
              //  
              //}
            //  if (draggedSlot is EquipmentSlot)
            //  {

            //}


            //draggedSlot.item = dropitemslot.item;

            // dropitemslot.item = dragItem;
        }

    }

}

