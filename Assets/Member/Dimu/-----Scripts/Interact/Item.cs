using TT.BuildSystem;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using NSY.Manager;
using NSY.Iven;

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
    [SerializeField] float cleanAmount;
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
    private int GetCountitems = 0;
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
    public float CleanAmount => cleanAmount;
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
        //MaximumStacks = 20;
        itemName = itemPrefab.name;
        //Debug.Log(itemName);
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
    public RecipeIteminfo[] recipe;

    public UnlcokIteminfo[] UnlockItem;
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
    [Header("잠금해제에 필요한 아이템")]
    public Item item;
    [Header("잠금해제할 아이템의 갯수")]
    public int count;

    [Header("추가로 필요할 조합슬롯")]
    public CraftSlot NeedCraftSlot;

}


