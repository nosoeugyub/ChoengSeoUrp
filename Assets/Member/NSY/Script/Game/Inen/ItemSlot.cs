using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DM.Inven;

namespace NSY.Iven
{
    public class ItemSlot : MonoBehaviour
    {
       
        [SerializeField]
        Image itemImage;
        private Item _item;
        public Item item
        {
            get { return _item; }
            set
            {
                _item = value;
                if (_item == null)
                {
                    itemImage.enabled = false;
                }
                else
                {
                    itemImage.sprite = _item.ItemSprite;
                    itemImage.enabled = true;

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
    }


}
