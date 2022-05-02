using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DM.Inven;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using System;

namespace NSY.Iven
{
    public class CraftSlot : MonoBehaviour , IPointerDownHandler
    {
        [Header("재료 갯수")]
       

        [Header("현재 흭득한 갯수")]
        public Text[] ReCipeamountText;
        

        [Header("결과 이미지")]
        public Image ResultSlotListImage;
        public Image ResultSlotImage;

        [Header("재료 이미지")]
        public Image[] RecipeSlot;
       
        [Header("결과 이름")]
        public Text RecipeName;


       
        public Text reamountText;
        public Text HaveAmount;


        public event Action<CraftSlot> OnLeftClickEventss;
         [SerializeField]
        private Item _recipeItem;


        public Item RecipeItem
        {
            get
            {
                return _recipeItem;
            }
            set
            {
                _recipeItem = value;

                ResultSlotListImage.sprite = _recipeItem.ItemSprite;
            }
        }


      
       


        private void OnValidate()
        {
            RecipeItem = _recipeItem;
        }


        void Update()
        {

        }
       

        public void OnPointerDown(PointerEventData eventData)
        {
         
            if (eventData != null && eventData.button == PointerEventData.InputButton.Left)
            {
                if (OnLeftClickEventss != null)
                {
                 
                    OnLeftClickEventss(this);
                }
            }
        }
    }

}
