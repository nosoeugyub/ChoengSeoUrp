using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DM.Inven;
using UnityEngine.EventSystems;
using System;

namespace NSY.Iven
{
    public class ItemSlot : MonoBehaviour , IPointerClickHandler, IPointerEnterHandler,IPointerExitHandler , IDragHandler, IBeginDragHandler , IEndDragHandler , IDropHandler
    {
       
        [SerializeField]
        Image itemImage;
        private Item _item;

        public event Action<ItemSlot> OnPointerEnterEvent;
        public event Action<ItemSlot> OnPointerExitEvent;
        public event Action<ItemSlot> OnRightClickEvent;
        public event Action<ItemSlot> OnBeginDragEvent;
        public event Action<ItemSlot> OnEndDragEvent;
        public event Action<ItemSlot> OnDragEvent;
        public event Action<ItemSlot> OnDropEvent;

        private Color normalColor = Color.white;
        private Color disabledColor = new Color(1, 1, 1, 0);
        private Color dragColor = new Color(1, 1, 1, 0.5f);

        public Item item
        {
            get { return _item; }
            set
            {
                _item = value;
                if (_item == null)
                {
                    itemImage.color = disabledColor;
                }
                else
                {
                    itemImage.sprite = _item.ItemSprite;
                    itemImage.color = normalColor;

                }
            }
        }
      


        protected virtual void OnValidate() //이미지가 비어있으면 찾아서 등록
        {
            if (itemImage == null)
            {
                itemImage = GetComponent<Image>();
            }
        }
        public virtual bool CanReceiveItem(Item item)
        {

            return false;
        }
        public void OnPointerClick(PointerEventData eventData)
        {
            if (eventData != null && eventData.button == PointerEventData.InputButton.Right)
            {
                if (OnRightClickEvent != null)
                {
                    OnRightClickEvent(this);
                }
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
            if (item != null)
            {
                itemImage.color = dragColor;
            }
            if (OnEndDragEvent != null)
            {
                OnEndDragEvent(this);
            }
        }

        public void OnDrop(PointerEventData eventData)
        {
            if (eventData.button == PointerEventData.InputButton.Left)
            {
                OnDropEvent.Invoke(this);
            }
            
          //  if (OnDropEvent != null)
           // {
            //    OnDropEvent(this);
           // }
        }

        public void OnPointerEnter(PointerEventData eventData)
        {
            if (OnPointerEnterEvent != null)
            {
                OnPointerEnterEvent(this);
            }
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            if (OnPointerExitEvent != null)
            {
                OnPointerExitEvent(this);
            }
        }

      
    }


}
