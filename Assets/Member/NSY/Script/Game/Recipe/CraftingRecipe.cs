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
        public Item item;
       
        [Range(1,100)] //갯수 
        public int Amount;
    }

    [CreateAssetMenu]
    public class CraftingRecipe : ScriptableObject
    {
       

        //재료를 생성할 때 소비될 재료
        public List<ItemAmount> Materials;//재료
        public List<ItemAmount> Results; // 결과

        public bool CanCraft(IItemContainer itemContainer)
        {
            return HasMaterials(itemContainer) ; 
        }

        public bool HasMaterials(IItemContainer itemContainer)
        {
            foreach (ItemAmount itemAmount in Materials)
            {
                if (itemContainer.ItemCount(itemAmount.item.ItemName) < itemAmount.Amount)
                {
                    return false;
                }
            }
            return true;
        }


        public void Craft(IItemContainer itemContainer)
        {
            if (CanCraft(itemContainer))
            {
                RemoveMat(itemContainer);
                AddResult(itemContainer);
            }
        }

        private void RemoveMat(IItemContainer itemContainer)
        {
            foreach (ItemAmount itemAmount in Materials)
            {
                for (int i = 0; i < itemAmount.Amount; i++)
                {
                    Item oldItem = itemContainer.RemoveItem(itemAmount.item.ItemName);
                    oldItem.Destroy();
                }
            }
        }

        private void AddResult(IItemContainer itemContainer)
        {
            foreach (ItemAmount itemAmount in Results)
            {
                for (int i = 0; i < itemAmount.Amount; i++)
                {
                    itemContainer.AddItem(itemAmount.item.GetCopy());
                }
            }
        }
    }


}
