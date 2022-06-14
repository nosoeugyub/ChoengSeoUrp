using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DM.Inven;
using System;
using UnityEngine.EventSystems;

namespace NSY.Iven
{
	public class CraftWindow : MonoBehaviour , IPointerEnterHandler , IPointerExitHandler 
    {
        //툴팁 이벤트
        public ItemTooltip TapToolTip;

        public Vector3 offset;
        private Color normalColor = Color.white;
        public Image reimage;
        public Text RecipeCurrentAmount;
        public Text RecipeHaverAmount;
        private Color cantInteractColors = new Color(1, 0.3f, 0.3f, 1f);
        private void OnValidate()
        {
            Item = _item;
            RecipeAmount = _RecipeAmount;
            HaveAmount = _haveAmount;
        }
        public void Interactble(bool canInteractable)// 채원이 빨갱잉
        {
            if (canInteractable)
            {
                reimage.color = normalColor;
            }
            else
            {
                reimage.color = cantInteractColors;
            }
        }

        public Item _item;
		public Item Item
        {
            get
            {
                return _item;
            }
            set
            {
                _item = value;
             
                if (_item == null)
                {
                    reimage = null;
                }
                


            }
        }

      
        public int _RecipeAmount;
        public int RecipeAmount
        {
            get
            {
                return _RecipeAmount;
            }
            set
            {
                _RecipeAmount = value;
                SetRecipeCurrentAmountText(_RecipeAmount.ToString());
                if (_RecipeAmount == 0)
                {
                    SetRecipeCurrentAmountText(" ");
                }
                
            }
        }
       // [SerializeField]
        public int _haveAmount;
        public int HaveAmount
        {
            get
            {
                return _haveAmount;
            }
            set
            {
                _haveAmount = value;
                SetRecipeHaverAmountText(_haveAmount.ToString());
                if (_haveAmount <= 0)
                {
                    _haveAmount = 0;
                   
                }
                if (_haveAmount == 0  && Item != null )
                {
                    SetRecipeHaverAmountText(" ");
                }
                
            }
        }
        public void SetRecipeHaverAmountText(string str)
        {
            RecipeHaverAmount.text = str;
        }
        public void SetRecipeCurrentAmountText(string str)
        {
            RecipeCurrentAmount.text = str;
        }
      
        public void OnPointerEnter(PointerEventData eventData)
        {
           
            TapToolTip.ShowItemTooltip(Item);
           
                Vector3 ToolVec = TapToolTip.tooltipTransform.transform.position;
                ToolVec.x = GetComponent<Image>().rectTransform.position.x + offset.x;
            ToolVec.y = GetComponent<Image>().rectTransform.position.y - offset.y;
            ToolVec.z = 0;
            TapToolTip.tooltipTransform.transform.localPosition = ToolVec;
        }
        public void OnPointerExit(PointerEventData eventData)
        {
          
            TapToolTip.HideTooltip();
        }
    }

}

