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
        //재료를 생성할 때 소비될 재료
        public List<ItemAmount> Materials;
        public List<ItemAmount> Results; //결과



        public bool CanCraft(IItemContainer itemContainer ) //조합가능 여부 
        {

            foreach (ItemAmount itemAmount in Materials)//항목을 체크하고 항목여부에따라 bool값 반환
            {
                if (itemContainer.ItemCount(itemAmount.Item.ItemName) < itemAmount.Amount)
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
                foreach (ItemAmount itemAmount in Materials)
                {
                    for (int i = 0; i < itemAmount.Amount; i++)
                    {
                        Item oldItem = itemContainer.RemoveItem(itemAmount.Item.ItemName);
                        oldItem.Destroy();
                    }
                }

                foreach (ItemAmount itemAmount in Materials)
                {
                    for (int i = 0; i < itemAmount.Amount; i++)
                    {
                        itemContainer.AddItem(itemAmount.Item.GetCopy());
                    }
                }
            }
        }
    }


}
