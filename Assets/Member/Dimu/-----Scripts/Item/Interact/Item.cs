using UnityEngine;

[CreateAssetMenu(fileName = "New Item", menuName = "Item")]
public class Item : ScriptableObject
{
    [Header("Info")]
    [SerializeField] private OutItemType outItemType;
    [SerializeField] private InItemType inItemType;
    [SerializeField] private string itemName;
    [TextArea]
    [SerializeField] private string itemDescription;
    [SerializeField] private Sprite itemSprite;
    [SerializeField] private Material itemMaterial;
    [Header("Combination")]
    [SerializeField] private ingredientNeeded[] necessaryIngredient;
    [Header("MineItemVariable")]
    [SerializeField] private int chopCount;
    [SerializeField] private DropItem[] dropItems;
    [SerializeField] private int durability;


    public string ItemName => itemName;
    public string ItemDescription => itemDescription;
    public InItemType InItemType => inItemType;
    public OutItemType OutItemType => outItemType;
    public Sprite ItemSprite => itemSprite;
    public Material ItemMaterial => itemMaterial;
    public ingredientNeeded[] NnecessaryIngredient => necessaryIngredient;
    public DropItem[] DropItems => dropItems;
    public int ChopCount => chopCount;

}



[System.Serializable]
public class ingredientNeeded
{
    [SerializeField]
    public Item item;

    [SerializeField]
    public int count;
}
