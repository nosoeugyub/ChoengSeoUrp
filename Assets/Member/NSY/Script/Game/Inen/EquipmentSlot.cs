using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DM.Inven;

namespace NSY.Iven
{
    public class EquipmentSlot : ItemSlot
    {
        public OutItemType equipmentType;

        protected override void OnValidate()//개체의 이름을 지정
        {
            base.OnValidate();
            gameObject.name = equipmentType.ToString() + " Slot";
        }

        public override bool CanReceiveItem(Item item)
        {
            if (item == null)
            {
                return true;
            }
            EquippableItem equippableitem = item as EquippableItem;
            return item != null && item.OutItemType == equipmentType;
        }
    }
}


