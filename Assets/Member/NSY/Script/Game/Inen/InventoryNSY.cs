using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DM.Inven;
using System;
namespace NSY.Iven
{
    public class InventoryNSY : MonoBehaviour
    {
        [SerializeField] List<Item> items; // 아이템 리스트
        [SerializeField] Transform itemsParent;
        [SerializeField] ItemSlot[] itemSlots;  //배열

        public event Action<Item> OnItemRightClickEvent;

        private void Awake()
        {
            for (int i = 0; i < itemSlots.Length; i++)
            {
                itemSlots[i].OnRightClickEvent += OnItemRightClickEvent;
            }
        }

        private void OnValidate()
        {
            if (itemsParent != null)
            {
                itemSlots = itemsParent.GetComponentsInChildren<ItemSlot>();
            }
            ReFreshUi();
        }
        private void ReFreshUi()//아이템 슬롯과 리스트가 일치하게 돌려주는 함수
        {
            int i = 0;
            for (; i < items.Count && i<itemSlots.Length; i++)//가지고 있는 슬롯에 아이템 리스트를 할당,
            {
                itemSlots[i].item = items[i];
            }

            for ( ; i < itemSlots.Length; i++) // 들어갈 항목이 없는 나머지 슬롯에  Null
            {
                itemSlots[i].item = null;
            }
        }

        //bool을 반화하는 슬롯체크 함수
        //항목을 items 리스트에 추가하고 ui를 새로고침한뒤 true;반환 
        public bool AddItem(Item item)
        {
            if (isFull())
            {
                return false;

            }
            items.Add(item);
            ReFreshUi();
            return true; 
        }
        //제거 제거가 가능하다면 제거하고 새로고침
        public bool RemoveItem(Item item)
        {
            if (items.Remove(item))
            {
                ReFreshUi();
                return true;
            }
            return false;
        }
        public bool isFull()//아이템 리스트가 슬롯칸보다 같거나 많을경우
        {
            return items.Count >= itemSlots.Length;
        }
    }

}

