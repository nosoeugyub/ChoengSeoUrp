using TT.BuildSystem;
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
    [SerializeField] int cleanAmount;
    [SerializeField] int eatAmount;

    //[Header("Combination")]
    //[SerializeField] private ingredientNeeded[] necessaryIngredient;
    [Header("MineItemVariable")]
    [SerializeField] private int chopCount;
    [SerializeField] private DropItem[] dropItems;
    [SerializeField] private int durability;
    [SerializeField] private GameObject itemPrefab;
    [SerializeField] private string usingToolSoundName;


    //NSY추가 및 조건 명시
    //아이템 식별 넘버
    [SerializeField]
    private int _ItemNubering;
    public int ItemNubering
    {
        get
        {
            return _ItemNubering;
        }
        set
        {
            _ItemNubering = value;
        }
    }

    [SerializeField]
    private int GetCountitems;
    public int GetCountItems
    {
        get
        {
            return GetCountitems;
        }
        set
        {
            GetCountitems = value;
           

        }
    }

    public Sprite ItemSprite
    {
        get
        {
            return itemSprite;
        }
        set
        {
            itemSprite = value;
        }
    }
    public Material ItemMaterial
    {
        get
        {
            return itemMaterial;
        }
        set
        {
            itemMaterial = value;
        }
    }
    public GameObject ItemPrefab
    {
        get
        {
            return itemPrefab;
        }
        set
        {
            itemPrefab = value;
        }
    }
    public OutItemType OutItemType
    {
        get
        {
            return outItemType;
        }
        set
        {
            outItemType = value;
        }
    }
    public InItemType InItemType
    {
        get
        {
            return inItemType;
        }
        set
        {
            inItemType = value;
        }
    }

 

    public string ItemName => itemName;
    public string ItemDescription => itemDescription;
    //public InItemType InItemType => inItemType;
    //public OutItemType OutItemType => outItemType;
    //public Sprite ItemSprite => itemSprite;
    //public Material ItemMaterial => itemMaterial;
    public int CleanAmount => cleanAmount;
    public int EatAmount => eatAmount;
    //public ingredientNeeded[] NnecessaryIngredient => necessaryIngredient;
    public DropItem[] DropItems => dropItems;
    public int ChopCount => chopCount;
    public string UsingToolSoundName => usingToolSoundName;
    //public GameObject ItemPrefab => itemPrefab;


 

    //NSY추가 
    public int MaximumStacks = 1;

    private void OnEnable()
    {
        GetCountItems = 0;
    }
    public virtual Item GetCopy()
    {
        return this;
    }

    public virtual void Destroy()
    {
        //Destroy(this);
    }
  
    public int GetNeedCountw(int i)
    {
        return recipe[i].count;
    }
    public Item GetIngredientNeededsItemType(int i)
    {
        return recipe[i].item;
    }

    [Header("레시피")]
    [SerializeField]
    public RecipeIteminfo[] recipe;

    [Header("해제조건")]
    [SerializeField]
    public UnlcokIteminfo[] UnlcokIteminfos;

}
[System.Serializable]
public class RecipeIteminfo
{
   
    public Item item;

    public int count;
    
}

//해제 조건 아이템
[System.Serializable]
public class UnlcokIteminfo
{

    public Item item;

    public int count;

}


