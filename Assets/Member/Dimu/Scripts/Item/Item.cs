using UnityEngine;

namespace DM.Inven
{
    [CreateAssetMenu(fileName = "New Item", menuName = "Item")]
    public class Item : ScriptableObject
    {
        [Header("Info")]
        [SerializeField] private ItemType itemType;
        [SerializeField] private string itemName;
        [TextArea]
        [SerializeField] private string itemDescription;
        [SerializeField] private Sprite itemSprite;
        [Header("Combination")]
        [SerializeField] private ingredientNeeded[] necessaryIngredient;

        public string ItemName => itemName;
        public string ItemDescription => itemDescription;
        public ItemType ItemType => itemType;
        public Sprite ItemSprite => itemSprite;
        public ingredientNeeded[] NnecessaryIngredient => necessaryIngredient;

    }

    public enum ItemType
    {
        //1차 재료
        None = 0, Twigs, Cutgrass, Rocks, Gold,leaf,Mud,
        //1차 식량
        Seed = 100, Berry,
        //2차재료
        Woodplank = 1000, Cutstone, String, torch,
        //2차 식량

        //3차재료
        Rope = 10000,
    }

    [System.Serializable]
    public class ingredientNeeded
    {
        [SerializeField]
        public Item item;

        [SerializeField]
        public int count;
    }
}