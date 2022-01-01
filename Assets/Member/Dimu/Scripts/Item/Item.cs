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
        [SerializeField] private Item[] necessaryIngredient;

        public string ItemName => itemName;
        public string ItemDescription => itemDescription;
        public ItemType ItemType => itemType;
        public Sprite ItemSprite => itemSprite;
        public Item[] NnecessaryIngredient => necessaryIngredient;

    }

    public enum ItemType
    {
        //1차 재료
        None = 0, Branch, Ricesheaf, Shingle,
        //1차 식량
        Seed = 100, Berry,
        //2차재료
        Wood = 1000, Stone, Rope, torch
        //3차재료

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