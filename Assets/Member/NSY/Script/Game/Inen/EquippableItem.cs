using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DM.Inven;

namespace NSY.Iven
{
   
    public enum EquipmentType
    {
       Tool_NSY
    }

    [CreateAssetMenu]
    public class EquippableItem : Item
    {
        public int VitalPlus;
        public int VitalMius;
       
        public EquipmentType equipmentType;

    }

}

