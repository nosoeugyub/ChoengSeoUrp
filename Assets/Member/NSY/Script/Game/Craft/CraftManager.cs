using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DM.Inven;
using System;

namespace NSY.Iven
{
    public class CraftManager : MonoBehaviour
    {
        public Item CurrentItem;

        [SerializeField] Transform CraftingSlotsParent;
        [SerializeField] CraftSlot[] CratfingSlots;

        public event Action<BaseItemSlot> OnPointerEnterEvent;
        public event Action<BaseItemSlot> OnPointerExitEvent;
        public event Action<BaseItemSlot> OnRightClickEvent;
        public event Action<BaseItemSlot> OnBeginDragEvent;
        public event Action<BaseItemSlot> OnEndDragEvent;
        public event Action<BaseItemSlot> OnDragEvent;
        public event Action<BaseItemSlot> OnDropEvent;


        private void Start()
        {
            for (int i = 0; i < CratfingSlots.Length; i++)
            {
                CratfingSlots[i].OnPointerEnterEvent += OnPointerEnterEvent;
                CratfingSlots[i].OnPointerExitEvent += OnPointerExitEvent;
                CratfingSlots[i].OnRightClickEvent += OnRightClickEvent;
                CratfingSlots[i].OnBeginDragEvent += OnBeginDragEvent;
                CratfingSlots[i].OnEndDragEvent += OnEndDragEvent;
                CratfingSlots[i].OnDragEvent += OnDragEvent;
                CratfingSlots[i].OnDropEvent += OnDropEvent;
            }

        }


        private void OnValidate()
        {
            CratfingSlots = CraftingSlotsParent.GetComponentsInChildren<CraftSlot>();
        }




        public bool CraftAddItem(Item item, out Item Craftpreviousitem)
        {
            for (int i = 0; i < CratfingSlots.Length; i++)
            {
                if (CratfingSlots[i].itemtype == item.ItemType)
                {
                    Craftpreviousitem = (Item)CratfingSlots[i].item;
                    CratfingSlots[i].item = item;
                    CratfingSlots[i].Amount = 1;
                    return true;
                }


            }
            Craftpreviousitem = null; //아니면 다시 인벤토리로 
            return false;
        }
        //제거
        public bool RemoveItem(Item item)
        {
            for (int i = 0; i < CratfingSlots.Length; i++)
            {
                if (CratfingSlots[i].item == item)
                {
                    CratfingSlots[i].item = null;
                    CratfingSlots[i].Amount = 0;
                    return true;
                }


            }
            return false;
        }



      
    }

}

