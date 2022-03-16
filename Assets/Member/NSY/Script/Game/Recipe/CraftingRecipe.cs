using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DM.Inven;
using System;

namespace NSY.Iven
{
    [Serializable]
    public struct ItemAmount //아이템 제작에 필요한 갯수와 아이템
    {
        public Item Item;
        [Range(1,100)] //갯수 
        public int Amount;
    }

    [CreateAssetMenu]
    public class CraftingRecipe : ScriptableObject
    {
        InventoryNSY inventory;
        CraftManager craftpanel;

        //재료를 생성할 때 소비될 재료
        public List<ItemAmount> Materials;
        public List<ItemAmount> Results; //결과



        

        public void Craft(IItemContainer itemContainert)
        {
            if (CanCraft(itemContainert))
            {
                RemoveMataial(craftpanel);
                AddResult(inventory);
            }
        }
        public bool CanCraft(IItemContainer itemContainer) //조합가능 여부 
        {


            return HasMaterials(itemContainer) && HasSpace(itemContainer);
        }
        private bool HasMaterials(IItemContainer itemContainer)
        {
            foreach (ItemAmount itemAmount in Materials)
            {
                if (itemContainer.ItemCount(itemAmount.Item.ItemName) < itemAmount.Amount)
                {
                    Debug.LogWarning("You don't have the required materials.");
                    return false;
                }
            }
            return true;
        }

        private bool HasSpace(IItemContainer itemContainer)
        {
            foreach (ItemAmount itemAmount in Results)
            {
                if (!itemContainer.CanAddItem(itemAmount.Item, itemAmount.Amount))
                {
                    Debug.LogWarning("Your inventory is full.");
                    return false;
                }
            }
            return true;
        }


        public void RemoveMataial(CraftManager craftSlots)
        {
            foreach (ItemAmount itemAmount in Materials)
            {
                for (int i = 0; i < itemAmount.Amount; i++)
                {
                    craftSlots.RemoveItem(itemAmount.Item);
                    
                }
            }
        }
        public void AddResult(IItemContainer itemContainern)
        {
            

            foreach (ItemAmount itemAmount in Results)
            {
                for (int i = 0; i < itemAmount.Amount; i++)
                {
                    itemContainern.AddItem(itemAmount.Item.GetCopy());
                }
            }
          
        }

    }


}
