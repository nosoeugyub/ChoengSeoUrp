using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BtnIven : MonoBehaviour
{

    [SerializeField] GameObject EquionmentPanel;
    [SerializeField] GameObject iteminfoPanel;
    [SerializeField] GameObject CraftPanel;
    [SerializeField] GameObject RecipeListPanel;

    [SerializeField] GameObject iventroy;



    //레시피목록
    [Header("레시피목록")]
    [SerializeField] GameObject ListBG;
    [SerializeField] GameObject ToolBtn;

    public bool isInven = true;
   public bool Craft;
    public bool Recipe;
    // 텝 버튼
    public void IvenBtnClick()
    {
        isInven = true;
        Craft = false;
        Recipe = false;
        if (isInven)
        {
            iventroy.SetActive(true);

            CraftPanel.SetActive(false);
            EquionmentPanel.SetActive(true);
            iteminfoPanel.SetActive(true);
            RecipeListPanel.SetActive(false);
        }
    }
    public void CraftBtnClick()
    {
        isInven = false;
        Craft = true;
        Recipe = false;
        if (Craft)
        {
            iventroy.SetActive(true);

            CraftPanel.SetActive(true);
            EquionmentPanel.SetActive(false);
            iteminfoPanel.SetActive(false);
            RecipeListPanel.SetActive(false);
        }
    } 

    public void RecipeListClick()
    {
        isInven = false;
        Craft = false;
        Recipe = true;
        if(Recipe)
        {
            iventroy.SetActive(false);

            CraftPanel.SetActive(false);
            EquionmentPanel.SetActive(false);
            iteminfoPanel.SetActive(false);
            RecipeListPanel.SetActive(true);
        }

    }



    //레시피 목록
    public void ToolBtnClick()
    {
        ListBG.SetActive(false);
        ToolBtn.SetActive(true);
    }

    //레시피 목록 뒤로가기

    public void ToolBackList()
    {
        ListBG.SetActive(true);
        ToolBtn.SetActive(false);
    }
}
