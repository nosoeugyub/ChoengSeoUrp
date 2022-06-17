using System;
using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace NSY.Iven
{
    public class CraftWindow : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
    {
        //툴팁 이벤트
        public ItemTooltip TapToolTip;
        private Image childImgObject;
        [SerializeField] Image slotBackgroundImg;

        public Vector3 offset;
        public Image reimage;
        public Text RecipeCurrentAmount;
        public Text RecipeHaverAmount;
        private Color normalColor = Color.white;
        private Color cantInteractColors = new Color(1, 0.3f, 0.3f, 1f);
        private void OnValidate()
        {
            slotBackgroundImg = transform.GetChild(0).GetComponent<Image>();
            childImgObject = transform.GetChild(1).GetComponent<Image>();
            //Item = _item;
            RecipeAmount = _RecipeAmount;
            HaveAmount = _haveAmount;
        }
        public void Interactble(bool canInteractable)// 채원이 빨갱잉
        {
            if (canInteractable)
            {
                slotBackgroundImg.color = normalColor;
            }
            else
            {
                slotBackgroundImg.color = cantInteractColors;
            }
        }

        private Item _item;
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
                    childImgObject.sprite = null;
                    childImgObject.color = Color.clear;
                    Interactble(true);
                    //reimage = null;
                    SetRecipeCurrentAmountText(" ");
                    SetRecipeHaverAmountText(" ");
                }
                else
                {
                    childImgObject.sprite = _item.ItemSprite;
                    childImgObject.color = normalColor;
                    Interactble(true);

                    SetRecipeHaverAmountText(_item.GetCountItems.ToString());

                    if (RecipeAmount > _item.GetCountItems)
                        Interactble(false);
                    else
                        Interactble(true);

                    //ResizeChildImg();
                    StartCoroutine(DelayChangSize());
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
                if (_haveAmount == 0 && Item != null)
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
            ToolVec.x = GetComponent<Image>().rectTransform.position.x;// + offset.x;
            ToolVec.y = GetComponent<Image>().rectTransform.position.y;// - offset.y;
            ToolVec.z = GetComponent<Image>().rectTransform.position.z;
            TapToolTip.tooltipTransform.transform.position = ToolVec;
        }
        public void OnPointerExit(PointerEventData eventData)
        {

            TapToolTip.HideTooltip();
        }

        public IEnumerator DelayChangSize()
        {
            if (transform.childCount > 0)
            {
                childImgObject.enabled = true;
                childImgObject.sprite = _item.ItemSprite;
                childImgObject.SetNativeSize();

                float maxsizeWH = childImgObject.sprite.texture.height;
                if (childImgObject.sprite.texture.width >= childImgObject.sprite.texture.height)
                {
                    maxsizeWH = childImgObject.sprite.texture.width;
                }

                LayoutRebuilder.ForceRebuildLayoutImmediate(reimage.rectTransform);
                print(reimage.rectTransform.rect.width);
                yield return new WaitForSeconds(0.001f);
                ScaleSlotImg(maxsizeWH);
            }
        }
        private void ResizeChildImg()
        {
            if (transform.childCount > 0)
            {
                childImgObject.enabled = true;
                childImgObject.sprite = _item.ItemSprite;
                childImgObject.SetNativeSize();

                float maxsizeWH = childImgObject.sprite.texture.height;
                if (childImgObject.sprite.texture.width >= childImgObject.sprite.texture.height)
                {
                    maxsizeWH = childImgObject.sprite.texture.width;
                }

                LayoutRebuilder.ForceRebuildLayoutImmediate(reimage.rectTransform);
                print(reimage.rectTransform.rect.width);
                ScaleSlotImg(maxsizeWH);
            }
        }

        private void ScaleSlotImg(float maxsizeWH)
        {
            float scale = reimage.rectTransform.rect.width / maxsizeWH;
            if (scale != 0)
            {
                Vector3 scaleVec = new Vector3(scale, scale, 1);
                childImgObject.rectTransform.localScale = scaleVec;// ResultSlotListImage.rectTransform.rect.width /maxsizeWH;
            }
        }
    }

}

