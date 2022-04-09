using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DM.Inven;
using NSY.Player;
using System;
using TT.BuildSystem;

namespace NSY.Iven
{
    public class InvenToryManagers : MonoBehaviour
    {
        

        //케릭터 속성
        public int Vital = 100;

        [SerializeField] PostEvent PostEvent;
        [SerializeField] BtnIven btniven;
        [SerializeField] InventoryNSY iventorynsy;
        [SerializeField] EquipPanel equipPanel;
        [SerializeField] CraftManager craftPanel;
        [SerializeField] Image draggableitem;
        [SerializeField] DropItemArea Dropitemarea;
        [SerializeField] QuestionDialog questionDialog;
        //조합 필요한 컴포넌트들
        CraftSlot craftslot;
     
      //레시피
      [Header("레시피 레퍼런스")]
        private CraftingRecipe carftingRecipe;
        [SerializeField] CraftSlot[] CraftSlot;
        //우편
        [SerializeField] PostPanel postpanel;

        //조합
        [SerializeField] CraftManager Craftmanager;
        [SerializeField] GameObject Craftmgr;
        public int Craftindex = 0;
        public bool isAdd = true;
        private BaseItemSlot dragitemSlot;

        [SerializeField] BuildingItemSpawn buildingItemSpawn;

        //더블클릭
        private float firstClickTime, timeBetweenClicks;
        private bool coroutineAllowed;
        private int clickCounter;


        //버리기
        public Text ScriptTxt;
        int discount = 1;
        private void Awake()
        {
            ScriptTxt.text = "1";





            btniven.OnPostEvent += ClickPostSlotUi;





            //인벤토리 클레스 이벤트
            iventorynsy.OnDubleClickEvent += OnDoubleClickEvent;
            iventorynsy.OnLeftClickEvent += BuildingLeftClick;
            iventorynsy.OnRightClickEvent += InventoryRightClick;
           // iventorynsy.OnLeftClickEvent += InventoryLeftClick;
            equipPanel.OnRightClickEvent += EquipmentPanelRightClick;
            craftPanel.OnLeftClickEvent += CraftPanelLeftClick;
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


            //carftingRecipe = new CraftingRecipe();
        }

        private void Update()
        {

            if (Craftmgr.activeSelf ==false)
            {
                //Debug.Log("미치녀");
                CloseCraftPanel();
            }
           

        }


        private void BuildingLeftClick(BaseItemSlot obj)
        {
            if (BuildingManager.isBuildMode)
            {
                buildingItemSpawn.BtnSpawnHouseBuildItem(obj.item);
            }
        }


        //더블클릭
        public void OnDoubleClickEvent(BaseItemSlot itemslot)
        {
            Debug.Log("더블클릭따땅");
        }



        //조합창 중지
        Item currntitem;
        public void CloseCraftPanel()
        {
          
            Craftmanager.RestSlot();
        }


        //우편 버튼 눌렀을때.
        private void ClickPostButton(PostSlot postslot)
        {
           Debug.Log("버튼클릭함");
        }

        private void InventoryRightClick(BaseItemSlot itemslot)
        {
          
            if (itemslot.item is Item && btniven.Craft == true)
            {
                if (isAdd)
                {
                    craftPanel.CraftAddItem(itemslot.item.GetCopy());
                    itemslot.Amount--;
                    UpdateRecipe();
                    Craftindex++;
                }
                if (Craftindex == 3)
                {
                    isAdd = false;
                }
              
              

            }

            if (itemslot.item is EquippableItem && btniven.isInven == true)
            {
                Equip((EquippableItem)itemslot.item);
             
            }
            else if (itemslot.item is UseableItem && btniven.isInven == true)
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
       
      
        private void CraftPanelLeftClick(BaseItemSlot itemslot)
        {
            if (itemslot.item is Item && btniven.Craft == true)
            {
                iventorynsy.AddItem(itemslot.item.GetCopy());
                itemslot.Amount--;
                UpdateRecipe();
                Craftindex--;

            }
            if (Craftindex < 3)
            {
                isAdd = true;
            }
        }
        void UpdateRecipe()
        {//탐색
            Item Recipe;

            Recipe = craftPanel.SetCraftingRecipe();
            


        }

      



        private void EquipmentPanelRightClick(BaseItemSlot itemslot)
        {
           
            if (itemslot.item is EquippableItem && btniven.isInven)
            {
                Unequip((EquippableItem)itemslot.item);
            }
        }
        
        private void BeginDrag(BaseItemSlot itemslot)
        {
            if (itemslot.item != null)
            {
                Debug.Log("드래그시작함");
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
        ;
        }
       
        private void Drop(BaseItemSlot dropitemslot)
        {

            Debug.Log("드래그시작함");


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
            discount = 0;
            if (dragitemSlot == null)
            {
                return;
            }
           
            BaseItemSlot baseitemslot = dragitemSlot;
            questionDialog.Show();
            questionDialog.OnYesEvent += () => DestroyItem(baseitemslot);


        }
        public void PlusBtn()
        {
            discount += 1;
            ScriptTxt.text = discount.ToString();
        }
        public void MiuseBtn()
        {
            discount -= 1;
            ScriptTxt.text = discount.ToString();
        }
        private void DestroyItem(BaseItemSlot baseitemslot)//버릴떄 쓰는 로직
        {
            baseitemslot.Amount -= discount;
            if (baseitemslot.Amount <= 0)
            {
                baseitemslot.item = null;
            }
          //  
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
            if (iventorynsy.CanAddItem(item) && equipPanel.RemoveItem(item))
            {
                iventorynsy.AddItem(item);
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



      
        public void  ClickPostSlotUi()
        {
          
            Debug.Log("버튼누름");
            PostEvent.BtnClickPost();

        }
    }

}

