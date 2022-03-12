using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DM.Inven;
using UnityEngine.EventSystems;

namespace NSY.Iven
{
    public class CraftSlot : ItemSlot
    {
       public int index;
        Item item;

        public ItemType itemtype;

        protected override void OnValidate()
        {
            base.OnValidate();
            gameObject.name = itemtype.ToString() + " Slot";
        }

      
    }

}
