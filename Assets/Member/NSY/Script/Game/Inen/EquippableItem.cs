using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DM.Inven;

namespace NSY.Iven
{
    

    [CreateAssetMenu]
    public class EquippableItem : Item
    {
        public int VitalPlus;
        public int VitalMius;
        [Space]
        public ItemType itemtype;
    }

}

