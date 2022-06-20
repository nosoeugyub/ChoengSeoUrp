using System;
using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;


namespace NSY.Iven
{
    public class CraftSlot : MonoBehaviour, IPointerDownHandler
    {
        [Header("재료 갯수")]
        public Sprite lockSprite;
        public static Sprite lockSprite2;

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
        public Image childImgObject_copy;
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

                if (isActiveAndEnabled)
                    StartCoroutine(DelayChangSize());
            }
        }
        public IEnumerator DelayChangSize()
        {
            if (transform.childCount > 0)
            {
                childImgObject.enabled = true;
                childImgObject.sprite = _recipeItem.ItemSprite;
                childImgObject.SetNativeSize();

                float maxsizeWH = childImgObject.sprite.texture.height;
                if (childImgObject.sprite.texture.width >= childImgObject.sprite.texture.height)
                {
                    maxsizeWH = childImgObject.sprite.texture.width;
                }

                LayoutRebuilder.ForceRebuildLayoutImmediate(ResultSlotListImage.rectTransform);
              
                ScaleSlotImg(maxsizeWH);
                yield return new WaitForSeconds(0.01f);
            }
        }

        private void ScaleSlotImg(float maxsizeWH)
        {
            float scale = ResultSlotListImage.rectTransform.rect.width / maxsizeWH;
            if (scale != 0)
            {
                Vector3 scaleVec = new Vector3(scale, scale, 1);
                childImgObject.rectTransform.localScale = scaleVec;// ResultSlotListImage.rectTransform.rect.width /maxsizeWH;
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

                LayoutRebuilder.ForceRebuildLayoutImmediate(ResultSlotListImage.rectTransform);
                ScaleSlotImg(maxsizeWH);
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
                HaveRecipeUpdate();
            }
        }

        private void HaveRecipeUpdate()
        {
            if (_isHaverecipeItem == false)
            {
                childImgObject.color = new Color(1f, 1f, 1f);
                //childImgObject.color = new Color(1f, 0.5f, 0.5f);
                if (childImgObject_copy)
                {
                    childImgObject_copy.enabled = true;
                childImgObject_copy.color = new Color(1f, 1f, 1f);
                }
            }
            else
            {
                if (childImgObject_copy)
                {
                childImgObject_copy.color = new Color(1f, 1f, 1f);
                    childImgObject_copy.enabled = false;
                }
                childImgObject.color = new Color(1f, 1f, 1f);
            }
        }

        private void Awake()
        {
            childImgObject_copy = transform.GetChild(1).GetComponent<Image>();
            childImgObject_copy.gameObject.SetActive(true);
            childImgObject_copy.rectTransform.localScale = Vector3.one*0.5f;
            childImgObject_copy.sprite = Resources.Load<Sprite>("Lock_v0");
            childImgObject_copy.SetNativeSize();
            HaveRecipeUpdate();
        }
        public void SetSpriteLock(Sprite sprite)
        {
            lockSprite = sprite;
            if (!childImgObject_copy)
            {
                SetLockObj();
            }
            if(lockSprite)
                childImgObject_copy.GetComponent<Image>().sprite = lockSprite;

            HaveRecipeUpdate();

        }

        private void SetLockObj()
        {
            //childImgObject_copy = Instantiate(childImgObject.gameObject, transform);
        }

        private void OnValidate()
        {
            RecipeItem = _recipeItem;
            isHaveRecipeItem = _isHaverecipeItem;
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
