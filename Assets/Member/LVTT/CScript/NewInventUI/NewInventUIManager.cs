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


    [SerializeField] CraftList Craftlist;
    [SerializeField] CraftList Craftlist1;
    [SerializeField] CraftList Craftlist2;
    [SerializeField] InventoryNSY iven;
    [SerializeField] Item nowSelectItem;
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

        Craftlist.OnLeftClickEventss += ShowRecipe;
        Craftlist1.OnLeftClickEventss += ShowRecipe;
        Craftlist2.OnLeftClickEventss += ShowRecipe;


    }
    private void Start()
    {

    }

    private void OnDisable()
    {
        Craftlist.OnLeftClickEventss -= ShowRecipe;
    }
    public void ShowRecipe(CraftSlot obj)
    {
        print("OnLeftClickEventss");

        nowSelectItem = obj.RecipeItem;
        obj.ResultSlotImage.sprite = obj.RecipeItem.ItemSprite;
        obj.RecipeName.text = obj.RecipeItem.ItemName;
        obj.RecipeExplain.text = obj.RecipeItem.ItemDescription;



        for (int i = 0; i < Craftlist.craftwind.Length; i++)
        {
            Craftlist.craftwind[i]._item = obj.RecipeItem.recipe[i].item;
            if (obj.RecipeItem.recipe[i].item)
                Craftlist.craftwind[i].reimage = obj.RecipeItem.recipe[i].item.ItemSprite;
            else
                Craftlist.craftwind[i].reimage = null;

            Craftlist.craftwind[i].GetComponent<Image>().sprite = Craftlist.craftwind[i].reimage;



            //갯수
            Craftlist.craftwind[i].RecipeAmount = obj.RecipeItem.recipe[i].count;
            Craftlist.craftwind[i].RecipeCurrentAmount.text = obj.RecipeItem.recipe[i].count.ToString();
            if (obj.RecipeItem.recipe[i].count == 0)
            {
                Craftlist.craftwind[i].RecipeCurrentAmount.text = " ";
                Craftlist.craftwind[i].RecipeHaverAmount.text = " ";
            }
            //현재  가지고 있는 갯수
            foreach (ItemSlot item in iven.ItemSlots)
            {
                if (item.item == null) return;
                Craftlist.craftwind[i].HaveAmount += item.item.GetCountItems;
            }

            if (obj.RecipeItem.recipe[i].item == null)
            {
                Craftlist.craftwind[i].GetComponent<Image>().sprite = null;
            }
        }


    }
    public void FixedUpdate()  //현재 갯수
    {

        for (int i = 0; i < Craftlist.craftwind.Length; i++)
        {

            for (int j = 0; j < iven.ItemSlots.Count; j++)
            {
                if (Craftlist.craftwind[i].Item != null && Craftlist.craftwind[i].Item == iven.ItemSlots[j].item)
                {
                    Craftlist.craftwind[i].HaveAmount = iven.ItemSlots[j].item.GetCountItems;
                    Craftlist.craftwind[i].RecipeHaverAmount.text = iven.ItemSlots[j].item.GetCountItems.ToString();
                    if (iven.ItemSlots[j].Amount == 0)
                    {
                        Craftlist.craftwind[i].RecipeHaverAmount.text = " ";
                    }
                }

            }
        }
    }

    public void BtnSolution()
    {

        foreach (CraftWindow itemwid in Craftlist.craftwind)
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

            //ItemSlot minSlot = itemSlots[0];

            //foreach (var item in itemSlots)
            //{
            //    if (item.Amount < minSlot.Amount)
            //    {
            //        minSlot = item;
            //    }
            //}

            ////계산함
            //if (minSlot.Amount < ra)//칸 3개, 필요 5개일 경우
            //{
            //    minSlot.Amount = 0;
            //    ra -= minSlot.Amount;

            //    itemSlots.Remove(minSlot); //계산한 슬롯은 리스트에서 제외

            //    minSlot = itemSlots[0]; //무조건 칸 하나 남게 되어있음.

            //    foreach (var item in itemSlots) //다시 최소 슬롯 찾음
            //    {
            //        if (item.Amount < minSlot.Amount)
            //            minSlot = item;
            //    }
            //    if (minSlot.Amount < ra)
            //    {
            //        minSlot.Amount = 0;
            //        ra -= minSlot.Amount;
            //    }
            //    else
            //        minSlot.Amount = ra;
            //}
            //else
            //    minSlot.Amount = ra;

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
            itemwid.RecipeHaverAmount.text = minSlot.item.GetCountItems.ToString();

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
