using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DM.Inven;

namespace NSY.Iven
{
    public class CraftSlot : ItemSlot
    {
       public int index;
        public Item item;

        public ItemType itemtype;

        protected override void OnValidate()
        {
            base.OnValidate();
            gameObject.name = itemtype.ToString() + " Slot";
        }

        public override bool CanReceiveItem(Item item)
        {
            if (item == null)
            {
                return true;
            }
            Item EQitem = item as Item;
            return EQitem != null && EQitem.ItemType == itemtype;
        }
    }

}
