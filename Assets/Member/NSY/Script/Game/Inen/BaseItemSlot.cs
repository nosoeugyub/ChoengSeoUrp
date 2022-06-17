using System;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace NSY.Iven
{
    public class BaseItemSlot : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerDownHandler, IPointerClickHandler
    {
        public Image itemImage;
        float ClickTime = 0;

        [SerializeField]
        ItemTooltip tooltip;
        [SerializeField]
        private Image childImgObject;
        //슬롯갯수
        public TextMeshProUGUI amountText;
        //public Text amountText;


        protected bool isPointerOver;

        private Color normalColor = Color.white;
        private Color disabledColor = new Color(1, 1, 1, 0);
        private Color cantInteractColor = new Color(1, 0.5f, 0.5f, 1); //채원이 빨갱이
        public event Action<BaseItemSlot> OnRightClickEvent;
        public event Action<BaseItemSlot> OnLeftClickEvent;
        public event Action<BaseItemSlot> OnPointerEnterEvent;
        public event Action<BaseItemSlot> OnPointerExitEvent;
        public event Action<BaseItemSlot> OnDubleClickEvent;
        //  public event Action<BaseItemSlot> OnLeftClickEvent;

        public bool canInteractWithSlot = true;


        /// <summary>
        /// 아이템
        /// </summary>
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
                    childImgObject.sprite = null;
                    childImgObject.color = disabledColor;
                }

                childImgObject.enabled = false;
                if (_item != null)
                {
                    childImgObject.sprite = _item.ItemSprite;
                    childImgObject.color = normalColor;
                    StartCoroutine(DelayChangSize());
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
            get
            {
                return _amount;
            }
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
                    amountText.enabled = _item != null && _amount > 1;
                    if (amountText.enabled)
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
        IEnumerator DelayChangSize()
        {
            yield return new WaitForEndOfFrame();
            ResizeChildImg();
        }
        private void ResizeChildImg()
        {
            if (transform.childCount > 0)
            {
                childImgObject.enabled = true;
                childImgObject = transform.GetChild(0).GetComponent<Image>();
                childImgObject.sprite = _item.ItemSprite;
                childImgObject.SetNativeSize();

                float maxsizeWH = childImgObject.sprite.texture.height;
                if (childImgObject.sprite.texture.width >= childImgObject.sprite.texture.height)
                    maxsizeWH = childImgObject.sprite.texture.width;

                LayoutRebuilder.ForceRebuildLayoutImmediate(itemImage.rectTransform);
                print(itemImage.rectTransform.rect.width);
                float scale = itemImage.rectTransform.rect.width / maxsizeWH;
                if (scale != 0)
                {
                    Vector3 scaleVec = new Vector3(scale, scale, 1);
                    childImgObject.rectTransform.localScale = scaleVec;// ResultSlotListImage.rectTransform.rect.width /maxsizeWH;
                }
            }
        }
        public void Interactble(bool canInteractable)// 채원이 빨갱잉
        {
            canInteractWithSlot = canInteractable;
            if (canInteractWithSlot)
            {
                childImgObject.color = normalColor;
            }
            else
            {
                childImgObject.color = cantInteractColor;
                print(item.ItemName);
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

        public void OnPointerExit(PointerEventData eventData)
        {

            isPointerOver = false;
            if (OnPointerExitEvent != null)
            {
                OnPointerExitEvent(this);
            }
        }


        public void OnPointerDown(PointerEventData eventData)
        {
            if (canInteractWithSlot == false)
            {
                return;
            }

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
                if (item is Item)
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
                ClickTime = Time.time;
            }

        }
    }

}

