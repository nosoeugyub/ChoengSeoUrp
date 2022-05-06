using DG.Tweening;
using NSY.Iven;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class NewInventUIManager : MonoBehaviour
{
    [Header("오픈될 오브젝트")] [SerializeField] RectTransform BG, invenBtn;
    [SerializeField] GameObject[] TabUI;
    [SerializeField] ScrollRect TopRect;


    [SerializeField] CraftList[] Craftlists;
    [SerializeField] CraftWindow[] CraftWindows;
    //[SerializeField] CraftList Craftlist1;
    //[SerializeField] CraftList Craftlist2;
    [SerializeField] InventoryNSY iven;
    [SerializeField] CraftSlot nowSelectItem;
    public int Sum = 0;
    public int num = 0;
    public int hum = 0;
    [Header("열고 닫고 체인지")]
    public bool isOpen;
    public bool isChange;
    public int TabuiNumber;
    // Start is called before the first frame update
    void Awake()
    {
        foreach (var item in Craftlists)
        {
            item.OnLeftClickEventss += ShowRecipe;
        }
        iven.OnAddItemEvent += ShowRecipe;

    }
    private void Start()
    {

    }

    private void OnDisable()
    {
        foreach (var item in Craftlists)
        {
            item.OnLeftClickEventss -= ShowRecipe;
        }
    }
    public void ShowRecipe()
    {
        for (int i = 0; i < CraftWindows.Length; i++)
        {
            if(nowSelectItem!=null && nowSelectItem.RecipeItem.recipe[i].item !=null)
            CraftWindows[i].SetRecipeHaverAmountText(nowSelectItem.RecipeItem.recipe[i].item.GetCountItems.ToString());

        }
    }
    public void ShowRecipe(CraftSlot obj)
    {
        print("OnLeftClickEventss");

        nowSelectItem = obj;
        obj.ResultSlotImage.sprite = obj.RecipeItem.ItemSprite; //결과 이미지
        obj.RecipeName.text = obj.RecipeItem.ItemName;
        obj.RecipeExplain.text = obj.RecipeItem.ItemDescription;

        for (int i = 0; i < Craftlists[0].craftwind.Length; i++)
        {
            Craftlists[0].craftwind[i]._item = obj.RecipeItem.recipe[i].item;
            if (obj.RecipeItem.recipe[i].item)
            {
                CraftWindows[i].GetComponent<Image>().enabled = true;
                CraftWindows[i].GetComponent<Image>().sprite = obj.RecipeItem.recipe[i].item.ItemSprite;
            }
            else
            {
                CraftWindows[i].GetComponent<Image>().enabled = false;
                CraftWindows[i].SetRecipeCurrentAmountText(" ");
                CraftWindows[i].SetRecipeHaverAmountText(" ");
                continue;
            }
            //갯수
            CraftWindows[i].RecipeAmount = obj.RecipeItem.recipe[i].count;
            CraftWindows[i].SetRecipeHaverAmountText(obj.RecipeItem.recipe[i].item.GetCountItems.ToString());
        }
    }
    public void FixedUpdate()  //현재 갯수
    {

        //for (int i = 0; i < Craftlist.craftwind.Length; i++)
        //{

        //    for (int j = 0; j < iven.ItemSlots.Count; j++)
        //    {
        //        if ( Craftlist.craftwind[i].Item == iven.ItemSlots[j].item)
        //        {
        //            //Craftlist.craftwind[i].HaveAmount = iven.ItemSlots[j].item.GetCountItems;
        //            //Craftlist.craftwind[i].RecipeHaverAmount.text = iven.ItemSlots[j].item.GetCountItems.ToString();
        //            if (iven.ItemSlots[j].Amount == 0)
        //            {
        //                Craftlist.craftwind[i].RecipeHaverAmount.text = " ";
        //            }
        //        }
        //    }
        //}
    }
    public void BtnSolution()
    {

        foreach (CraftWindow itemwid in CraftWindows)
        {
            List<ItemSlot> itemSlots = new List<ItemSlot>();
            for (int i = 0; i < iven.ItemSlots.Count; i++)
            {
                //특정 템 있는 슬롯 얻어옴
                if (itemwid.Item == iven.ItemSlots[i].item)
                {
                    if (itemwid.Item == null) return;
                    if (itemwid.RecipeAmount > iven.ItemSlots[i].item.GetCountItems) return;
                    itemSlots.Add(iven.ItemSlots[i]);
                }
            }

            if (itemSlots.Count <= 0) return; //슬롯이 비었다면 취소

            //슬롯 중 숫자가 제일 작은 슬롯 구해옴
            int ra = itemwid.RecipeAmount;

            ItemSlot minSlot = itemSlots[itemSlots.Count - 1];

            do
            {
                minSlot = itemSlots[itemSlots.Count - 1];

                foreach (var item in itemSlots) //최소 슬롯 찾음
                {
                    if (item.Amount < minSlot.Amount)
                    {
                        minSlot = item;
                    }
                }

                if (minSlot.Amount < ra) //전체 6  민 칸 1개, 필요 2개일 경우 
                {
                    minSlot.item.GetCountItems -= minSlot.Amount;//6- 1
                    ra -= minSlot.Amount; //2 -= 1
                    minSlot.Amount = 0;
                    itemSlots.Remove(minSlot);
                }
                else
                    break;
            }
            while (ra > 0);// 그런데도

            minSlot.item.GetCountItems -= ra;
            itemwid.SetRecipeHaverAmountText(minSlot.item.GetCountItems.ToString());

            nowSelectItem.RecipeItem.GetCountItems++;
            iven.AddItem(nowSelectItem.RecipeItem);

            minSlot.Amount -= ra;
        }
    }





    public void BtnTabSelect(int TabNum)
    {


        Open();
        for (TabuiNumber = 0; TabuiNumber < TabUI.Length; TabuiNumber++)
        {

            TabUI[TabuiNumber].SetActive(false);

        }
        TabuiNumber = TabNum;

        TabUI[TabNum].SetActive(true);

        if (TabUI[0].activeSelf == true)
        {
            Sum += 2;
            num = 0;
            hum = 0;
            if (Sum == 4)
            {

                close();

            }
        }
        if (TabUI[1].activeSelf == true)
        {
            Sum = 0;
            num += 3;
            hum = 0;
            if (num == 6)
            {

                close();

            }
        }
        if (TabUI[2].activeSelf == true)
        {
            Sum = 0;
            num = 0;
            hum += 4;
            if (hum == 8)
            {

                close();

            }
        }
        TopRect.content = TabUI[TabNum].GetComponent<RectTransform>();




    }

    public void Open()
    {


        if (isOpen == false)
        {
            BG.DOLocalMoveX(707, 1).SetEase(Ease.OutQuart);
            invenBtn.DOLocalMoveX(1294, 1).SetEase(Ease.OutQuart);
            isOpen = true;
        }


    }
    int a = 0;
    public void close()
    {
        Sum = 0;
        num = 0;
        hum = 0;
        BG.DOLocalMoveX(1212.46f, 1).SetEase(Ease.OutQuart);
        invenBtn.DOLocalMoveX(1797.24f, 1).SetEase(Ease.OutQuart);
        isOpen = false;

    }

}
