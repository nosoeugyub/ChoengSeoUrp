using UnityEngine;
using DM.Inven;

namespace DM.Building
{
    [CreateAssetMenu(fileName = "IngredientToNeed", menuName = "IngredientToNeed/new IngredientToNeed", order = 0)]
    public class BuildingInfo : ScriptableObject
    {
        [SerializeField]
        public ingredientNeeded[] ingredientNeededs;
        [SerializeField]
        private int buildID; 
        

        public int BuildingID()
        {
            return buildID;
        }

        public int GetNeedCountw(int i)
        {
            return ingredientNeededs[i].count;
        }
        public Item GetIngredientNeededsItemType(int i)
        {
            return ingredientNeededs[i].item;
        }
        public int GetIngredientNeededsLength()
        {
            return ingredientNeededs.Length;
        }
    }
}