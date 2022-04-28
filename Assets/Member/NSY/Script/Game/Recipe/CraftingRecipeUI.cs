using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DM.Inven;
using System;
namespace NSY.Iven
{

    public class CraftingRecipeUI : MonoBehaviour
    {
        [Header("참조 스크립트")]
        [SerializeField] BaseItemSlot[] CraftItemSlot;

        public ItemContainer itemContainer;

        [Header("레시피 프로퍼티")]
        private CraftingRecipe craftingrecipe;
        public CraftingRecipe Craftingrecipe
        {
            get
            {
                return craftingrecipe;
            }
            set
            {
                SetCraftingRecipe(value);
            }
        }


        private void OnValidate()
        {
            CraftItemSlot = GetComponentsInChildren<BaseItemSlot>(includeInactive: true);
        }

        public void OnCraftButtonClick()
        {
            if (craftingrecipe != null && itemContainer != null)
            {
                craftingrecipe.Craft(itemContainer);
            }
        }

        private void SetCraftingRecipe(CraftingRecipe newCraftingRecipe)
        {
            craftingrecipe = newCraftingRecipe;

            if (craftingrecipe != null)
            {
                int slotIndex = 0;
                slotIndex = SetSlots(craftingrecipe.Materials, slotIndex);
               // arrowParent.SetSiblingIndex(slotIndex);
                slotIndex = SetSlots(craftingrecipe.Results, slotIndex);

                for (int i = slotIndex; i < CraftItemSlot.Length; i++)
                {
                    CraftItemSlot[i].transform.parent.gameObject.SetActive(false);
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
                BaseItemSlot itemSlot = CraftItemSlot[slotIndex];

                itemSlot.item = itemAmount.item;
                itemSlot.Amount = itemAmount.Amount;
                itemSlot.transform.parent.gameObject.SetActive(true);
            }
            return slotIndex;
        }

    }

}
