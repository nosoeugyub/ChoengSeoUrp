﻿using System.Collections;
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
       
        public void Update()
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                AddToolRecipe(testitem);
               
                Debug.Log("스페이스바는 누름 ㅋ");
            }
        }

        public  void AddToolRecipe(Item item)
        {
            if (NoneRecipeList == null)
            {
                Debug.Log("레시피 다떨어짐");
            }

            for (int i = 0; i < NoneRecipeList.Length; i++)
            {

                if (testitem.ItemName == NoneRecipeList[i].ItemName)
                {
                    RecipeList.Add(NoneRecipeList[i]);
                    Destroy(NoneRecipeList[i]);
                    NoneRecipeList[i] = null;

                } 

            
            }
            SrotRecipe();
            return ;
            
        }
        //순회해서 정렬
        public void SrotRecipe()
        {
           
        }
    }


}

