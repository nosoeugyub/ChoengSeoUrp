using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DM.Inven;
using System;
using UnityEngine.Serialization;

namespace NSY.Iven
{
    public class InventoryNSY : ItemContainer
    {
      //  [FormerlySerializedAs("items")]
        [SerializeField] Item[] startingitems;   // 아이템 리스트
        [SerializeField] Transform itemsParent; //this
     
        protected override void OnValidate()
        {
            if (itemsParent != null)
            {
                itemsParent.GetComponentsInChildren(includeInactive: true, result: ItemSlots);
            }
        }
        protected override void Awake()
        {
            base.Awake();
            SetStartingTiems();
        }
        private void SetStartingTiems()//아이템 슬롯과 리스트가 일치하게 돌려주는 함수 setstarign
        {
            Clear();
            foreach (Item item in startingitems)
            {
                if (item)
                {
                    AddItem(item.GetCopy());
                }
            }
        }


    }
}

