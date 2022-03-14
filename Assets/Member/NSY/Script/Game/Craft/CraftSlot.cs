using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DM.Inven;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace NSY.Iven
{
    public class CraftSlot : ItemSlot
    {
       public int index;
       

        public ItemType itemtype;

        protected override void OnValidate()
        {
            base.OnValidate();
            gameObject.name = itemtype.ToString() + " Slot";
        }

      
    }

}
