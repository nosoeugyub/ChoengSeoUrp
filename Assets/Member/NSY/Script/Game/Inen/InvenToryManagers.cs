﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DM.Inven;
using NSY.Player;
using System;


namespace NSY.Iven
{
    public class InvenToryManagers : MonoBehaviour
    {
        

        //케릭터 속성
        public int Vital = 100;

        [SerializeField] InventoryNSY iventorynsy;
        [SerializeField] EquipPanel equipPanel;
        [SerializeField] CraftManager craftPanel;
        [SerializeField] Image draggableitem;
        [SerializeField] DropItemArea Dropitemarea;
        [SerializeField] QuestionDialog questionDialog;
        //조합 필요한 컴포넌트들
        CraftSlot craftslot;
        Item currntitem;
      





        private BaseItemSlot dragitemSlot;

       

        private void Awake()
        {
            //인벤토리 클레스 이벤트

            iventorynsy.OnRightClickEvent += InventoryRightClick;
           
            equipPanel.OnRightClickEvent += EquipmentPanelRightClick;
            
            //드래그 시작
            iventorynsy.OnBeginDragEvent += BeginDrag;
            equipPanel.OnBeginDragEvent += BeginDrag;
            craftPanel.OnBeginDragEvent += BeginDrag;
            //드래그 끝
            iventorynsy.OnEndDragEvent += EndDrag;
            equipPanel.OnEndDragEvent += EndDrag;
            craftPanel.OnEndDragEvent += EndDrag;
            //드래그
            iventorynsy.OnDragEvent += Drag;
            equipPanel.OnDragEvent += Drag;
            craftPanel.OnDragEvent += Drag;
            //드롭
            iventorynsy.OnDropEvent += Drop;
            equipPanel.OnDropEvent += Drop;
            craftPanel.OnDropEvent += Drop;
            Dropitemarea.OnDropEvent += DropItemOutsideUI;
        }
       

        private void InventoryRightClick(BaseItemSlot itemslot)
        {
            Debug.Log("이거됨");
            if (itemslot.item is EquippableItem)
            {
                Equip((EquippableItem)itemslot.item);
             
            }
            else if(itemslot.item is UseableItem)
            {
                UseableItem usableitem = (UseableItem)itemslot.item;
                usableitem.Use(this);

                if (usableitem.isConsumable)
                {
                    iventorynsy.RemoveItem(usableitem);
                    usableitem.Destroy();
                }
            }
        }
        private void EquipmentPanelRightClick(BaseItemSlot itemslot)
        {
            EquippableItem equippableItem = itemslot.item as EquippableItem;
            if (itemslot.item is EquippableItem)
            {
                Unequip((EquippableItem)itemslot.item);
            }
        }
        private void BeginDrag(BaseItemSlot itemslot)
        {
            if (itemslot.item != null)
            {
                dragitemSlot = itemslot;
                draggableitem.sprite = itemslot.item.ItemSprite;
                draggableitem.transform.position = Input.mousePosition;
                draggableitem.gameObject.SetActive(true);
            }
        }
        private void Drag(BaseItemSlot itemslot)
        {
          
                draggableitem.transform.position = Input.mousePosition;
           

        }
        private void EndDrag(BaseItemSlot itemslot)
        {
          
            dragitemSlot = null;
            draggableitem.gameObject.SetActive(false);
         //   this.enabled = false;
        }
       
        private void Drop(BaseItemSlot dropitemslot)
        {
          

            if (currntitem != null)
            {
                craftPanel.itemList[craftslot.index] = currntitem;

            }

            if (dragitemSlot == null) return;

            if (dropitemslot.CanAddStack(dragitemSlot.item))
            {
                AddStacks(dropitemslot);
            }
            else if (dropitemslot.CanReceiveItem(dragitemSlot.item) && dragitemSlot.CanReceiveItem(dropitemslot.item))
            {
                Swapitems(dropitemslot);
            }



        }
        //버리기
        private void DropItemOutsideUI()
        {
            if (dragitemSlot == null)
            {
                return;
            }
           
            BaseItemSlot baseitemslot = dragitemSlot;
            questionDialog.Show();
            questionDialog.OnYesEvent += () => DestroyItem(baseitemslot);


        }
        private void DestroyItem(BaseItemSlot baseitemslot)//버릴떄 쓰는 로직
        {
            baseitemslot.item.Destroy();
            baseitemslot.item = null;
        }

        //조합
       void CheckForCreatedRecipes()
        {
          //  craftPanel.ResultSlot.gameObject.SetActive(false);
          //  craftPanel.ResultSlot = null;

            string currentRecipeString = "";
            foreach (Item item  in craftPanel.itemList)
            {
                if (item != null)
                {
                    currentRecipeString += item.ItemName;
                }
                else
                {
                    currentRecipeString += "null";
                }
            }

            for (int i = 0; i < craftPanel.recipes.Length; i++)
            {
                if (craftPanel.recipes[i] == currentRecipeString)
                {
                    
                    craftPanel.ResultSlot.item = craftPanel.recipeResults[i];
                }
            }
        }

        public void OnClickSlot(CraftSlot craftslot)
        {
            craftslot.item = null;
            craftPanel.itemList[craftslot.index] = null;
            craftslot.gameObject.SetActive(false);
            CheckForCreatedRecipes();
           

        }
        //아이템 장착 해제
        public void Equip(EquippableItem item)
        {
           
            if (iventorynsy.RemoveItem(item))
            {
                EquippableItem previousitem;
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
        public void Unequip(EquippableItem item)
        {
            if (!iventorynsy.CanAddItem(item) && equipPanel.RemoveItem(item))
            {
                iventorynsy.AddItem(item);
            }
        }
        //조합 아이템을 추가또는 제거
        public void Craftinsert(Item item)
        {
            if (iventorynsy.RemoveItem(item))
            {
                Item CraftpreviousItem;
                if (craftPanel.CraftAddItem(item, out CraftpreviousItem))
                {
                    iventorynsy.AddItem(CraftpreviousItem);
                }
                else
                {
                    iventorynsy.AddItem(item);
                }
            }
        }






      
        private void AddStacks(BaseItemSlot dropitemslot)
        {
            //갯수 중복시 옮기는거
            int numAddableStacks = dropitemslot.item.MaximumStacks - dropitemslot.Amount;
            int stacksToAdd = Mathf.Min(numAddableStacks, dragitemSlot.Amount);

            dropitemslot.Amount += stacksToAdd;
            dropitemslot.Amount -= stacksToAdd;
        }
        private void Swapitems(BaseItemSlot dropitemslot)
        {
            EquippableItem dragItem = dragitemSlot.item as EquippableItem;
            EquippableItem dropitem = dropitemslot.item as EquippableItem;

           

            Item draggedItem = dragitemSlot.item;
            int draggedItemAmount = dragitemSlot.Amount;

            dragitemSlot.item = dropitemslot.item;
            dragitemSlot.Amount = dropitemslot.Amount;

            dropitemslot.item = draggedItem;
            dropitemslot.Amount = draggedItemAmount;


        }


        //
        private ItemContainer openItemContainer;

        private void TransferToItemContainer(BaseItemSlot itemSlot)
        {
            Item item = itemSlot.item;
            if (item != null && openItemContainer.CanAddItem(item))
            {
                iventorynsy.RemoveItem(item);
                openItemContainer.AddItem(item);
            }
        }

        private void TransferToInventory(BaseItemSlot itemSlot)
        {
            Item item = itemSlot.item;
            if (item != null && iventorynsy.CanAddItem(item))
            {
                openItemContainer.RemoveItem(item);
                iventorynsy.AddItem(item);
            }
        }

        public void OpenItemContainer(ItemContainer itemContainer)
        {
            openItemContainer = itemContainer;

            iventorynsy.OnRightClickEvent -= InventoryRightClick;
            iventorynsy.OnRightClickEvent += TransferToItemContainer;

            itemContainer.OnRightClickEvent += TransferToInventory;

         //   itemContainer.OnPointerEnterEvent += ShowTooltip;
         //   itemContainer.OnPointerExitEvent += HideTooltip;
            itemContainer.OnBeginDragEvent += BeginDrag;
            itemContainer.OnEndDragEvent += EndDrag;
            itemContainer.OnDragEvent += Drag;
            itemContainer.OnDropEvent += Drop;
        }

        public void CloseItemContainer(ItemContainer itemContainer)
        {
            openItemContainer = null;

            iventorynsy.OnRightClickEvent += InventoryRightClick;
            iventorynsy.OnRightClickEvent -= TransferToItemContainer;

            itemContainer.OnRightClickEvent -= TransferToInventory;

        //    itemContainer.OnPointerEnterEvent -= ShowTooltip;
        //    itemContainer.OnPointerExitEvent -= HideTooltip;
            itemContainer.OnBeginDragEvent -= BeginDrag;
            itemContainer.OnEndDragEvent -= EndDrag;
            itemContainer.OnDragEvent -= Drag;
            itemContainer.OnDropEvent -= Drop;
        }



    }

}

