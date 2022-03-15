using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DM.Inven;
using System;

namespace NSY.Iven
{
    public class CraftManager : MonoBehaviour
    {
        Item CurrentItem;
        //레시피
        public List<Item> itemList;
        public string[] recipes;
        public Item[] recipeResults;




        [SerializeField] Transform CraftingSlotsParent;
        [SerializeField] CraftSlot[] CratfingSlots;
        public CraftSlot ResultSlot;


        [Header("Public variables")]
        public IItemContainer itemContainer; //아이템을 제작하기위한 참조


        private CraftingRecipe craftingRecipe;
        public CraftingRecipe CraftingRecipe
        {
            get
            {
                return craftingRecipe;
            }
            set
            {
                SetCraftingRecipe(value);
            }

        }

        


        public event Action<BaseItemSlot> OnPointerEnterEvent;
        public event Action<BaseItemSlot> OnPointerExitEvent;
        public event Action<BaseItemSlot> OnLeftClickEvent;
        public event Action<BaseItemSlot> OnBeginDragEvent;
        public event Action<BaseItemSlot> OnEndDragEvent;
        public event Action<BaseItemSlot> OnDragEvent;
        public event Action<BaseItemSlot> OnDropEvent;


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


        private void OnValidate()
        {
            CratfingSlots = CraftingSlotsParent.GetComponentsInChildren<CraftSlot>();
        }

        public void OuMouseDownItem(Item item)
        {
            if (CurrentItem == null)
            {
                CurrentItem = item;
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

        private void SetCraftingRecipe(CraftingRecipe newCraftingRecipe)
        {
            craftingRecipe = newCraftingRecipe;

            if (craftingRecipe != null)
            {
                int slotIndex = 0;
                slotIndex = SetSlots(craftingRecipe.Materials, slotIndex);
               // arrowParent.SetSiblingIndex(slotIndex);
                slotIndex = SetSlots(craftingRecipe.Results, slotIndex);

                for (int i = slotIndex; i < CratfingSlots.Length; i++)
                {
                    CratfingSlots[i].transform.parent.gameObject.SetActive(false);
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
                CraftSlot itemSlot = CratfingSlots[slotIndex];

                itemSlot.item = itemAmount.Item;
                itemSlot.Amount = itemAmount.Amount;
                itemSlot.transform.parent.gameObject.SetActive(true);
            }
            return slotIndex;
        }
        public void OnCraftButtonClick()
        {
            if (craftingRecipe != null && itemContainer != null)
            {
                craftingRecipe.Craft(itemContainer);
            }
        }
      
    }

}

