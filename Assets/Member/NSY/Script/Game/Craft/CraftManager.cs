using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DM.Inven;
using System;

namespace NSY.Iven
{
    public class CraftManager : MonoBehaviour 
    {

        public Item _item;
        public Item Item
        {
            get
            {
                return _item;
            }
            set
            {
                _item = value;

                if (_item == null)
                {
                   
                }

            }
        }



    }

}

