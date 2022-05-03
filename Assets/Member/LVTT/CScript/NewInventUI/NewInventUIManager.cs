﻿using DG.Tweening;
using NSY.Iven;
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
            Craftlist.craftwind[i].reimage = obj.RecipeItem.recipe[i].item.ItemSprite;
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
                Craftlist.craftwind[i].HaveAmount = item.Amount;
            }

            if (obj.RecipeItem.recipe[i].item == null)
            {
                Craftlist.craftwind[i].GetComponent<Image>().sprite = null;
            }
        }


    }
    public void FixedUpdate()
    {

        for (int i = 0; i < Craftlist.craftwind.Length; i++)
        {

            for (int j = 0; j < iven.ItemSlots.Count; j++)
            {
                if (Craftlist.craftwind[i]._item == iven.ItemSlots[j].item)
                {
                    Craftlist.craftwind[i].HaveAmount = iven.ItemSlots[j].Amount;
                    Craftlist.craftwind[i].RecipeHaverAmount.text = iven.ItemSlots[j].Amount.ToString(); ;
                    if (iven.ItemSlots[j].Amount == 0)
                    {
                        Craftlist.craftwind[i].RecipeHaverAmount.text = " ";
                    }
                }

            }
        }
    }



    public void BtnSolutino()
    {

        for (int i = 0; i < Craftlist.craftwind.Length; i++)
        {
            for (int j = 0; j < iven.ItemSlots.Count; j++)
            {
                if (Craftlist.craftwind[i].RecipeAmount <= Craftlist.craftwind[i].HaveAmount &&
                    Craftlist.craftwind[i]._item == iven.ItemSlots[j].item)///////////////
                {
                        addresults();
                        iven.ItemSlots[j].Amount -= Craftlist.craftwind[i].RecipeAmount;
                        Craftlist.craftwind[i].HaveAmount = iven.ItemSlots[j].Amount;
                        Craftlist.craftwind[i].RecipeHaverAmount.text = iven.ItemSlots[j].Amount.ToString(); 
                }
                else
                    return;


            }
        }
    }

    private void addresults()
    {
                iven.AddItem(nowSelectItem);
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
