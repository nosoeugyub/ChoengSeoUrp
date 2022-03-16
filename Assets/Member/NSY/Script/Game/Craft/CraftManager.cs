using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DM.Inven;
using System;

namespace NSY.Iven
{
    public class CraftManager : MonoBehaviour
    {
        Item CurrentItem;
        //레시피
       
        
        public Item[] recipeResults;
        [SerializeField] BaseItemSlot[] itemSlots;
        [SerializeField] RectTransform arrowParent;
        public int slotIndex = 0;

        [Header("레시피 컴포넌트")]
        public List<Item> itemList;
        [SerializeField] Transform CraftingSlotsParent;
        [SerializeField] CraftSlot[] CratfingSlots;
        [SerializeField] CraftingRecipe[] Recipe;
        [SerializeField] CraftSlot ResultSlot;
        CraftSlot craftslot;
       

        [Header("Public variables")]
        public IItemContainer itemContainer; //아이템을 제작하기위한 참조



        private CraftingRecipe craftingRecipe;
        public CraftingRecipe CraftingRecipe
        {
            get { return craftingRecipe; }
            set { SetCraftingRecipe(value); }
        }



        public event Action<BaseItemSlot> OnPointerEnterEvent;
        public event Action<BaseItemSlot> OnPointerExitEvent;
        public event Action<BaseItemSlot> OnLeftClickEvent;
        public event Action<BaseItemSlot> OnBeginDragEvent;
        public event Action<BaseItemSlot> OnEndDragEvent;
        public event Action<BaseItemSlot> OnDragEvent;
        public event Action<BaseItemSlot> OnDropEvent;

        private void OnValidate()
        {
            CratfingSlots = CraftingSlotsParent.GetComponentsInChildren<CraftSlot>();
        }

        private void Start()
        {
            //for (int i = 0; i < CratfingSlots.Length; i++)
          //  {
                //CratfingSlots[i].OnPointerEnterEvent += OnPointerEnterEvent;
                //CratfingSlots[i].OnPointerExitEvent += OnPointerExitEvent;
                //CratfingSlots[i].OnRightClickEvent += OnLeftClickEvent;
                //CratfingSlots[i].OnBeginDragEvent += OnBeginDragEvent;
                //CratfingSlots[i].OnEndDragEvent += OnEndDragEvent;
                //CratfingSlots[i].OnDragEvent += OnDragEvent;
                //CratfingSlots[i].OnDropEvent += OnDropEvent;
          //  }


            foreach (CraftSlot craftSlot in CratfingSlots)
            {
                craftSlot.OnPointerEnterEvent += OnPointerEnterEvent;
                craftSlot.OnPointerExitEvent += OnPointerExitEvent;
                craftSlot.OnRightClickEvent += OnLeftClickEvent;
                craftSlot.OnBeginDragEvent += OnBeginDragEvent;
                craftSlot.OnEndDragEvent += OnEndDragEvent;
                craftSlot.OnDragEvent += OnDragEvent;
                craftSlot.OnDropEvent += OnDropEvent;
            }
        }


      

       

        public  bool CraftAddItem( Item item)
        {
            for (int i = 0; i < CratfingSlots.Length; i++)
            {
                if (CratfingSlots[i].CanAddStack(item))
                {
                    CratfingSlots[i].item = item;
                    CratfingSlots[i].Amount++;
                    return true;
                }


            }
            for (int i = 0; i < CratfingSlots.Length; i++)
            {
                if (CratfingSlots[i].item == null)
                {
                    CratfingSlots[i].item = item;
                    CratfingSlots[i].Amount++;
                    return true;
                }


            }
            return false;
        }
        //제거
        public bool RemoveItem(Item item)
        {
            for (int i = 0; i < CratfingSlots.Length; i++)
            {
                if (CratfingSlots[i].item == item)
                {
                    CratfingSlots[i].item = null;
                    CratfingSlots[i].Amount = 0;
                    return true;
                }


            }
            return false;
        }



      
       public void OnResultBtn()
        {
            Debug.Log("버튼클릭함");
            craftingRecipe.Craft(itemContainer);
        }
        private void SetCraftingRecipe(CraftingRecipe newCraftingRecipe)
        {
            craftingRecipe = newCraftingRecipe;

            if (craftingRecipe != null)
            {
                int slotIndex = 0;
                slotIndex = SetSlots(craftingRecipe.Materials, slotIndex);
                arrowParent.SetSiblingIndex(slotIndex);
                slotIndex = SetSlots(craftingRecipe.Results, slotIndex);

                for (int i = slotIndex; i < itemSlots.Length; i++)
                {
                    itemSlots[i].transform.parent.gameObject.SetActive(false);
                }

                gameObject.SetActive(true);
            }
            else
            {
                gameObject.SetActive(false);
            }
        }
        private int SetSlots(IList<ItemAmount> itemAmountList, int slotIndex)
        {
            for (int i = 0; i < itemAmountList.Count; i++, slotIndex++)
            {
                ItemAmount itemAmount = itemAmountList[i];
                BaseItemSlot itemSlot = itemSlots[slotIndex];

                itemSlot.item = itemAmount.Item;
                itemSlot.Amount = itemAmount.Amount;
                itemSlot.transform.parent.gameObject.SetActive(true);
            }
            return slotIndex;
        }

    }

}

