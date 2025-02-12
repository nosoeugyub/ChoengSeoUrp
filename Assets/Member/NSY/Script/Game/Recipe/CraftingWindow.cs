﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using DM.Inven;
namespace NSY.Iven
{
    public class CraftingWindow : MonoBehaviour
    {
		[Header("References")]
		[SerializeField] CraftingRecipeUI recipeUIPrefab;
		[SerializeField] RectTransform recipeUIParent;
		[SerializeField] List<CraftingRecipeUI> craftingRecipeUIs;

		[Header("Public Variables")]
		public ItemContainer ItemContainer;
		public List<CraftingRecipe> CraftingRecipes;

		public event Action<BaseItemSlot> OnPointerEnterEvent;
		public event Action<BaseItemSlot> OnPointerExitEvent;

		private void OnValidate()
		{
			Init();
		}

		private void Start()
		{
			Init();

			
		}

		private void Init()
		{
			recipeUIParent.GetComponentsInChildren<CraftingRecipeUI>(includeInactive: true, result: craftingRecipeUIs);
			UpdateCraftingRecipes();
		}

		public void UpdateCraftingRecipes()
		{
			for (int i = 0; i < CraftingRecipes.Count; i++)
			{
				if (craftingRecipeUIs.Count == i)
				{
					craftingRecipeUIs.Add(Instantiate(recipeUIPrefab, recipeUIParent, false));
				}
				else if (craftingRecipeUIs[i] == null)
				{
					craftingRecipeUIs[i] = Instantiate(recipeUIPrefab, recipeUIParent, false);
				}

				craftingRecipeUIs[i].itemContainer = ItemContainer;
				craftingRecipeUIs[i].Craftingrecipe = CraftingRecipes[i];
			}

			for (int i = CraftingRecipes.Count; i < craftingRecipeUIs.Count; i++)
			{
				craftingRecipeUIs[i].Craftingrecipe = null;
			}
		}
	}

}

