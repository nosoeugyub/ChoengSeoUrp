using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DM.Inven;

namespace NSY.Iven
{
    public class RecipeSlot : BaseItemSlot
    {
        [Header("레시피 슬롯")]
        public Image ResultImg;

       

       

        public Image[] MatImage;
        public Text[] AmountImage;

        //미흭득 컬러 흭득 컬러
        private Color NullColor = new Color(1, 1, 1, 0);
        private Color NothingColor = new Color(1, 1, 1, 0.5f);
        private Color GetColor = new Color(1, 1, 1, 1);
  
        [Header("레시피 아이템")]
        public Item _RecipeItem;
        public Item RecipeItem
        {
            get
            {
                return _RecipeItem;
            }

            set
            {
                for (int i = 0; i < _RecipeItem.recipe.Length; i++)
                {
                    ResultImg.sprite = _RecipeItem.ItemSprite;
                    MatImage[i].sprite = _RecipeItem.recipe[i].item.ItemSprite;
                  

                   
                }
              

            }
        }
        //[Header("레시피 재료 갯수")]
        public int _RecipeAmount;
        public int RecipeAmout
        {
            get
            {
                return _RecipeAmount;
            }
            set
            {

                for (int i = 0; i < _RecipeItem.recipe.Length; i++)
                {
                    AmountImage[i].text = _RecipeItem.recipe[i].Count.ToString();
                 


                }
              
            }
        }
        protected override void OnValidate()
        {
           // base.OnValidate();
            RecipeItem = _RecipeItem;
            RecipeAmout = _RecipeAmount;


        }
            

    }


}

