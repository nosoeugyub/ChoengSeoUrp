using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DM.Inven;
using System;
using UnityEngine.Serialization;

namespace NSY.Iven
{
    public class InventoryNSY : ItemContainer
    {
        [FormerlySerializedAs("items")]
        [SerializeField] Item[] startingitems; // 아이템 리스트
        [SerializeField] Transform itemsParent; //this
     
      

        public event Action<BaseItemSlot> OnPointerEnterEvent;
        public event Action<BaseItemSlot> OnPointerExitEvent;
        public event Action<BaseItemSlot> OnRightClickEvent;
        public event Action<BaseItemSlot> OnBeginDragEvent;
        public event Action<BaseItemSlot> OnEndDragEvent;
        public event Action<BaseItemSlot> OnDragEvent;
        public event Action<BaseItemSlot> OnDropEvent;

        private void Start()
        {
            for (int i = 0; i < itemSlots.Length; i++)
            {
                itemSlots[i].OnPointerEnterEvent += OnPointerEnterEvent;
                itemSlots[i].OnPointerExitEvent += OnPointerExitEvent;
                itemSlots[i].OnRightClickEvent += OnRightClickEvent;
                itemSlots[i].OnBeginDragEvent += OnBeginDragEvent;
                itemSlots[i].OnEndDragEvent += OnEndDragEvent;
                itemSlots[i].OnDragEvent += OnDragEvent;
                itemSlots[i].OnDropEvent += OnDropEvent;
            }
            ReFreshUi();
        }
        
        private void OnValidate()
        {
            if (itemsParent != null)
            {
               // itemsParent.GetComponentsInChildren(includeInactive: true, result: itemSlots);
                itemSlots = itemsParent.GetComponentsInChildren<ItemSlot>(); //itemslot => baseitemslot수정
            }
            ReFreshUi();
        }
        private void ReFreshUi()//아이템 슬롯과 리스트가 일치하게 돌려주는 함수
        {
            int i = 0;
            for (; i < startingitems.Length && i<itemSlots.Length; i++)//가지고 있는 슬롯에 아이템 리스트를 할당,
            {
                itemSlots[i].item = startingitems[i].GetCopy();
                itemSlots[i].Amount = 1;
            }

            for ( ; i < itemSlots.Length; i++) // 들어갈 항목이 없는 나머지 슬롯에  Null
            {
                itemSlots[i].item = null;
                itemSlots[i].Amount = 0;
            }
        }

        //bool을 반화하는 슬롯체크 함수
        //항목을 items 리스트에 추가하고 ui를 새로고침한뒤 true;반환 슬롯에 항목을 장착할 수 있는지 확인 슬롯이 비어있으면 아이템 추가
      
        
        //제거 제거가 가능하다면 제거하고 새로고침 슬롯에 아이템 체크
       
       
       


       

       

       
    }

}

