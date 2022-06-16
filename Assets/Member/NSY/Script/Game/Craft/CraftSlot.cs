using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;


namespace NSY.Iven
{
    public class CraftSlot : MonoBehaviour, IPointerDownHandler
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

        [Header("결과 설명")]
        public Text RecipeExplain;

        public Text reamountText;
        public Text HaveAmount;


        public event Action<CraftSlot> OnLeftClickEventss;
        [SerializeField]
        private Item _recipeItem;
        [SerializeField]
        private Image childImgObject;
        //지금 갖고있는아이템
        public Item RecipeItem
        {
            get
            {
                return _recipeItem;
            }
            set
            {
                _recipeItem = value;

                ResultSlotListImage.enabled = true;
                ResultSlotListImage.color = Color.clear;

                ResizeChildImg();
            }
        }

        private void ResizeChildImg()
        {
            if (transform.childCount > 0)
            {
                childImgObject = transform.GetChild(0).GetComponent<Image>();
                childImgObject.sprite = _recipeItem.ItemSprite;
                childImgObject.SetNativeSize();

                float maxsizeWH = childImgObject.sprite.texture.height;
                if (childImgObject.sprite.texture.width >= childImgObject.sprite.texture.height)
                {
                    maxsizeWH = childImgObject.sprite.texture.width;
                }

                float scale = ResultSlotListImage.rectTransform.rect.width / maxsizeWH;
                if (scale != 0)
                {
                    print(ResultSlotListImage.rectTransform.rect.width);
                    Vector3 scaleVec = new Vector3(scale, scale, 1);
                    childImgObject.rectTransform.localScale = scaleVec;// ResultSlotListImage.rectTransform.rect.width /maxsizeWH;
                }
                else
                {
                 //   childImgObject.rectTransform.localScale = Vector3.one * 0.1f;// ResultSlotListImage.rectTransform.rect.width /maxsizeWH;

                }
            }
        }

        [SerializeField]
        private bool _isHaverecipeItem;
        public bool isHaveRecipeItem
        {
            get
            {
                return _isHaverecipeItem;
            }
            set
            {
                _isHaverecipeItem = value;

                if (!childImgObject) return;
                if (_isHaverecipeItem == false)
                {
                    childImgObject.color = new Color(0.5f, 0.5f, 0.5f);
                }
                else
                {
                    childImgObject.color = new Color(1f, 1f, 1f);
                }
            }
        }






        private void OnValidate()
        {
            RecipeItem = _recipeItem;
            isHaveRecipeItem = _isHaverecipeItem;
        }


        void Update()
        {

        }


        public void OnPointerDown(PointerEventData eventData)
        {
            if (isHaveRecipeItem == true)
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

}
