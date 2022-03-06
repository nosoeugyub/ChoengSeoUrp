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

            foreach (CraftingRecipe craftingRecipeUI in craftingRecipeUis)
            {

            }
        }
    }

}

