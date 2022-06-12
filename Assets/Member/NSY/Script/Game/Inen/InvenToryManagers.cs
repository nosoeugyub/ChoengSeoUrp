using DG.Tweening;
using DM.Building;
using NSY.Player;
using UnityEngine;
using UnityEngine.UI;

namespace NSY.Iven
{
    public class InvenToryManagers : MonoBehaviour
    {
        //조합

        public BaseItemSlot dragitemSlot;
        public ReSultSlot resultslot;
        //버리기
        public Text ScriptTxt;
        public int MaxDiscount;
        [SerializeField]
        private int discount;
        public int _discount
        {
            get
            {
                return discount;
            }
            set
            {
                discount = value;
                if (discount < 0)
                {
                    discount = 0;
                }
                if (discount > MaxDiscount)
                {
                    discount = MaxDiscount;
                }
                if (ScriptTxt != null && ScriptTxt.enabled)
                {
                    ScriptTxt.text = discount.ToString();
                }
            }
        }
        [SerializeField] ItemTooltip itemTooltip;

        [SerializeField] InventoryNSY iventorynsy;
        //[SerializeField] EquipPanel equipPanel;

        [SerializeField] Image draggableitem;
        [SerializeField] DropItemArea Dropitemarea;
        [SerializeField] QuestionDialog questionDialog;
        [SerializeField] PlayerEat PlayerEat;
        [SerializeField] PlayerInteract playerinterract;
        //조합 필요한 컴포넌트들
        CraftSlot craftslot;

        //레시피
        [Header("레시피 레퍼런스")]
        private CraftingRecipe carftingRecipe;


        

        //더블클릭
        private float firstClickTime, timeBetweenClicks;
        private bool coroutineAllowed;
        private int clickCounter;

        //Ui Rect
        public RectTransform BaseCharcterPanel;
        public RectTransform button_UpDown;
        public bool isUp;
        public bool isDown;
      

        //빌딩쓰
        bool isBuildingHands = false;
        public Item CheckBuliditem;

        private void OnValidate()
        {
            _discount = discount;
            
        }

        private void Awake()
        {
           
            //툴립
            iventorynsy.OnPointerEnterEvent += ShowToolTip;
            //equipPanel.OnPointerEnterEvent += ShowToolTip;
            //
            iventorynsy.OnPointerExitEvent += HideTootip;
            //equipPanel.OnPointerExitEvent += HideTootip;


            //인벤토리 클레스 이벤트

            iventorynsy.OnDubleClickEvent += OnDoubleClickEvent;
            iventorynsy.OnLeftClickEvent += BuildingLeftClick;
            iventorynsy.OnRightClickEvent += InventoryRightClick;
            // iventorynsy.OnLeftClickEvent += InventoryLeftClick;
            //equipPanel.OnLeftClickEvent += EquipmentPanelLeftClick;
            //  craftPanel.OnLeftClickEvent += CraftPanelLeftClick;
            //드래그 시작
            iventorynsy.OnBeginDragEvent += BeginDrag;
            //equipPanel.OnBeginDragEvent += BeginDrag;
            //   craftPanel.OnBeginDragEvent += BeginDrag;
            //드래그 끝
            iventorynsy.OnEndDragEvent += EndDrag;
            //equipPanel.OnEndDragEvent += EndDrag;
            //   craftPanel.OnEndDragEvent += EndDrag;
            //드래그
            iventorynsy.OnDragEvent += Drag;
            //equipPanel.OnDragEvent += Drag;
            //    craftPanel.OnDragEvent += Drag;
            //드롭
            iventorynsy.OnDropEvent += Drop;
            //equipPanel.OnDropEvent += Drop;
            //     craftPanel.OnDropEvent += Drop;
            Dropitemarea.OnDropEvent += DropItemOutsideUI;


            //carftingRecipe = new CraftingRecipe();
        }

        private void HideTootip(BaseItemSlot itemSlot)
        {
            if (itemTooltip.gameObject.activeSelf)
            {
                itemTooltip.HideTooltip();
            }
        }

        private void ShowToolTip(BaseItemSlot itemSlot)
        {
            if (itemSlot.item != null)
            {
                itemTooltip.ShowItemTooltip(itemSlot.item);
                Vector3 ToolVec = itemTooltip.tooltipTransform.transform.position;
                ToolVec.x = itemSlot.GetComponent<Image>().rectTransform.position.x + 0.7f;
                ToolVec.y = itemSlot.GetComponent<Image>().rectTransform.position.y + 0.3f;
                ToolVec.z = itemSlot.GetComponent<Image>().rectTransform.position.z;
                itemTooltip.tooltipTransform.transform.position = ToolVec;
            }
        }




        public bool isRed = false;
        private void BuildingLeftClick(BaseItemSlot obj)
        {

            if (CheckBuliditem != null)
            {
                return;
            }

            if (BuildingBlock.isBuildMode && CheckBuliditem == null)
            {
                CheckBuliditem = obj.item;
                BuildingBlock.nowBuildingBlock.BtnSpawnHouseBuildItem(obj.item);

                foreach (ItemSlot itemslots in iventorynsy.ItemSlots) //인벤 빌딩슬롯 정검 
                {
                    if (!itemslots.item) continue;
                    if (itemslots.item.OutItemType == OutItemType.BuildingItemObj || CheckBuliditem.ItemName != itemslots.item.ItemName)
                    {
                        itemslots.Interactble(false);
                        itemslots.isRedbulid = true;

                    }
                    if (itemslots.item == CheckBuliditem)
                    {
                        obj.Amount--;
                    }
                }
                //  obj.Amount--;


            }
            else
            {
                BuildingHandyObjSpawn HandySpawnObj = FindObjectOfType<BuildingHandyObjSpawn>();
                switch (obj.item.InItemType)
                {
                    case InItemType.BuildingItemObj_Essential:
                        HandySpawnObj.HandySpawnBuildItem(obj.item);

                        break;
                    case InItemType.BuildingItemObj_Additional:
                        HandySpawnObj.HandySpawnBuildItem(obj.item);

                        break;
                }
            }

        }


        //더블클릭
        public void OnDoubleClickEvent(BaseItemSlot itemslot)
        {
            if (itemslot.item.OutItemType == OutItemType.Food)
            {
                PlayerEat.Eat(itemslot);
            }
        }
        private void InventoryRightClick(BaseItemSlot itemslot)
        {
            if (itemslot.item.OutItemType == OutItemType.Tool)
            {
                Unequip(itemslot.item);
            }
            else if (itemslot.item is UseableItem)
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

        //장비슬롯에서 눌렀을때
        private void EquipmentPanelLeftClick(BaseItemSlot itemslot)
        {
            Equip(itemslot.item); //장비칸장착
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
        }

        private void Drop(BaseItemSlot dropitemslot)
        {
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
            Debug.Log("씨발련아 ㅣㅈ나ㅉ");
            if (dragitemSlot == null)
            {
                return;
            }
            questionDialog.Show();
            BaseItemSlot baseitemslot = dragitemSlot;
            MaxDiscount = baseitemslot.Amount;
            questionDialog.OnYesEvent += () => DestroyItem(baseitemslot);
        }

        public void PlusBtn()
        {
            _discount++;
           
        }
        public void MiuseBtn()
        {
            _discount--;
        }

        private void DestroyItem(BaseItemSlot baseitemslot)//버릴떄 쓰는 로직
        {
            baseitemslot.item.GetCountItems -= discount;
            baseitemslot.Amount -= discount;
            if (baseitemslot.Amount <= 0)
            {
                baseitemslot.item = null;
            }
            //  
        }




        //장비창에서 장착창으로
        public void Equip(Item item)
        {
           // if (equipPanel.AddResultItem(item))
            {


            }

        }

        //인벤창에서 장착창으로
        public void Unequip(Item item)//
        {

            //if (equipPanel.AddItem(item))
            {
                if (iventorynsy.RemoveItem(item))
                {

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

        public void Open_CloseBtn()
        {
            if (isUp == true)
            {
                Up();
            }
            else
            {
                Down();
            }
        }
        private void Up()
        {
            BaseCharcterPanel.DOLocalMoveY(0, 1).SetEase(Ease.OutQuart);
            button_UpDown.DOBlendableLocalRotateBy(new Vector3(0, 0, 180), 1, RotateMode.Fast).SetEase(Ease.OutQuart);
            //BaseCharcterPanel.anchoredPosition = new Vector3(0, 0, 0);
            isUp = false;
            isDown = true;
        }
        void Down()
        {
            BaseCharcterPanel.DOLocalMoveY(-160, 1).SetEase(Ease.OutQuart);
            button_UpDown.DOBlendableLocalRotateBy(new Vector3(0, 0, 180), 1, RotateMode.Fast).SetEase(Ease.OutQuart);

            //BaseCharcterPanel.anchoredPosition = new Vector3(0, -170, 0);
            isUp = true;
            isDown = false;
        }

    }

}

