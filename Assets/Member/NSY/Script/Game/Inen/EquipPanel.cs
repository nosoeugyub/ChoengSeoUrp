﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DM.Inven;
using System;

namespace NSY.Iven
{
    public class EquipPanel : MonoBehaviour
    {
        [SerializeField] Transform equipmentSlotsParent;
       public EquipmentSlot[] equipmentSlots;
        public ReSultSlot ResultEquip;
        public chagimg chage;

        public event Action<BaseItemSlot> OnPointerEnterEvent;
        public event Action<BaseItemSlot> OnPointerExitEvent;
        public event Action<BaseItemSlot> OnLeftClickEvent;
        public event Action<BaseItemSlot> OnLeftClickEvents;
        public event Action<BaseItemSlot> OnBeginDragEvent;
        public event Action<BaseItemSlot> OnEndDragEvent;
        public event Action<BaseItemSlot> OnDragEvent;
        public event Action<BaseItemSlot> OnDropEvent;


        private void Start()                        
        {
            for (int i = 0; i < equipmentSlots.Length; i++)
            {
                equipmentSlots[i].OnPointerEnterEvent +=  OnPointerEnterEvent;
                equipmentSlots[i].OnPointerExitEvent +=  OnPointerExitEvent;
                equipmentSlots[i].OnLeftClickEvent += OnLeftClickEvent;
                equipmentSlots[i].OnBeginDragEvent +=   OnBeginDragEvent;
                equipmentSlots[i].OnEndDragEvent += OnEndDragEvent;
                equipmentSlots[i].OnDragEvent +=  OnDragEvent;
                equipmentSlots[i].OnDropEvent +=  OnDropEvent;
              
            }
            ResultEquip.OnLeftClickEvent += OnLeftClickEvents;
        }


        private void OnValidate()
        {
            equipmentSlots = equipmentSlotsParent.GetComponentsInChildren<EquipmentSlot>();
        }
        //추가 out 매개 변수는 반환 값이 있는 것과 같지만 해당 값이 할당될 매개변수로 변수를 전달 합니다.
        //슬롯에 추가하기전에 out변수에 이전항목을 할당해야합니다
        public bool AddItem(Item item )
        {
           
            for (int i = 0; i < equipmentSlots.Length; i++)
            {
                if (equipmentSlots[i].item == null)//equipmentSlots[i].equipmentType == item.equipmentType
                {
                  //  item =  equipmentSlots[i].item;
                    equipmentSlots[i].item = item;
                    equipmentSlots[i].Amount = 1;
                    return true;
                }
               if (equipmentSlots[i].item != null)
                {
                 //   item = equipmentSlots[i+1].item;
                    equipmentSlots[i+1].item = item;
                    equipmentSlots[i+1].Amount = 1;
                    return true;
                }

            }
            
           
            return false;
        }

        //무기 장착
        public bool AddResultItem(Item item)//결과 장착
        {
            Debug.Log("삐용쓰");
            for (int i = 0; i < equipmentSlots.Length; i++)
            {
               
                if (equipmentSlots[i].item == item && ResultEquip.item == null)
                {
                    item = equipmentSlots[i].item;
                    ResultEquip.item = item;
                    ResultEquip.Amount = 1;
                    equipmentSlots[i].item = null;
                    equipmentSlots[i].Amount = 0;
                    return true;
                }
                if(ResultEquip.item != null && equipmentSlots[i].item == null)
                {
                    Debug.Log("바꾸라");
                    item = equipmentSlots[i].item; //슬롯에서 아이템으로 
                    equipmentSlots[i].item = ResultEquip.item;
                    ResultEquip.item = item;
   
                }
                if (ResultEquip.item != null && equipmentSlots[i+1].item == null)
                {
                    Debug.Log("바꾸라2");
                    item = equipmentSlots[i+1].item; //슬롯에서 아이템으로 
                    equipmentSlots[i+1].item = ResultEquip.item;
                    ResultEquip.item = item;

                }
                if (ResultEquip.item != null && equipmentSlots[i + 2].item == null)
                {
                    Debug.Log("바꾸라3");
                    item = equipmentSlots[i+1].item; //슬롯에서 아이템으로 
                    equipmentSlots[i+1].item = ResultEquip.item;
                    ResultEquip.item = item;

                }
            }
            
            return false;
        }


        public bool RemoveResultItem(Item item) //장착한 아이템을 장비창으로 되돌림 
        {
            for (int i = 0; i < equipmentSlots.Length; i++)
            {
                if (ResultEquip.item == item && equipmentSlots[i].item == null)
                {
                    Debug.Log("라자냐 쳐먹고싶다");
                    equipmentSlots[i].item = item;
                    equipmentSlots[i].Amount = 1;
                    ResultEquip.item = null;
                    ResultEquip.Amount = 0;
                   
                   
                    return true;
                }
                 if (ResultEquip.item == item && equipmentSlots[i+1].item == null)
                {

                    Debug.Log("권경수 바부");
                    equipmentSlots[i+1].item = item;
                    equipmentSlots[i+1].Amount = 1;
                    ResultEquip.item = null;
                    ResultEquip.Amount = 0;

                    return true;

                }
                 if(ResultEquip.item == item && equipmentSlots[i+2].item == null)
                {
                    Debug.Log("권경수 tq");
                    equipmentSlots[i + 2].item = item;
                    equipmentSlots[i + 2].Amount = 1;
                    ResultEquip.item = null;
                    ResultEquip.Amount = 0;

                    return true;
                }
             
            }
                
            
            return false;
        }

        public bool changeItem(Item item)
        {
            for (int i = 0; i < equipmentSlots.Length; i++)
            {
                if (ResultEquip.item != null)
                {

                    Debug.Log("바꿔라");
                    equipmentSlots[i].item = item;
                    equipmentSlots[i].Amount = 1;
                    return true;
                }
            }
           
            return false;
        }
    }

}

