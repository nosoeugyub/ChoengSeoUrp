using DG.Tweening;
using NSY.Iven;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using NSY.Manager;
public class NewInventUIManager : MonoBehaviour
{
    [Header("오픈될 오브젝트")] [SerializeField] RectTransform BG, invenBtn;
    [SerializeField] GameObject[] TabUI;
    [SerializeField] ScrollRect TopRect;


    //[SerializeField] CraftList[] Craftlists;
    [SerializeField] CraftWindow[] CraftWindows;
    //[SerializeField] CraftList Craftlist1;
    //[SerializeField] CraftList Craftlist2;
    [SerializeField] InventoryNSY iven;
    [SerializeField] CraftSlot nowSelectItem;

    [SerializeField] Scrollbar scrollbar;

    public int nowActiveTabIdx;

    [Header("열고 닫고 체인지")]
    public bool isOpen;
    public bool isChange;
    public int TabuiNumber;
    // Start is called before the first frame update
    void Awake()
    {
        for (int i = 0; i < TabUI.Length; i++)
        {
            for (int j = 1; j < TabUI[i].transform.childCount; j++)
            {
                TabUI[i].transform.GetChild(j).GetComponent<CraftList>().OnLeftClickEventss += ShowRecipe;
            }
        }

        iven.OnAddItemEvent += ShowRecipe;
    }

    private void Start()
    {
        nowActiveTabIdx = -1;
        //조건 검사 as

        UnLockManager.Unlockmanager.GetItemUnlocks += InterectingItem;
        UnLockManager.Unlockmanager.GetCreateUnlocks += CreateingItem;
        UnLockManager.Unlockmanager.GetUnlockUnlocks += UnlockingItem;
    }

    

    private void OnDisable()
    {
        for (int i = 0; i < TabUI.Length; i++)
        {
            for (int j = 1; j < TabUI[i].transform.childCount; j++)
            {
                TabUI[i].transform.GetChild(j).GetComponent<CraftList>().OnLeftClickEventss -= ShowRecipe;
            }
        }
        UnLockManager.Unlockmanager.GetItemUnlocks -= InterectingItem;
        UnLockManager.Unlockmanager.GetCreateUnlocks -= CreateingItem;
        UnLockManager.Unlockmanager.GetUnlockUnlocks -= UnlockingItem;

    }

    private void UnlockingItem() //레시피 해금 여부
    {
        for (int i = 0; i < iven.ItemSlots.Count; i++)
        {
            for (int r = 0; r < TabUI.Length; r++) //4 
            {
                for (int j = 1; j < TabUI[r].transform.childCount; j++)
                {
                    CraftList tempCL = TabUI[r].transform.GetChild(j).GetComponent<CraftList>();
                    for (int k = 0; k < tempCL.Craftslot.Count; k++)
                    {
                        if (tempCL.Craftslot[k].RecipeItem != null)
                        {
                            for (int n = 0; n < tempCL.Craftslot[k].RecipeItem.UnlockItem.Length; n++) // 이벤토리 슬롯과 탭창의 아이템의 해금재료아이템을 검사
                            {
                                if (iven.ItemSlots[i].item != null)
                                {
                                    Debug.Log("뒤질래");
                                    if (iven.ItemSlots[i].item.ItemName == tempCL.Craftslot[k].RecipeItem.UnlockItem[n].item.ItemName && //슬롯과 해료 이름이같으면
                                       iven.ItemSlots[i].item.GetCountItems == tempCL.Craftslot[k].RecipeItem.UnlockItem[n].count &&    // 갯수도 같으면
                                        tempCL.Craftslot[k].RecipeItem.UnlockItem[n].NeedCraftSlot.isHaveRecipeItem == true )     //추가 슬롯도필요하면                                                                                            // 다른 염료도 같으면
                                    {

                                        if (tempCL.Craftslot[k].isHaveRecipeItem == false )
                                        {
                                            tempCL.Craftslot[k].isHaveRecipeItem = true;
                                        }
                                        else
                                        {
                                            Debug.Log("왜안댕");
                                            return;
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            }
        }
    }
    private void CreateingItem()  //아이템 n개 제작 시 해금
    {
        for (int i = 0; i < iven.ItemSlots.Count; i++)
        {
            for (int r = 0; r < TabUI.Length; r++) //4 
            {
                for (int j = 1; j < TabUI[r].transform.childCount; j++)
                {
                    CraftList tempCL = TabUI[r].transform.GetChild(j).GetComponent<CraftList>();
                    for (int k = 0; k < tempCL.Craftslot.Count; k++)
                    {
                        if (tempCL.Craftslot[k].RecipeItem != null)
                        {
                            for (int n = 0; n < tempCL.Craftslot[k].RecipeItem.UnlockItem.Length; n++) // 이벤토리 슬롯과 탭창의 아이템의 해금재료아이템을 검사
                            {
                                if (iven.ItemSlots[i].item != null)
                                {
                                    Debug.Log("뒤질래");
                                    if (iven.ItemSlots[i].item.ItemName == tempCL.Craftslot[k].RecipeItem.UnlockItem[n].item.ItemName && //슬롯과 해료 이름이같으면
                                       iven.ItemSlots[i].item.GetCountItems == tempCL.Craftslot[k].RecipeItem.UnlockItem[n].count) // 갯수도 같으면
                                    {
                                      
                                        if (tempCL.Craftslot[k].isHaveRecipeItem == false)
                                        {
                                            tempCL.Craftslot[k].isHaveRecipeItem = true;
                                        }
                                        else
                                        {
                                            Debug.Log("왜안댕");
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            }
        }

    }


    void InterectingItem()//아이템 n개 획득 시 해금  검사
    {
        for (int i = 0; i < iven.ItemSlots.Count; i++)
        {
            for (int r = 0; r < TabUI.Length; r++) //4 
            {
                for (int j = 1; j < TabUI[r].transform.childCount; j++)
                {
                    CraftList tempCL = TabUI[r].transform.GetChild(j).GetComponent<CraftList>();
                    for (int k = 0; k < tempCL.Craftslot.Count; k++)
                    {
                        if (tempCL.Craftslot[k].RecipeItem != null)
                        {
                            for (int n = 0; n < tempCL.Craftslot[k].RecipeItem.UnlockItem.Length; n++) // 이벤토리 슬롯과 탭창의 아이템의 해금재료아이템을 검사
                            {
                                if (iven.ItemSlots[i].item != null)
                                {
                                    if (iven.ItemSlots[i].item.ItemName == tempCL.Craftslot[k].RecipeItem.UnlockItem[n].item.ItemName && //슬롯과 해료 이름이같으면
                                       iven.ItemSlots[i].item.GetCountItems == tempCL.Craftslot[k].RecipeItem.UnlockItem[n].count) // 갯수도 같으면
                                    {
                                        if (tempCL.Craftslot[k].isHaveRecipeItem == false)
                                        {
                                            tempCL.Craftslot[k].isHaveRecipeItem = true;
                                        }
                                        else
                                        {
                                            Debug.Log("");
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            }
        }


    }

    public void ShowRecipe()
    {
        for (int i = 0; i < CraftWindows.Length; i++)
        {
            if (nowSelectItem != null && nowSelectItem.RecipeItem.recipe[i].item != null)
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

        for (int i = 0; i < CraftWindows.Length; i++)
        {
            CraftWindows[i]._item = obj.RecipeItem.recipe[i].item;
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
        scrollbar.size = 0;

    }
    public void BtnSolution()
    {
        List<List<ItemSlot>> itemSlots = CanCraftItem();
        if (itemSlots == null) return;
        if (itemSlots[0].Count == 0) return;

        for (int i = 0; i < CraftWindows.Length; i++)
        {
            if (itemSlots[i].Count == 0) continue;

            //슬롯 중 숫자가 제일 작은 슬롯 구해옴
            int ra = CraftWindows[i].RecipeAmount;

            ItemSlot minSlot = itemSlots[i][itemSlots[i].Count - 1];

            do
            {
                //print(itemSlots[i].Count - 1);
                minSlot = itemSlots[i][itemSlots[i].Count - 1];

                foreach (var item in itemSlots[i]) //최소 슬롯 찾음
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
                    itemSlots[i].Remove(minSlot);
                }
                else
                    break;
            }
            while (ra > 0);// 그런데도

            minSlot.item.GetCountItems -= ra;
            CraftWindows[i].SetRecipeHaverAmountText(minSlot.item.GetCountItems.ToString());

            minSlot.Amount -= ra;
        }
        
        iven.AddItem(nowSelectItem.RecipeItem);
        SuperManager.Instance.unlockmanager.GetCreateItemUnLocking();

    }

    private List<List<ItemSlot>> CanCraftItem()
    {
        List<List<ItemSlot>> itemSlots = new List<List<ItemSlot>>();

        foreach (CraftWindow itemwid in CraftWindows)
        {
            List<ItemSlot> itemSlot1 = new List<ItemSlot>();
            itemSlots.Add(itemSlot1);
            if (itemwid.Item == null) continue;// 아이템이 널이면 다음 슬롯 검사로.

            for (int i = 0; i < iven.ItemSlots.Count; i++)
            {
                //특정 템 있는 슬롯 얻어옴
                if (itemwid.Item == iven.ItemSlots[i].item) // 필요 재료와 아이템 타입이 같다.
                {
                    if (itemwid.RecipeAmount > iven.ItemSlots[i].item.GetCountItems) return null; //개수가 모자라면 리턴시킴.
                    itemSlots[itemSlots.Count - 1].Add(iven.ItemSlots[i]);
                    //break;//개수 충족했다면 일단 다음.
                }
            }
            if (itemSlots[itemSlots.Count - 1].Count == 0) return null;
        }
        return itemSlots;
    }

    public void BtnTabSelect(int TabNum)
    {
        foreach (var item in TabUI)
        {
            item.SetActive(false);
        }
        TabUI[TabNum].SetActive(true);


        if (nowActiveTabIdx == TabNum)//같은탭을 눌렀다면
        {
            //nowActiveTabIdx = -1;
            if (isOpen == false) Open(); //닫혀있을 때는 열리기
            else close();
        }
        else //다르면
        {
            nowActiveTabIdx = TabNum;
            if (isOpen == false) Open(); //닫혀있을 때는 열리기
        }
        TopRect.content = TabUI[TabNum].GetComponent<RectTransform>();
    }

    public void Open()
    {
        if (isOpen == false)
        {
            BG.DOLocalMoveX(743, 1).SetEase(Ease.OutQuart);
            invenBtn.DOLocalMoveX(458 + 25, 1).SetEase(Ease.OutQuart);
            isOpen = true;
        }
    }

    public void MoveTabs(int index)
    {
        for (int i = 0; i < invenBtn.childCount; i++)
        {
            if (index == i)
                invenBtn.GetChild(i).DOLocalMoveX(0, 1).SetEase(Ease.OutQuart);
            else
                invenBtn.GetChild(i).DOLocalMoveX(25, 1).SetEase(Ease.OutQuart);
        }
    }
    public void close()
    {
        BG.DOLocalMoveX(1182, 1).SetEase(Ease.OutQuart);
        invenBtn.DOLocalMoveX(922, 1).SetEase(Ease.OutQuart);
        isOpen = false;
    }

}
