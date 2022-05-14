using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DM.Inven;



namespace NSY.Iven
{
	public class CraftWindow : MonoBehaviour
	{
        public Sprite reimage;
        public Text RecipeCurrentAmount;
        public Text RecipeHaverAmount;

        private void OnValidate()
        {
            Item = _item;
            RecipeAmount = _RecipeAmount;
            HaveAmount = _haveAmount;
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
      
        public void MiuseBtn()
        {
            _haveAmount -= 1;
        }

        public void SetRecipeHaverAmountText(string str)
        {
            RecipeHaverAmount.text = str;
        }
        public void SetRecipeCurrentAmountText(string str)
        {
            RecipeCurrentAmount.text = str;
        }
    }

}

