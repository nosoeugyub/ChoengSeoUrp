using UnityEngine;

namespace DM.Inven
{
    [CreateAssetMenu(fileName = "New Item", menuName = "Item")]
    public class Item : ScriptableObject
    {
        public ItemType itemType;
        public string itemName;
        [TextArea]
        public string itemDescription;
        public Sprite itemSprite;
    }

    public enum ItemType
    {
       None, Branch, Stone, Seed, Berry
    }
}