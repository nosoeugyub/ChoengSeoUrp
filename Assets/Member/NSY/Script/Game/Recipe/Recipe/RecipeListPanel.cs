using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

namespace NSY.Iven
{
    public class RecipeListPanel : MonoBehaviour
    {
        public List<Item> RecipeList = new List<Item>(); // 흭득 레시피


        public Item[] NoneRecipeList; // 미흭득 레시피


      public  Item testitem;
     
        [Header("정렬 컴포넌트")]
        RectTransform AddUiPos;
        public List<RecipeSlot> Recipeslot = new List<RecipeSlot>();
        public int AddStack = 0;
       
      
        public  void AddToolRecipe(Item item)
        {
            if (NoneRecipeList == null)
            {
               
            }

            for (int i = 0; i < NoneRecipeList.Length; i++)
            {

                if (testitem.ItemName == NoneRecipeList[i].ItemName)
                {
                    RecipeList.Add(NoneRecipeList[i]);
                    Destroy(NoneRecipeList[i]);
                    NoneRecipeList[i] = null;
                  
                    UpdateListRecipe();
                }
              
            }
           


        }
        //순회해서 정렬
        public void UpdateListRecipe()
        {
           
            for (int i = 0; i < Recipeslot.Count; i++)
            {
                for (int j = 0; j < RecipeList.Count; j++)
                {
                    if (Recipeslot[i]._RecipeItem.ItemName  == RecipeList[j].ItemName)
                    {
                        Recipeslot[i].transform.SetSiblingIndex(AddStack);
                        Color color = Recipeslot[i].itemImage.color ;
                        color.a = 1f;
                        Recipeslot[i].itemImage.color = color;
                       
                    }
                }
            }
            AddStack++;
        }


    }


}

