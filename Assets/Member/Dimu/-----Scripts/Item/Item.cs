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

public enum OutItemType
{
    Talk,Tool, Mineral, Food, Collect, Build, Etc
}


public enum InItemType
{
    //1차 재료
    None = 0, Twigs, Cutgrass, Rocks, Gold, leaf, Mud, Trash,
    //1차 식량
    Seed = 100, Berry,
    //2차재료
    Woodplank = 1000, Cutstone, String, torch,
    //2차 식량

    //3차재료
    Rope = 10000,

    //도구
    Ax = 100000, Pickaxe, MagnifyingGlass,
    //미네랄
    tree = 1000000, Stone,
}

[System.Serializable]
public class ingredientNeeded
{
    [SerializeField]
    public Item item;

    [SerializeField]
    public int count;
}
