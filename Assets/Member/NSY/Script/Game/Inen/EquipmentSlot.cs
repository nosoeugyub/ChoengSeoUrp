using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DM.Inven;

namespace NSY.Iven
{
    public class EquipmentSlot : ItemSlot
    {
        public ItemType itemtype;

        protected override void OnValidate()
        {
            base.OnValidate();
            gameObject.name = itemtype.ToString() + " Slot";
        }
    }
}


