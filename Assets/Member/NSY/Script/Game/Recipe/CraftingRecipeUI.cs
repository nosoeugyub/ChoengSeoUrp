using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DM.Inven;
using System;
namespace NSY.Iven
{

    public class CraftingRecipeUI : MonoBehaviour
    {
        [Header("Renferences")]
        [SerializeField] RectTransform arrowParent;
        [SerializeField] BaseItemSlot[] itemSlots;

        [Header("Public Variables")]
        public IItemContainer ItemContainer;

        public CraftingRecipe craftingRectipe;
        public CraftingRecipe CraftingRecipe
        {
            get { return CraftingRecipe; }
            set { SetCraftingRecipe(value); }
        }


        public event Action<BaseItemSlot> OnPointerEnterEvent;
        public event Action<BaseItemSlot> OnPoinerExitEvent;

        private void OnValidate()
        {
            itemSlots = GetComponentsInChildren<BaseItemSlot>(includeInactive: true);

        }

        private void Start()
        {
            foreach (BaseItemSlot itemSlot in itemSlots)//모든항목을 업데이트
            {
                itemSlot.OnPointerEnterEvent += OnPointerEnterEvent;
                itemSlot.OnPointerExitEvent += OnPoinerExitEvent;
            }
        }

        public void OnCraftButtonClick()
        {
            if (CraftingRecipe != null && ItemContainer != null)
            {
                if (CraftingRecipe.CanCraft(ItemContainer))
                {
                   // if (!ItemContainer.CanAddItem(Item item, 1))
                   // {
                        craftingRectipe.Craft(ItemContainer);
                  //  }
                 //   else
                 //   {
                        Debug.Log("인벤꽉참");
                   // }
                }
                else
                {
                    Debug.Log("만들기 불가!");
                }
            }


        }


        //
        private void SetCraftingRecipe(CraftingRecipe newCraftingRecipe)
        {
            CraftingRecipe = newCraftingRecipe;

            if (CraftingRecipe != null)
            {
                int slotIndex = 0;
                slotIndex = SetSlotss(CraftingRecipe.Materials, slotIndex);
                arrowParent.SetSiblingIndex(slotIndex);
                slotIndex = SetSlotss(craftingRectipe.Results, slotIndex);

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
        

        private int SetSlotss(IList<ItemAmount> itemAmountList, int slotIndex)
        {
        for (int i = 0; i < itemAmountList.Count; i++, slotIndex++)
        {
            ItemAmount itemAmount = itemAmountList[i];
            BaseItemSlot itemSlot = itemSlots[slotIndex];

            itemSlot.item
                    = itemAmount.Item;
            itemSlot.Amount = itemAmount.Amount;
            itemSlot.transform.parent.gameObject.SetActive(true);
        }
        return slotIndex;
    }

    }

}
