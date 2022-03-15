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
       

        public InItemType itemtype;

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
            Item craftitem = item ;
            return craftitem != null && craftitem.InItemType == itemtype;
        }

    }

}
