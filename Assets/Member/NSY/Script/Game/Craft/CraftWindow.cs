using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace NSY.Iven
{
	public class CraftWindow : MonoBehaviour
	{
		[Header("References")]
		[SerializeField] CraftManager recipeUIPrefab;
		[SerializeField] RectTransform recipeUIParent;
		[SerializeField] List<CraftManager> craftingRecipeUIs;

		[Header("Public Variables")]
		public ItemContainer ItemContainer;
		public List<CraftingRecipe> CraftingRecipes;

		
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
			recipeUIParent.GetComponentsInChildren<CraftManager>(includeInactive: true, result: craftingRecipeUIs);
			UpdateCraftingRecipes();
		}

		public void UpdateCraftingRecipes()
		{
			for (int i = 0; i < CraftingRecipes.Count; i++)
			{
				

				craftingRecipeUIs[i].itemContainer = ItemContainer;
				//craftingRecipeUIs[i].CraftingRecipe = CraftingRecipes[i];
			}

			for (int i = CraftingRecipes.Count; i < craftingRecipeUIs.Count; i++)
			{
				//craftingRecipeUIs[i].CraftingRecipe = null;
			}
		}
	}

}

