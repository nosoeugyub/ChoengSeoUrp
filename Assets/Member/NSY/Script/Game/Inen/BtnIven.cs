using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BtnIven : MonoBehaviour
{

    [SerializeField] GameObject EquionmentPanel;
    [SerializeField] GameObject iteminfoPanel;
    [SerializeField] GameObject CraftPanel;
    [SerializeField] GameObject RecipeListPanel;
    [SerializeField] GameObject RecipeListBackBtn;

    [SerializeField] GameObject iventroy;



    //레시피목록
    [Header("레시피목록")]
    [SerializeField] GameObject ListBG;
    [SerializeField] GameObject ToolBtn;
    //우편 목록
    [Header("우편 목록")]
    [SerializeField] GameObject postPanel;
    [SerializeField] GameObject ClosePost;
    [SerializeField] GameObject PostpanelBG;

    public bool isInven = true;
    public bool Craft;
    public bool Recipe;
    public bool Post;
    // 텝 버튼
    public void IvenBtnClick()
    {
        isInven = true;
        Craft = false;
        Recipe = false;
        Post = false;
        if (isInven)
        {
            iventroy.SetActive(true);

            CraftPanel.SetActive(false);
            EquionmentPanel.SetActive(true);
            iteminfoPanel.SetActive(true);
            RecipeListPanel.SetActive(false);
            postPanel.SetActive(false);
        }
    }
    public void CraftBtnClick()
    {
        isInven = false;
        Craft = true;
        Recipe = false;
        Post = false;
        if (Craft)
        {
            iventroy.SetActive(true);

            CraftPanel.SetActive(true);
            EquionmentPanel.SetActive(false);
            iteminfoPanel.SetActive(false);
            RecipeListPanel.SetActive(false);
            postPanel.SetActive(false);
        }
    } 

    public void RecipeListClick()
    {
        isInven = false;
        Craft = false;
        Recipe = true;
        Post = false;
        if (Recipe)
        {
            iventroy.SetActive(false);

            CraftPanel.SetActive(false);
            EquionmentPanel.SetActive(false);
            iteminfoPanel.SetActive(false);
            RecipeListPanel.SetActive(true);
            postPanel.SetActive(false);
        }

    }



    //레시피 목록
    public void ToolBtnClick()
    {
        ListBG.SetActive(false);
        ToolBtn.SetActive(true);
        RecipeListBackBtn.SetActive(true);
    }

    //레시피 목록 뒤로가기

    public void ToolBackList()
    {
        ListBG.SetActive(true);
        ToolBtn.SetActive(false);
        RecipeListBackBtn.SetActive(false);
    }

    //우편함 클릭
    public void PostBtnClick()
    {
        isInven = false;
        Craft = false;
        Recipe = false;
        Post = true;
        if (Post)
        {
            iventroy.SetActive(false);

            CraftPanel.SetActive(false);
            EquionmentPanel.SetActive(false);
            iteminfoPanel.SetActive(false);
            RecipeListPanel.SetActive(false);
            postPanel.SetActive(true);
        }
       
    }
    //이장 우편
    public void PostSlotClick()
    {
       
        GameObject.Find("MainPostSlot").transform.Find("MainNpcPost").gameObject.SetActive(true);
    }
    public void CloseSlotClick()
    {
        GameObject.Find("MainPostSlot").transform.Find("MainNpcPost").gameObject.SetActive(false);
    }
}
