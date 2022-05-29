using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DM.Inven;
using NSY.Player;

namespace NSY.Iven
{
    [CreateAssetMenu]
    public class UseableItem : Item
    {
       

        public bool isConsumable;
        public int somAmount;
        public virtual void Use(InvenToryManagers inventoryMgr)
        {
         //  inventoryMgr.Vital += somAmount;


        }
    }


}

