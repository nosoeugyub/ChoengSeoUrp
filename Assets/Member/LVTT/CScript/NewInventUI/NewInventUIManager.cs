using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using NSY.Iven;
using System;

public class NewInventUIManager : MonoBehaviour
{
    [SerializeField] GameObject [] TabUI;
    [SerializeField] ScrollRect TopRect;


    [SerializeField] CraftList Craftlist;
    [SerializeField] InventoryNSY iven;
    private int Sum = 0;

    // Start is called before the first frame update
    void Awake()
    {
        Craftlist.OnLeftClickEventss += ShowRecipe;
    }
    private void OnDisable()
    {
        Craftlist.OnLeftClickEventss -= ShowRecipe;
    }
    public void ShowRecipe(CraftSlot obj)
    {
        Debug.Log("나오셈");
        obj.ResultSlotImage.sprite = obj.RecipeItem.ItemSprite;
        obj.RecipeName.text = obj.RecipeItem.ItemName;

     

        for (int i = 0; i < Craftlist.craftwind.Length ; i++)
        {
            Craftlist.craftwind[i]._item = obj.RecipeItem.recipe[i].item;
            Craftlist.craftwind[i].reimage = obj.RecipeItem.recipe[i].item.ItemSprite;
            Craftlist.craftwind[i].GetComponent<Image>().sprite = Craftlist.craftwind[i].reimage;



            //갯수
            Craftlist.craftwind[i].RecipeAmount = obj.RecipeItem.recipe[i].count;
            Craftlist.craftwind[i].RecipeCurrentAmount.text = obj.RecipeItem.recipe[i].count.ToString();
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

                }
              
            }
        }
    }
    


    public void BtnSolutino( )
    {
        addresults();
        for (int i = 0; i < Craftlist.craftwind.Length; i++)
        {
            for (int j = 0; j < iven.ItemSlots.Count; j++)
            {
                if (Craftlist.craftwind[i].RecipeAmount % Craftlist.craftwind[i].HaveAmount == Sum)
                {
                    if (Craftlist.craftwind[i]._item == iven.ItemSlots[j].item)
                    {
                        iven.ItemSlots[j].Amount -= Craftlist.craftwind[i].RecipeAmount;
                        Craftlist.craftwind[i].HaveAmount = iven.ItemSlots[j].Amount;  
                        Craftlist.craftwind[i].RecipeHaverAmount.text = iven.ItemSlots[j].Amount.ToString(); ;

                    }
                }
                else
                    return;
               

            }
        }
    }

    private void addresults()
    {
        foreach (CraftSlot item in Craftlist.Craftslot)
        {
            if (item.ResultSlotListImage.sprite == item.ResultSlotImage.sprite)
            {
               
                iven.AddItem(item.RecipeItem);
               
            }
            
        }
       
         
        
    }

 
    public void BtnTabSelect(int TabNum)
    {
        for (int i=0;i<TabUI.Length;i++)
        {
            TabUI[i].SetActive(false);
        }
        TabUI[TabNum].SetActive(true);
        TopRect.content = TabUI[TabNum].GetComponent<RectTransform>();
    }
}
