using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DM.Inven;

namespace NSY.Iven
{
    public class RecipeSlot : BaseItemSlot
    {
        public Image ResultImg;

       

        //1번쨰 칸
        [Header("1번쨰 칸")]
        public Image firstSlotImg;
        public Text firstSlotAmount;
        //2 번쨰칸
        [Header("2번쨰 칸")]
        public Image SecondSlotImg;
        public Text SecondSlotAmount;
        //3번쨰칸
        [Header("3번쨰 칸")]
        public Image ThridSlotImg;
        public Text ThridSlotAmount;


        //미흭득 컬러 흭득 커럴
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
                    firstSlotImg.sprite = _RecipeItem.recipe[0].item.ItemSprite ;
                    SecondSlotImg.sprite = _RecipeItem.recipe[1].item.ItemSprite;
                    SecondSlotImg.sprite = _RecipeItem.recipe[2].item.ItemSprite;

                   
                }
              

            }
        }
        [Header("레시피 재료 갯수")]
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

                    firstSlotAmount.text = _RecipeItem.recipe[0].Count.ToString();
                    SecondSlotAmount.text = _RecipeItem.recipe[1].Count.ToString();
                    ThridSlotAmount.text = _RecipeItem.recipe[2].Count.ToString();


                }
            }
        }
        protected override void OnValidate()
        {


            RecipeItem = _RecipeItem;
            RecipeAmout = _RecipeAmount;
        }

    }


}

