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
        public ItemAmount (Item a , int b) { Item = a; Amount = b; }
        [Range(1,100)] //갯수 
        public int Amount;
    }

    [CreateAssetMenu]
    public class CraftingRecipe : ScriptableObject
    {
        CraftManager craftpanel;
        InventoryNSY inventory;

        //재료를 생성할 때 소비될 재료
        public List<ItemAmount> Materials;//재료
        public ItemAmount Results; //결과



        

       

    }


}
