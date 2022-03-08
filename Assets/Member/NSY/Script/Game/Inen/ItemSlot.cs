﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DM.Inven;
using UnityEngine.EventSystems;
using System;

namespace NSY.Iven
{
    public class ItemSlot : BaseItemSlot , IDragHandler, IBeginDragHandler , IEndDragHandler , IDropHandler
    {
        bool isDragging;
      
        public event Action<BaseItemSlot> OnBeginDragEvent;
        public event Action<BaseItemSlot> OnEndDragEvent;
        public event Action<BaseItemSlot> OnDragEvent;
        public event Action<BaseItemSlot> OnDropEvent;

         private Color normalColor = Color.white;
      //  private Color disabledColor = new Color(1, 1, 1, 0);
        private Color dragColor = new Color(1, 1, 1, 0.2f);



        public override bool CanAddStack(Item item, int amount = 1)
        {
            return base.CanAddStack(item, amount) && Amount + amount <= item.MaximumStacks; 
        }


        public override bool CanReceiveItem(Item item)
        {
            return true;
        }
        protected override void OnDisable()
        {
            base.OnDisable();

            if (isDragging)
            {
                OnEndDrag(null);
            }
        }

        /// <summary>
        ///  드래그엔드롭
        /// </summary>
        /// <param name="eventData"></param>
        /// 

        Vector2 originalPosition; //슬롯 초기위치
        public void OnDrag(PointerEventData eventData)
        {
            //드레그
            // itemImage.transform.position = Input.mousePosition;
            if (OnDragEvent != null)
            {
                OnDragEvent(this);
            }
        }

        public void OnBeginDrag(PointerEventData eventData)
        {
            isDragging = true;

            if (item != null)
            {
                itemImage.color = dragColor;
            }
            if (OnBeginDragEvent != null)
            {
                OnBeginDragEvent(this);
            }
        }

        public void OnEndDrag(PointerEventData eventData)
        {
            isDragging = false;
            if (item != null)
            {
                itemImage.color = normalColor;
            }
            if (OnEndDragEvent != null)
            {
                OnEndDragEvent(this);
            }
        }

        public void OnDrop(PointerEventData eventData)
        {
            //if (eventData.button == PointerEventData.InputButton.Left)
            //{
            //    OnDropEvent.Invoke(this);
            //}
            
            if (OnDropEvent != null)
            {
                OnDropEvent(this);
            }
        }

      

      
    }


}
