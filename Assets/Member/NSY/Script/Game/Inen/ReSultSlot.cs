using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.EventSystems;
namespace NSY.Iven
{
    public class ReSultSlot : ItemSlot 
    {
 
        public override bool CanReceiveItem(Item item)
        {
            if (item == null)
            {
                return true;
            }
            EquippableItem equippableitem = item as EquippableItem;
            return equippableitem != null;
        }

       
    }

}

