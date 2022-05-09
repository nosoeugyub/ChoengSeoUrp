﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DM.Inven;
using UnityEngine.EventSystems;
using System;
using TMPro;

namespace NSY.Iven
{
    public class BaseItemSlot : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerDownHandler , IPointerClickHandler
    {
        public Image itemImage;
        float ClickTime = 0;

        [SerializeField]
        ItemTooltip tooltip;
        //슬롯갯수
        public TextMeshProUGUI amountText;
        //public Text amountText;


        protected bool isPointerOver;

        private Color normalColor = Color.white;
        private Color disabledColor = new Color(1, 1, 1, 0);
        private Color cantInteractColor = new Color(1, 0.3f, 0.3f, 0.5f);
        public event Action<BaseItemSlot> OnRightClickEvent;
        public event Action<BaseItemSlot> OnLeftClickEvent;
        public event Action<BaseItemSlot> OnPointerEnterEvent;
        public event Action<BaseItemSlot> OnPointerExitEvent;
        public event Action<BaseItemSlot> OnDubleClickEvent;
      //  public event Action<BaseItemSlot> OnLeftClickEvent;

        public Item _item;
        public Item item
        {
            get { return _item; }
            set
            {
                _item = value;
                
                if (_item == null && Amount != 0)
                {
                    Amount = 0;
                }

                if (_item == null)
                {
                    itemImage.sprite = null;
                    itemImage.color = disabledColor;
                }
                

                if(_item != null)
                {
                    itemImage.sprite = _item.ItemSprite;
                    itemImage.color = normalColor;

                }

                if (isPointerOver)
                {

                    OnPointerExit(null);
                    OnPointerEnter(null);
                }

              
            }
        }

        //슬로갯수 프로퍼티
        private int _amount;
        public int Amount
        {
            get { return _amount; }
            set
            {
                _amount = value;
                if (_amount < 0)
                {
                    //item.GetCountItems = 0;
                    _amount = 0;
                }
                if (_amount == 0 && item != null)
                {
                    //item.GetCountItems = 0;
                    item = null;
                }
                if (amountText != null) //&& _item.MaximumStacks > 1 
                {
                    amountText.enabled = _item != null && _amount > 1 ;
                    if(amountText.enabled)
                    {
                        amountText.text = _amount.ToString();
                    }
                }


            }
        }
       
        // 이미지 텍스트
        protected virtual void OnValidate() //이미지가 비어있으면 찾아서 등록
        {
            if (tooltip == null)
            {
                tooltip = FindObjectOfType<ItemTooltip>();
            }

            if (itemImage == null)
            {
                itemImage = GetComponent<Image>();
            }

            if (amountText == null)
            {
                amountText = GetComponentInChildren<TextMeshProUGUI>();
            }
            item = _item;
            Amount = _amount;
            
        }
        protected virtual void OnDisable()
        {
            if (isPointerOver)
            {
              
                OnPointerExit(null);
            }
        }
        public void Interactble(bool canInteractable)
        {

            if (canInteractable)
            {
                itemImage.color = normalColor;
            }
            else
            {
                itemImage.color = cantInteractColor;
            }


        }

        public virtual bool CanAddStack(Item Item, int amount = 1)
        {
            return item != null && item.ItemName == Item.ItemName;
        }
        //갯수채우기 함수

        public virtual bool CanReceiveItem(Item item)
        {
            return false;
        }

        public void OnPointerEnter(PointerEventData eventData)
        {

            isPointerOver = true;
            if (OnPointerEnterEvent != null)
            {
                OnPointerEnterEvent(this);
            }
        }

        public  void OnPointerExit(PointerEventData eventData)
        {

            isPointerOver = false;
            if (OnPointerExitEvent != null)
            {
                OnPointerExitEvent(this);
            }
        }


        public void OnPointerDown(PointerEventData eventData)
        {
            
            if (eventData != null && eventData.button == PointerEventData.InputButton.Right)
            {
                if (OnRightClickEvent != null)
                {
                   
                    OnRightClickEvent(this);
                }
            }
            if (eventData != null && eventData.button == PointerEventData.InputButton.Left)
            {
                if (OnLeftClickEvent != null)
                {
                  
                    OnLeftClickEvent(this);
                }
                if (item is EquippableItem)
                {
                    tooltip.ShowEqulTooltip((EquippableItem)item);
                }
                if (item is Item )
                {
                    tooltip.ShowItemTooltip(item);
                }

            }
        }

        void IPointerClickHandler.OnPointerClick(PointerEventData eventData)
        {
            if (Mathf.Abs(Time.time - ClickTime) < 0.75f)
            {
                if (OnDubleClickEvent != null)
                {
                    OnDubleClickEvent(this);
                }
            }
            else
            {
                ClickTime = Time.time ;
            }
           
        }
    }

}

