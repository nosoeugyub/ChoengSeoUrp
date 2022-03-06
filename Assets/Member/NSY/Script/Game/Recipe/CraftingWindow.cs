using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using DM.Inven;
namespace NSY.Iven
{
    public class CraftingWindow : MonoBehaviour
    {
        [Header("Referneces")]
        [SerializeField] CraftingRecipeUI recipeUIPrefab;
        [SerializeField] RectTransform recipeUIParent;
        [SerializeField] List<CraftingRecipeUI> craftingRecipeUis;

        [Header("Pubilic Variables")]
        public ItemContainer ItemContainer;
        public List<CraftingRecipe> CraftingRecipes;

        public event Action<BaseItemSlot> OnPointerEnterEvent;
        public event Action<BaseItemSlot> OnPointerExitEvent;


        public void OnValidate()
        {
            Init();
        }

        private void Start()
        {
            Init();

            foreach (CraftingRecipeUI craftingRecipeUI in craftingRecipeUis)
            {
                craftingRecipeUI.OnPointerEnterEvent += OnPointerEnterEvent;
                craftingRecipeUI.OnPointerEnterEvent += OnPointerEnterEvent;
            }
        }

        private void Init()
        {
            recipeUIParent.GetComponentsInChildren<CraftingRecipeUI>(includeInactive: true, result: craftingRecipeUis);
            UpdateCraftingRecipes();
        }

        public void UpdateCraftingRecipes()
        {
            for (int i = 0; i < CraftingRecipes.Count; i++)
            {
                if (craftingRecipeUis.Count== i)
                {
                    craftingRecipeUis.Add(Instantiate(recipeUIPrefab, recipeUIParent, false));
                }
                else if (craftingRecipeUis[i] == null)
                {
                    craftingRecipeUis[i] = Instantiate(recipeUIPrefab, recipeUIParent, false);

                }

                craftingRecipeUis[i].ItemContainer = ItemContainer;
                craftingRecipeUis[i].CraftingRecipe = CraftingRecipes[i];
            }

            for (int i = CraftingRecipes.Count; i < craftingRecipeUis.Count; i++)
            {
                craftingRecipeUis[i].CraftingRecipe = null;
            }
        }
    }

}

