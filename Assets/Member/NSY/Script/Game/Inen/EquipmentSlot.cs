﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DM.Inven;
using UnityEngine.UI;
namespace NSY.Iven
{
    public class EquipmentSlot : ItemSlot
    {
        public OutItemType equipmentType;
        public Image GetImg;

       
        private Image EqImg;
        public Image _EqImg
        {
            get 
            {
                return EqImg;
            }
            set
            {
                EqImg = value;
                EqImg.sprite = item.ItemSprite;
                GetImg.sprite = EqImg.sprite;
            }
        }


        protected override void OnValidate()//개체의 이름을 지정
        {
        //    base.OnValidate();
            gameObject.name = equipmentType.ToString() + " Slot";
            _EqImg = _EqImg;
        }

        public override bool CanReceiveItem(Item item)
        {
            if (item == null)
            {
                return true;
            }
            Item equippableitem = item ;
             return item != null && item.OutItemType == equipmentType;
        }
    }
}


