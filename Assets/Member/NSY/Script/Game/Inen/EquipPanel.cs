using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DM.Inven;
using System;

namespace NSY.Iven
{
    public class EquipPanel : MonoBehaviour
    {
        [SerializeField] Transform equipmentSlotsParent;
        [SerializeField] EquipmentSlot[] equipmentSlots;

        public event Action<ItemSlot> OnPointerEnterEvent;
        public event Action<ItemSlot> OnPointerExitEvent;
        public event Action<ItemSlot> OnRightClickEvent;
        public event Action<ItemSlot> OnBeginDragEvent;
        public event Action<ItemSlot> OnEndDragEvent;
        public event Action<ItemSlot> OnDragEvent;
        public event Action<ItemSlot> OnDropEvent;

        private void OnValidate()
        {
            equipmentSlots = equipmentSlotsParent.GetComponentsInChildren<EquipmentSlot>();
        }
        //추가 out 매개 변수는 반환 값이 있는 것과 같지만 해당 값이 할당될 매개변수로 변수를 전달 합니다.
        //슬롯에 추가하기전에 out변수에 이전항목을 할당해야합니다
        public bool AddItem(Item item , out Item previousitem)
        {
            for (int i = 0; i < equipmentSlots.Length; i++)
            {
                if (equipmentSlots[i].itemtype == item.ItemType)
                {
                    previousitem = (Item)equipmentSlots[i].item;
                    equipmentSlots[i].item = item;
                    return true;
                }
                

            }
            previousitem = null; //아니면 다시 인벤토리로 
            return false;
        }
        //제거
        public bool RemoveItem(Item item)
        {
            for (int i = 0; i < equipmentSlots.Length; i++)
            {
                if (equipmentSlots[i].item == item)
                {
                    equipmentSlots[i].item = null;
                    return true;
                }


            }
            return false;
        }
    }

}

